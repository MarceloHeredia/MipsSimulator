using System;
using System.IO;
using System.Linq;

namespace MipsSimulator.Processor

{

    public class DataBlock
    {
        #region Variables Declaration
        #region getter delegates
        //mux and mux's delegates
        Tools.DelegateMUX getPC;//teste...
        Tools.DelegateMUX getAluOut;
        Tools.DelegateMUX get4 = Get4;

        //these ones aren't being initialized because they work on instance methods...
        Tools.DelegateMUX getMDR;
        Tools.DelegateMUX getA;
        Tools.DelegateMUX getB;
        Tools.DelegateMUX getRt;
        Tools.DelegateMUX getRd;
        Tools.DelegateMUX getSignExtendedImm;
        Tools.DelegateMUX getSignExtendedShift2Imm;
        Tools.DelegateMUX getAluResult;
        Tools.DelegateMUX getAluZero;
        Tools.DelegateMUX getReverseAluZero;

        #endregion

        #region mux
        public MUX muxIorD;
        public MUX muxRegDest;
        public MUX muxMemtoReg;
        public MUX muxAluSrcA;
        public MUX muxAluSrcB;
        public MUX muxPCSrc;
        //muxAluZero serve para corrigir a saida Zero da ula
        //se for um beq, deve passar a saida normal, se for um bne deve passar o inverso da saida zero
        public MUX muxAluZero;
        #endregion

        #region regs e memoria
        //registers & memory
        public NormalReg A;
        public NormalReg B;
        public NormalReg MDR;
        public NormalReg aluOut;
        public Registers registers;
        public IR instrReg;
        public Memory memory;
        public ALU alu;
        public AluControl aluCtrl;
        public PC pc;
        #endregion
        //adress after last instruction
        public UInt32 finalPC;
        //control block indication of beq or bne on stage 3
        public bool beqbne = false;

        #endregion
        //inicializar todos circuitos e fazer as respectivas conexoes de acordo 
        public DataBlock(String[] lines)
        {
            this.muxIorD = new MUX(2);
            this.muxRegDest = new MUX(2);
            this.muxMemtoReg = new MUX(2);
            this.muxAluSrcA = new MUX(2);
            this.muxAluSrcB = new MUX(4);
            this.muxPCSrc = new MUX(2);
            this.muxAluZero = new MUX(2);

            this.A = new NormalReg("A");
            this.B = new NormalReg("B");
            this.MDR = new NormalReg("MDR");
            this.registers = new Registers();
            this.instrReg = new IR();
            this.memory = new Memory();
            this.aluOut = new NormalReg("ULA Saida");
            this.alu = new ALU();
            this.aluCtrl = new AluControl();
            //fill memory
            this.finalPC = ReadFileRoutine(lines, memory);
            this.pc = new PC(finalPC);

            #region instanciando delegates e preenchendo os MUX
            getPC = pc.GetPCValue;
            getMDR = MDR.GetRegVal;
            getA = A.GetRegVal;
            getB = B.GetRegVal;
            getAluOut = aluOut.GetRegVal;
            getRt = instrReg.GetRt;
            getRd = instrReg.GetRd;
            getSignExtendedImm = instrReg.GetSignalExtendedImmediate;
            getSignExtendedShift2Imm = instrReg.GetSignalExtendedShift2Immediate;
            getAluResult = alu.GetAluResult;
            getAluZero = alu.GetAluZero;
            getReverseAluZero = alu.GetReverseAluZero;

            muxIorD.PlaceEntry(0, getPC); //posicao 0 do mux tem PC
            muxIorD.PlaceEntry(1, getAluOut); //posicao 1 do mux tem AluOut

            muxRegDest.PlaceEntry(0, getRt); //posicao 0 do mux  tem rt
            muxRegDest.PlaceEntry(1, getRd); //posicao 1 do mux tem rd

            muxMemtoReg.PlaceEntry(0, getAluOut); //posicao 0 do mux tem registrador Ula Saida
            muxMemtoReg.PlaceEntry(1, getMDR); //posicao 1 do mux tem dado do Registrador de dados da Memoria

            muxAluSrcA.PlaceEntry(0, getPC); //posicao 0 do mux tem PC
            muxAluSrcA.PlaceEntry(1, getA); //posicao 1 do mux tem valor do registrador A

            muxAluSrcB.PlaceEntry(0, getB); //posicao 00 do mux tem valor do registrador B
            muxAluSrcB.PlaceEntry(1, get4); //posicao 01 do mux tem  o numero 4 
            muxAluSrcB.PlaceEntry(2, getSignExtendedImm); //posicao 10 do mux tem os bits de imediato com sinal extendido 
            muxAluSrcB.PlaceEntry(3, getSignExtendedShift2Imm); //posicao 11 do mux tem os bits de imediato com sinal extendido com shift de 2 bits a esquerda

            muxPCSrc.PlaceEntry(0, getAluResult); //posicao 0 do mux tem saida da Ula
            muxPCSrc.PlaceEntry(1, getAluOut); //posicao 1 do mux tem valor de Ula Saida

            muxAluZero.PlaceEntry(0, getAluZero); //posicao 0 indica saida normal, ou seja, BEQ
            muxAluZero.PlaceEntry(1, getReverseAluZero); //posicao 1 indica inversa, ou seja, BNE

            #endregion
        }

