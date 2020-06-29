using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace MipsSimulator
{
    public partial class MipsMainScreen : Form
    {
        public MipsMainScreen()
        {
            InitializeComponent();
        }

        private void Tests()
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
            addLog(tstbin);

            uint valor2 = Convert.ToUInt32(tstbin, 2);
            addLog(Convert.ToString(valor2));

        }

        /// <summary>
        /// Writes the text in a new Line of the Logs textbox
        /// </summary>
        /// <param name="log"></param>
        private void addLog(String log)
        {
            executionLogs.AppendText("\n" + log);
        }

        private void TestBtnClick(object sender, EventArgs e)
        {
            this.Tests();
        }
    }
}
