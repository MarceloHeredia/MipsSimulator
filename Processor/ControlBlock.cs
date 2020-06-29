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

        #region signal delegates
        Tools.DelegateBCBool sPCEsc = PC.SetWritePC;
        Tools.DelegateBCBool sPCEscCond = PC.SetWriteCondPC;
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
        Tools.DelegateBCInt32 sBeqOrBne; //PCSrc
        #endregion

        public ControlBlock(String file)
        {
            dt = new DataBlock(file);
            sIouD = dt.muxIorD.Set;
            sLerMem = dt.memory.SetReadMem;
            sEscMem = dt.memory.SetWriteMem;
            sMemParaReg = dt.muxMemtoReg.Set;
            sIREsc = dt.instrReg.SetWriteIR;
            sFontePC = dt.muxPCSrc.Set;
            sULAOp = dt.aluCtrl.sAluOP;
            sULAFonteA = dt.muxAluSrcA.Set;
            sULAFonteB = dt.muxAluSrcB.Set;
            sEscReg = dt.bRegs.SetRegWrite;
            sRegDst = dt.muxRegDest.Set;

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
                this.ExecuteFstCicle();
                Console.Write("Pressione Enter para avançar ao próximo ciclo!");
                WaitToContinue(ConsoleKey.Enter);
                //this.ExecuteSndCicle();

            }
            catch (ExecutionOverException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void InstructionExecution()
        {

        }
        private void AllExecution()
        {

        }
        /// <summary>
        /// Executa o primeiro ciclo de uma instrucao
        /// </summary>
        private void ExecuteFstCicle()
        {
            Console.WriteLine("Ciclo 1");
            Console.WriteLine("Sinais do Bloco de Controle:");
            //IouD = 0, LerMem = 1, ULAFonteA = 0, ULAFonteB = 01, ULAOp = 000, PCEsc = 1, IREsc = 1, FontePC = 0
            sLerMem(true);
            PrintControlSignal("LerMem", 1);
            sULAFonteA(0);
            PrintControlSignal("ULAFonteA", 0);
            sIouD(0);
            PrintControlSignal("IouD", 0);
            sIREsc(true);
            PrintControlSignal("IRESC", 1);
            sULAFonteB(1);
            PrintControlSignal("ULAFonteB", 1, 2);
            sULAOp(0b000);
            PrintControlSignal("ULAOp", 0, 3);
            sPCEsc(true);
            PrintControlSignal("PCEsc", 1, 1);
            sFontePC(0);
            PrintControlSignal("FontePC", 0, 1);

            //setando os nao necessarios em false pois o default dos valores eh 0
            //os mux serao deixados default a principio
            sEscMem(false);
            sEscReg(false);

            dt.RunCicle();
        }

        private void ExcecuteSndCicle()
        {
            Console.WriteLine("Ciclo 1");
            Console.WriteLine("Sinais do Bloco de Controle:");
            //IouD = 0, LerMem = 1, ULAFonteA = 0, ULAFonteB = 01, ULAOp = 000, PCEsc = 1, IREsc = 1, FontePC = 0
            sLerMem(true);
            PrintControlSignal("LerMem", 1);
            sULAFonteA(0);
            PrintControlSignal("ULAFonteA", 0);
            sIouD(0);
            PrintControlSignal("IouD", 0);
            sIREsc(true);
            PrintControlSignal("IRESC", 1);
            sULAFonteB(1);
            PrintControlSignal("ULAFonteB", 1, 2);
            sULAOp(0b000);
            PrintControlSignal("ULAOp", 0, 3);
            sPCEsc(true);
            PrintControlSignal("PCEsc", 1, 1);
            sFontePC(0);
            PrintControlSignal("FontePC", 0, 1);

            //setando os nao necessarios em false pois o default dos valores eh 0
            //os mux serao deixados default a principio
            sEscMem(false);
            sEscReg(false);

            dt.RunCicle();
        }

        /// <summary>
        /// Codigo auxiliar para aguardar para continuar execucao
        /// </summary>
        /// <param name="key"></param>
        private void WaitToContinue(ConsoleKey key)
        {
            while (Console.ReadKey(true).Key != key) { }
        }

        private void PrintControlSignal(string sname, Int32 value, int digitos = 1)
        {
            string strvalue = Convert.ToString(value, 2).PadLeft(digitos, '0');
            Console.WriteLine(String.Format("{0} -> {1}", sname.PadRight(10), strvalue));
        }
        //metodo para zerar todas saidas para iniciar proxima instrucao

    }
}