        /// <summary>
        /// Metodo responsavel por executar o ciclo do bloco de controle
        /// O metodo IgnoreException ajuda a simular o bloco de dados tentando passar os dados por todos circuitos e sendo barrado pelo Bloco de Controle
        /// </summary>
        public void RunCicle()
        {//TEST EXECUTION FIRST CICLE!
            //tenta colocar o valor do mux IouD no endereco de memoria
            if (muxIorD.isSet)
            {
                Tools.IgnoreException(() => memory.Adress = Convert.ToUInt32(muxIorD.Value));
            }

            if (memory.ReadAllowed)
            {
                Tools.IgnoreException(() => instrReg.Instr = memory.MemData); // IR instrucao <- dado da memoria

                Tools.IgnoreException(() => MDR.Reg = memory.MemData); //MDR dado <- dado da memoria
            }

            registers.Reg1 = instrReg.Rs;
            registers.Reg2 = instrReg.Rt;

            Tools.IgnoreException(() => registers.RegTarget = muxRegDest.Value); // registrador alvo é o resultado do mux RegDst

            Tools.IgnoreException(() => registers.Write = muxMemtoReg.Value);

            A.Reg = registers.Reg1;

            B.Reg = registers.Reg2;

            Tools.IgnoreException(() => aluCtrl.Funct = instrReg.Funct); // Operacao_da_Ula  <- IR bits de Funcao
            Tools.IgnoreException(() => aluCtrl.SetAluOp()); //Seleciona operacao a realizar
            Tools.IgnoreException(() => alu.Op = aluCtrl.Op);//passa a operacao para a ALU


            Tools.IgnoreException(() => alu.Entrada1 = muxAluSrcA.Value); //Entrada 1 da ULA <- valor do mux A

            Tools.IgnoreException(() => alu.Entrada2 = muxAluSrcB.Value); //Entrada 2 da ULA <- valor do mux B

            Tools.IgnoreException(() => alu.Operate()); //manda a ula operar

            if (!beqbne) //etapa 3 desvio condicional nao escreve em ulaSaida 
            {
                Tools.IgnoreException(() => aluOut.Reg = alu.Result); //registrador ULA Saida <- resultado da ULA
            }

            bool aluZero = false;                               //aluZero sera 0 como padrao para evitar erros 

            Tools.IgnoreException(() => aluZero = Convert.ToBoolean(muxAluZero.Value)); //tenta pegar o valor de aluZero

            memory.WriteInMem = B.Reg;

            if (muxPCSrc.isSet)
            {
                this.pc.ChangePCValue(muxPCSrc.Value, aluZero); //nao pode ignorar excecao nesse metodo pois se chegar ao fim da execucao ele que encerrara
            }

        }
        public static Int32 Get4() => 4;

        /// <summary>
        /// Inverte RS e RT quando tem uma instrucao de SHIFT
        /// </summary>
        /// <param name="shiftInstruction">Instrucao de shift</param>
        /// <returns>Instrucao de shift corrigida</returns>
        public static String RepairShifts(String shiftInstruction)
        {
            //abre o codigo hexadecimal em 32 bits
            var binaryInstr = Convert.ToString(Convert.ToUInt32(shiftInstruction, 16), 2).PadLeft(32, '0');
            string rs = binaryInstr.Substring(6, 5);
            string rt = binaryInstr.Substring(11, 5);
            //inverte o rs e rt do codigo binario
            binaryInstr = String.Concat(binaryInstr.Substring(0, 6), rt, rs, binaryInstr.Substring(16, 16));
            //retorna o hexadecimal novamente
            return String.Concat("0x", Convert.ToString(Convert.ToUInt32(binaryInstr, 2), 16).PadLeft(8, '0'));
        }

        public UInt32 ReadFileRoutine(String[] lines, Memory memo)
        {
            try
            {
                UInt32 currPc = Tools.defaultIniPC;
                UInt32 currData = Tools.iniMemData;
                Boolean readingData = false;

                foreach (var line in lines)
                {
                    if (line.Trim().Contains(".text") ||
                        line.Trim().Contains(".globl") ||
                        line.Trim() == String.Empty)
                        continue;
                    //started reading Data
                    if (line.Trim().StartsWith(".data"))
                    {
                        readingData = true;
                        continue;
                    }

                    if (readingData)
                    {
                        String[] datas = line.Split(':')[1].Trim().Split(' ');
                        datas = datas.Where(dt => dt.Trim() != String.Empty && !dt.Trim().StartsWith('.')).ToArray();

                        foreach (var dt in datas)
                        {
                            memo.WMFFR(currData, Convert.ToInt32(dt));
                            currData += 4;
                        }
                    }
                    else
                    {
                        string instr = line;
                        if (instr.StartsWith("0x"))
                            instr = instr.Substring(2);

                        var binInstr = Convert.ToString(Convert.ToUInt32(instr, 16), 2).PadLeft(32, '0');
                        if (binInstr.Substring(0, 6) == "000000")//TIPO R
                        {
                            if (binInstr.Substring(26, 6) == "000000" ||
                                 binInstr.Substring(26, 6) == "000010")//se for shift left or shift right
                            {
                                instr = RepairShifts(instr);
                            }
                        }

                        memo.WMFFR(currPc, Convert.ToInt32(instr, 16));
                        currPc += 4;
                    }
                }
                return currPc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
