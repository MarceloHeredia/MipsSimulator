using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace MipsSimulator.BD
{
    class Registers
    {
        // banco de registradores configurado em 32bits com sinal
        private Int32[] registers; 
        // sinal RegWrite vindo do BC configurado como booleano, 1 = true, 0 = false
        private Boolean regWrite;

        //os valores a seguir sao a POSICAO do vetor de registradores de onde sairao os valores para A e B
        //registrador 1 chamado pelo registrador de instrucao e a ser pego para salvar no registrador A
        private Int32 reg1;
        //registrador 2 , mesma funcionalidade do 1 porem devera salvo em registrador B
        private Int32 reg2;


        /// <summary>
        /// Register Constructor
        /// Initializes an Register object with a 32 positions array of Int32 which will be the 32 registers of general usage.
        /// regWrite flag will be set to FALSE
        /// </summary>
        public Registers()
        {
            this.registers = new Int32[32];                                            //inicializa o vetor de 32 posicoes
           // Array.Fill<Int32>(this.registers, 0);                                     //inicializa todos registradores com valor 0
            this.regWrite = false;                                                          //Inicializa regWrite em 0
        }
        
        /// <summary>
        /// RegWrite setter must be set by te Control Block
        /// </summary>
        public Boolean RegWrite
        {
            set
            {
                this.regWrite = value;
            }
        }

        public Int32 Reg1
        {
            set
            {
                
            }
        }

        public Int32 Reg2
        {
            set
            {

            }
        }
    }
}
