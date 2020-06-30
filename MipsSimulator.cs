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
                    file = Console.ReadLine();
                }

                var lines = RotinaLeituraConsole(file);

                ControlBlock bloco_controle = new ControlBlock(lines);

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static string[] RotinaLeituraConsole(string file)
        {
            var fileRead = false;
            string[] lines;
            while (!fileRead)
            {
                try
                {

                    lines = File.ReadAllLines(file);

                    return lines;
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Digite o nome correto do arquivo com seu caminho completo");
                    file = Console.ReadLine();
                }
            }
            return new string[0];
        }

    }
}
