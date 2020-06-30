/**
 * Projeto feito por Marcelo Heredia e Pedro Castro 
 * Disciplina Organizacao e Arquitetura de Computadores 1
 * */
using MipsSimulator;
using MipsSimulator.Processor;
using System;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;

namespace SimuladorMIPS
{
    class MipsSimulator
    {
        static void Main(string[] args)
        {
            try
            {

                //ler arquivo .mips
                var separator = Path.DirectorySeparatorChar;
                string file;
                if (args.Length > 0)
                {
                    file = args[0];
                }
                else
                {
                    Console.WriteLine("Digite o arquivo junto com o caminho completo: ");
                    //file = Console.ReadLine();
                    file = String.Format("C:{0}libs{0}school{0}repo{0}teste.mips",separator);
                }

                ControlBlock bloco_controle = new ControlBlock(file);

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
   
    }
}
