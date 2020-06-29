/**
 * Projeto feito por Marcelo Heredia e Pedro Castro 
 * Disciplina Organizacao e Arquitetura de Computadores 1
 * */
using MipsSimulator;
using MipsSimulator.Processor;
using System;
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



                //Tests();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
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
            //Int32 valor = 8;
            //var tstbin = Convert.ToString(valor, 2);
            //Console.WriteLine(tstbin);

            //var valor2 = Convert.ToInt32(tstbin, 2);
            //Console.WriteLine(valor2);




            //var srl = "0x0019c842";
            //var sll = "0x0019c880";

            //var srlb = "00000000000110011100100001000010";
            //var srlbc = "00000011001000001100100001000010";
            //var sllb = "00000000000110011100100010000000";
            //var sllbc = "00000011001000001100100010000000";

            //Console.WriteLine(DataBlock.RepairShifts(sll, sllb, sllbc));



            ////eh assim que faz o immediato ficar negativo na conversao!
            //string teste = "1011111111111110";
            //Console.WriteLine(Convert.ToInt32(Convert.ToInt16(teste,2)));


            //UInt32 x = 268501004;
            //Console.WriteLine(Convert.ToString(Convert.ToInt32(x),2).PadLeft(32,'0'));

            //Int32 testeShift = Convert.ToInt32("00000000000000001111111111111111",2);
            //Console.WriteLine(testeShift);

            //var b = testeShift << 16;

            //Console.WriteLine(Convert.ToString(b,2).PadLeft(32,'0'));
            //LUI EH ASSIM


            //var zero = 0;

            //var notzero = Convert.ToInt32(!Convert.ToBoolean(zero));

            //Console.WriteLine(zero);
            //Console.WriteLine(notzero);


            var x = -5;
            Console.WriteLine("0x"+Convert.ToString(x,16).PadLeft(8,'0'));
        }

    }
}
