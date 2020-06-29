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
            Console.WriteLine("{0} <- {1}", name.PadRight(10), value);
        }

        public static void IgnoreException(Action act)
        {
            try
            {
                act.Invoke();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }

    public class ExecutionOverException : Exception
    {
        public ExecutionOverException(string message) : base(message) { }
    }

}
