/**
 * Projeto feito por Marcelo Heredia e Pedro Castro 
 * Disciplina Organizacao e Arquitetura de Computadores 1
 * */
using MipsSimulator;
using MipsSimulator.BD;
using System;
using System.IO;

namespace SimuladorMIPS
{
    class MipsSimulator
    {
        static void Main(string[] args)
        {
            //ler arquivo .mips




            /*
            //fazer devidos ajustes
            DataBlock proc_db = new DataBlock();

            proc_db.RunCicle();
            */
            Tests();
        }
        private static void Tests()
        {
            /*
            var x = 0x40404004;
            Int32 y = 0b111;
            Console.WriteLine(y);

            int a = 10;

            var bin = Convert.ToString(a, 2);
            if (a > 0) { bin = bin.PadLeft(32, '0'); }


            Console.WriteLine(bin);

            int b = Convert.ToInt32(bin, 2);

            //conversao de binarios complemento de 2

            a = 0b01011111010111100000111111110000;
            Console.WriteLine(Convert.ToString(a, 2).PadLeft(32, '0'));
            Console.WriteLine(a);
            a = a << 2;
            Console.WriteLine(Convert.ToString(a, 2));
            Console.WriteLine(a);


            //usar este metodo para .data com caracteres

            char testaConv = 'A';

            int testacv = Convert.ToInt32(testaConv);
            Console.WriteLine(testacv);

            testaConv = Convert.ToChar(testacv);
            Console.WriteLine(testaConv);
            */
            int valor = -8;
            var tstbin = Convert.ToString(valor, 2);
            Console.WriteLine(tstbin);

            uint valor2 = Convert.ToUInt32(tstbin, 2);
            Console.WriteLine(valor2);

        }
    }
}
