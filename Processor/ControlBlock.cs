using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace MipsSimulator.Processor
{
    public class ControlBlock
    {
        private DataBlock dt;
        private Byte opCode;
        private Byte funct;
        private bool has4cicle;
        private bool has5cicle;



        #region signal delegates
        // nao estaticos
        Tools.DelegateBCInt32 sIouD;
        Tools.DelegateBCBool sLerMem;
        Tools.DelegateBCBool sEscMem;
        Tools.DelegateBCInt32 sMemParaReg;
        Tools.DelegateBCBool sIREsc;
        Tools.DelegateBCInt32 sFontePC;
        Tools.DelegateBCByte sULAOp;
        Tools.DelegateBCInt32 sULAFonteB;
        Tools.DelegateBCInt32 sULAFonteA;
        Tools.DelegateBCBool sEscReg;
        Tools.DelegateBCInt32 sRegDst;
        Tools.DelegateBCInt32 sBeqOrBne; //select between beq and bne on bit zero of alu 
        Tools.DelegateBCBool sPCEsc;
        Tools.DelegateBCBool sPCEscCond;

        Tools.DelegateResetMux rstIouD;
        Tools.DelegateResetMux rstMemParaReg;
        Tools.DelegateResetMux rstFontePC;
        Tools.DelegateResetMux rstULAFonteB;
        Tools.DelegateResetMux rstULAFonteA;
        Tools.DelegateResetMux rstRegDst;
        Tools.DelegateResetMux rstBeqOrBne;

        #endregion

        public ControlBlock(String[] lines)
        {
            dt = new DataBlock(lines);
            sPCEsc = dt.pc.SetWritePC;
            sPCEscCond = dt.pc.SetWriteCondPC;
            sIouD = dt.muxIorD.Set;
            sLerMem = dt.memory.SetReadMem;
            sEscMem = dt.memory.SetWriteMem;
            sMemParaReg = dt.muxMemtoReg.Set;
            sIREsc = dt.instrReg.SetWriteIR;
            sFontePC = dt.muxPCSrc.Set;
            sULAOp = dt.aluCtrl.sAluOP;
            sULAFonteA = dt.muxAluSrcA.Set;
            sULAFonteB = dt.muxAluSrcB.Set;
            sEscReg = dt.registers.SetRegWrite;
            sRegDst = dt.muxRegDest.Set;
            sBeqOrBne = dt.muxAluZero.Set;

            rstIouD = dt.muxIorD.Reset;
            rstMemParaReg = dt.muxMemtoReg.Reset;
            rstFontePC = dt.muxPCSrc.Reset;
            rstBeqOrBne = dt.muxAluZero.Reset;
            rstULAFonteA = dt.muxAluSrcA.Reset;
            rstULAFonteB = dt.muxAluSrcB.Reset;
            rstRegDst = dt.muxRegDest.Reset;

            dt.beqbne = false;
            has4cicle = false;
            has5cicle = true;

            this.Start();
        }

        /// <summary>
        /// Loops receiving user commands
        /// </summary>
        private void Start()
        {
        defineExec:
            Console.WriteLine("Deseja que a execucao seja por ciclos, por instrucoes, ou que execute o codigo todo de uma vez?");
            Console.WriteLine("Pressione C para ciclos, I para instrucoes ou T para tudo");
            ConsoleKey choice = Console.ReadKey(true).Key;

            switch (choice)
            {
                case ConsoleKey.C:
                    this.CicleExecution();
                    break;
                case ConsoleKey.I:
                    this.InstructionExecution();
                    break;
                case ConsoleKey.T:
                    this.AllExecution();
                    break;
                default:
                    Console.WriteLine("Resposta invalida...");
                    Console.ReadLine();
                    goto defineExec;
            }
        }

        /// <summary>
        /// Executes a single cicle and waits for the user to tell to continue
        /// </summary>
        private void CicleExecution()
        {
            try
            {
                while (true) //fica em loop ate PC jogar a exceção de fim de execução
                {
                    Console.Write("Pressione Enter para iniciar o ciclo!");
                    WaitToContinue(ConsoleKey.Enter);
                    this.ResetSignals();
                    this.ExecuteFstCicle();

                    Console.Write("\n\n\n\n\n\nPressione Enter para avançar ao próximo ciclo!");
                    WaitToContinue(ConsoleKey.Enter);
                    this.ResetSignals();
                    this.ExecuteSndCicle();
                    Console.Write("\n\n\n\n\n\nPressione Enter para avançar ao próximo ciclo!");
                    WaitToContinue(ConsoleKey.Enter);
                    this.ResetSignals();
                    this.Execute3Cicle();

                    if (has4cicle)
                    {
                        Console.Write("\n\n\n\n\n\nPressione Enter para avançar ao próximo ciclo!");
                        WaitToContinue(ConsoleKey.Enter);
                        this.ResetSignals();
                        this.Execute4Cicle();
                    }
                    if (has5cicle)
                    {
                        Console.Write("\n\n\n\n\n\nPressione Enter para avançar ao próximo ciclo!");
                        WaitToContinue(ConsoleKey.Enter);
                        this.ResetSignals();
                        this.Execute5Cicle();
                    }
                    Console.WriteLine("\n\n\n\n\n\n");
                }

            }
            catch (ExecutionOverException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Deseja consultar o bloco de registradores e de memória? s/n");
                char resp = (char)Console.Read();
                if (resp == 's')
                {
                    dt.registers.PrintRegisters();
                    dt.memory.PrintMemory();
                }
            }
        }
        private void InstructionExecution()
        {
            try
            {
                while (true) //fica em loop ate PC jogar a exceção de fim de execução
                {
                    Console.Write("\n\n\n\nPressione Enter para iniciar a instrução");
                    WaitToContinue(ConsoleKey.Enter);
                    this.ResetSignals();
                    this.ExecuteFstCicle();

                    Console.WriteLine("\n");
                    this.ResetSignals();
                    this.ExecuteSndCicle();

                    Console.WriteLine("\n");
                    this.ResetSignals();
                    this.Execute3Cicle();

                    if (has4cicle)
                    {
                        Console.WriteLine("\n");
                        this.ResetSignals();
                        this.Execute4Cicle();
                    }
                    if (has5cicle)
                    {
                        Console.WriteLine("\n");
                        this.ResetSignals();
                        this.Execute5Cicle();
                    }

                }

            }
            catch (ExecutionOverException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Deseja consultar o bloco de registradores e de memória? s/n");
                char resp = (char)Console.Read();
                if (resp == 's')
                {
                    dt.registers.PrintRegisters();
                    dt.memory.PrintMemory();
                }
            }
        }
        private void AllExecution()
        {
            try
            {
                while (true) //fica em loop ate PC jogar a exceção de fim de execução
                {
                    this.ResetSignals();
                    this.ExecuteFstCicle();

                    Console.WriteLine("\n");
                    this.ResetSignals();
                    this.ExecuteSndCicle();

                    Console.WriteLine("\n");
                    this.ResetSignals();
                    this.Execute3Cicle();

                    if (has4cicle)
                    {
                        Console.WriteLine("\n");
                        this.ResetSignals();
                        this.Execute4Cicle();
                    }
                    if (has5cicle)
                    {
                        Console.WriteLine("\n");
                        this.ResetSignals();
                        this.Execute5Cicle();
                    }
                }
            }
            catch (ExecutionOverException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Deseja consultar o bloco de registradores e de memória? s/n");
                char resp = (char)Console.Read();
                if (resp == 's')
                {
                    Console.WriteLine("Registradores:");
                    dt.registers.PrintRegisters();
                    Console.WriteLine("Memória:");
                    dt.memory.PrintMemory();
                }
            }
        }
        /// <summary>
        /// Executa o primeiro ciclo de uma instrucao
        /// </summary>
        private void ExecuteFstCicle()
        {
            Console.WriteLine("\nCiclo 1");
            Console.WriteLine("Sinais do Bloco de Controle:");
            //IouD = 0, LerMem = 1, ULAFonteA = 0, ULAFonteB = 01, ULAOp = 000, PCEsc = 1, IREsc = 1, FontePC = 0
            sLerMem(true);
            Tools.PrintControlSignal("LerMem", 1);
            sULAFonteA(0);
            Tools.PrintControlSignal("ULAFonteA", 0);
            sIouD(0);
            Tools.PrintControlSignal("IouD", 0);
            sIREsc(true);
            Tools.PrintControlSignal("IREsc", 1);
            sULAFonteB(1);
            Tools.PrintControlSignal("ULAFonteB", 1, 2);
            sULAOp(0b000);
            Tools.PrintControlSignal("ULAOp", 0, 3);
            sPCEsc(true);
            Tools.PrintControlSignal("PCEsc", 1, 1);
            sFontePC(0);
            Tools.PrintControlSignal("FontePC", 0, 1);

            dt.RunCicle();
        }

        private void ExecuteSndCicle()
        {
            Console.WriteLine("\nCiclo 2");
            Console.WriteLine("Sinais do Bloco de Controle:");
            sULAFonteA(0);
            Tools.PrintControlSignal("ULAFonteA", 0);
            sULAFonteB(0b11);
            Tools.PrintControlSignal("ULAFonteB", 3, 2);
            sULAOp(0b000); //add
            Tools.PrintControlSignal("ULAOp", 0, 3);

            dt.RunCicle();
            this.funct = Convert.ToByte(dt.instrReg.Funct, 2);
            this.opCode = Convert.ToByte(dt.instrReg.OpCode, 2);
        }

        private void Execute3Cicle()
        {
            Console.WriteLine("\nCiclo 3");
            Console.WriteLine("Sinais do Bloco de Controle:");
            switch (opCode)
            {
                case 0x23: //lw
                    Console.WriteLine("Instrução - LW");
                    has4cicle = true;
                    has5cicle = true;
                    sULAFonteA(1);
                    Tools.PrintControlSignal("ULAFonteA", 1);
                    sULAFonteB(2);
                    Tools.PrintControlSignal("ULAFonteB", 2, 2);
                    sULAOp(0b000);
                    Tools.PrintControlSignal("ULAOp", 0, 3);
                    break;
                case 0x2B: //sw
                    Console.WriteLine("Instrução - SW");
                    has4cicle = true;
                    has5cicle = false;
                    sULAFonteA(1);
                    Tools.PrintControlSignal("ULAFonteA", 1);
                    sULAFonteB(2);
                    Tools.PrintControlSignal("ULAFonteB", 2, 2);
                    sULAOp(0b000);
                    Tools.PrintControlSignal("ULAOp", 0, 3);
                    break;
                case 0x0: //Tipo R
                    Console.WriteLine("Instrução Tipo R");
                    Tools.TellInstructionR(this.funct);
                    has4cicle = true;
                    has5cicle = false;
                    if (TestaShift())//se for shift
                    {

                        sULAFonteA(1); //RS e RT foram invertidos
                        Tools.PrintControlSignal("ULAFonteA", 1);
                        sULAFonteB(0b10);
                        Tools.PrintControlSignal("ULAFonteB", 0b10, 2);
                        sULAOp(0b010);
                        Tools.PrintControlSignal("ULAOp", 0b010, 3);
                    }
                    else //instrucao normal..
                    {
                        sULAFonteA(1);
                        Tools.PrintControlSignal("ULAFonteA", 1);
                        sULAFonteB(0B00);
                        Tools.PrintControlSignal("ULAFonteB", 0, 2);
                        sULAOp(0b010);
                        Tools.PrintControlSignal("ULAOp", 0b010, 3);
                    }
                    break;
                case 0x9: //addiu
                    has4cicle = true;
                    has5cicle = false;
                    Console.WriteLine("Instrução - ADDIU");
                    sULAFonteA(1);
                    Tools.PrintControlSignal("ULAFonteA", 1);
                    sULAFonteB(0b10);
                    Tools.PrintControlSignal("ULAFonteB", 0b10, 2);
                    sULAOp(0b000);
                    Tools.PrintControlSignal("ULAOp", 0b000, 3);
                    break;
                case 0xC: //andi
                    has4cicle = true;
                    has5cicle = false;
                    Console.WriteLine("Instrução - ANDI");
                    sULAFonteA(1);
                    Tools.PrintControlSignal("ULAFonteA", 1);
                    sULAFonteB(0b10);
                    Tools.PrintControlSignal("ULAFonteB", 0b10, 2);
                    sULAOp(0b011); //and
                    Tools.PrintControlSignal("ULAOp", 0b011, 3);
                    break;
                case 0xD: //ori
                    has4cicle = true;
                    has5cicle = false;
                    Console.WriteLine("Instrução - ORI");
                    sULAFonteA(1);
                    Tools.PrintControlSignal("ULAFonteA", 1);
                    sULAFonteB(0b10);
                    Tools.PrintControlSignal("ULAFonteB", 0b10, 2);
                    sULAOp(0b100); //or
                    Tools.PrintControlSignal("ULAOp", 0b100, 3);
                    break;
                case 0xF: //lui
                    has4cicle = true;
                    has5cicle = false;
                    Console.WriteLine("Instrução - LUI");
                    sULAFonteA(1);
                    Tools.PrintControlSignal("ULAFonteA", 1);
                    sULAFonteB(0b10);
                    Tools.PrintControlSignal("ULAFonteB", 0b10, 2);
                    sULAOp(0b101);  //lui
                    Tools.PrintControlSignal("ULAOp", 0b101, 3);
                    break;
                case 0x4: //beq
                    has4cicle = false;
                    has5cicle = false;
                    Console.WriteLine("Instrução - BEQ");
                    sPCEsc(false);
                    Tools.PrintControlSignal("PCEsc", 0);
                    sPCEscCond(true);
                    Tools.PrintControlSignal("PCEscCond", 1);
                    sFontePC(1);
                    Tools.PrintControlSignal("FontePC", 1);
                    sULAFonteA(1);
                    Tools.PrintControlSignal("ULAFonteA", 1);
                    sULAFonteB(0b00);
                    Tools.PrintControlSignal("ULAFonteB", 0b00, 2);
                    sULAOp(0b001);
                    Tools.PrintControlSignal("ULAOp", 0b001, 3);
                    sBeqOrBne(0);
                    Tools.PrintControlSignal("BeqOrBne", 0);
                    dt.beqbne = true;
                    break;
                case 0x5: //bne
                    has4cicle = false;
                    has5cicle = false;
                    Console.WriteLine("Instrução - BNE");
                    sPCEsc(false);
                    Tools.PrintControlSignal("PCEsc", 0);
                    sPCEscCond(true);
                    Tools.PrintControlSignal("PCEscCond", 1);
                    sFontePC(1);
                    Tools.PrintControlSignal("FontePC", 1);
                    sULAFonteA(1);
                    Tools.PrintControlSignal("ULAFonteA", 1);
                    sULAFonteB(0b00);
                    Tools.PrintControlSignal("ULAFonteB", 0b00, 2);
                    sULAOp(0b001);
                    Tools.PrintControlSignal("ULAOp", 0b001, 3);
                    sBeqOrBne(1);
                    Tools.PrintControlSignal("BeqOrBne", 1);
                    dt.beqbne = true;

                    break;
            }
            dt.RunCicle();
        }

        private void Execute4Cicle()
        {
            Console.WriteLine("\nCiclo 4");
            Console.WriteLine("Sinais do Bloco de Controle:");
            switch (opCode)
            {
                case 0x23:
                    sIouD(1);
                    Tools.PrintControlSignal("IouD", 1);
                    sLerMem(true);
                    Tools.PrintControlSignal("LerMem", 1);
                    break;
                case 0x2B:
                    sIouD(1);
                    Tools.PrintControlSignal("IouD", 1);
                    sEscMem(true);
                    Tools.PrintControlSignal("EscMem", 1);
                    break;
                case 0x0:
                    sEscReg(true);
                    Tools.PrintControlSignal("EscReg", 1);
                    sMemParaReg(0);
                    Tools.PrintControlSignal("MemParaReg", 0);
                    sRegDst(1);
                    Tools.PrintControlSignal("RegDst", 1);
                    break;
                case 0x9: //addiu
                case 0xC: //andi
                case 0xD: //ori
                case 0xF: //lui
                    sEscReg(true);
                    Tools.PrintControlSignal("EscReg", 1);
                    sMemParaReg(0);
                    Tools.PrintControlSignal("MemParaReg", 0);
                    sRegDst(0);
                    Tools.PrintControlSignal("RegDst", 0);
                    break;
            }
            dt.RunCicle();
        }

        private void Execute5Cicle()//lw
        {
            Console.WriteLine("\nCiclo 5");
            Console.WriteLine("Sinais do Bloco de Controle:");
            sMemParaReg(1);
            Tools.PrintControlSignal("MemParaReg", 1);
            sRegDst(0);
            Tools.PrintControlSignal("RegDst", 0);
            sEscReg(true);
            Tools.PrintControlSignal("EscReg", 1);
            dt.RunCicle();
        }

        private bool TestaShift()
        {
            if (this.funct == 0x0 ||
                 this.funct == 0x2) //se for shift
                return true;
            return false;
        }
        /// <summary>
        /// resets all signals coming out of control block
        /// </summary>
        private void ResetSignals()
        {
            sLerMem(false);
            sIREsc(false);
            sPCEsc(false);
            sEscMem(false);
            sEscReg(false);
            dt.beqbne = false;
            rstBeqOrBne();
            rstFontePC();
            rstIouD();
            rstMemParaReg();
            rstRegDst();
            rstULAFonteA();
            rstULAFonteB();
            dt.beqbne = false;
        }
        /// <summary>
        /// Codigo auxiliar para aguardar para continuar execucao
        /// </summary>
        /// <param name="key"></param>
        private void WaitToContinue(ConsoleKey key)
        {
            while (Console.ReadKey(true).Key != key) { }
        }

        //metodo para zerar todas saidas para iniciar proxima instrucao

    }
}
