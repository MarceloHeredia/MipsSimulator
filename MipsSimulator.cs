/**
 * Projeto feito por Marcelo Heredia e Pedro Castro 
 * Disciplina Organizacao e Arquitetura de Computadores 1
 * */
using System;

namespace SimuladorMIPS
{
    class MipsSimulator
    {
        static void Main(string[] args)
        {
            var x = 0x40404004;
            Int32 y = 0b111;
            Console.WriteLine(y);

            int a = 10;

            var bin = Convert.ToString(a, 2);
            if (a > 0) { bin = bin.PadLeft(32, '0'); }


            Console.WriteLine(bin);

            int b = Convert.ToInt32(bin, 2);

            Console.WriteLine(b);


            Console.ReadKey();
        }
    }
}
