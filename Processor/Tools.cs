using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MipsSimulator.Processor
{

    /// <summary>
    /// Classe contendo constantes e valores GLOBAIS para o projeto.
    /// </summary>
    public static class Tools
    {
        public const Int32 defaultIniPC = 0x00400000;
        public const Int32 iniMemData = 0x10010000;            //verificar valor inicial da parte de dados da memoria para uso posterior
                                                               //delegate para enviar o método para o mux
        public delegate Int32 DelegateMUX();
        //delegates para setar os sinais a partir do Bloco de Controle.
        public delegate void DelegateBCBool(bool signal);
        public delegate void DelegateBCInt32(Int32 signal);
        public delegate void DelegateBCByte(Byte signal);
        public delegate void DelegateResetMux();

        public static void Print(String name, String value)
        {
            Console.WriteLine("{0} <- {1}", name.PadRight(20), value);
        }
        public static void PrintControlSignal(string sname, Int32 value, int digitos = 1)
        {
            string strvalue = Convert.ToString(value, 2).PadLeft(digitos, '0');
            Console.WriteLine(String.Format("{0} -> {1}", sname.PadRight(20), strvalue));
        }

        public static void IgnoreException(Action act)
        {
            try
            {
                act.Invoke();
            }
            catch { }
        }

        public enum RType
        {
            xor = 0x26,
            addu = 0x21,
            and = 0x24,
            sll = 0x0,
            srl = 0x2,
            slt = 0x2A
        };

        public static void TellInstructionR(byte funct)
        {
            Console.WriteLine("Instrução - "+Enum.GetName(typeof(RType), (RType)funct).ToUpper());
        }
    }

    public class ExecutionOverException : Exception
    {
        public ExecutionOverException(string message) : base(message) { }
    }

}
