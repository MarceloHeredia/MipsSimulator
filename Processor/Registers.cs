using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace MipsSimulator.Processor
{
    public class Registers
    {
        // banco de registradores configurado em 32bits com sinal
        private Int32[] _registers;
        // sinal RegWrite vindo do BC configurado como booleano, 1 = true, 0 = false
        private Boolean _regWrite;

        //os valores a seguir sao a POSICAO do vetor de registradores de onde sairao os valores para A e B
        //registrador 1 chamado pelo registrador de instrucao e a ser pego para salvar no registrador A
        private Int32 _reg1;
        //registrador 2 , mesma funcionalidade do 1 porem devera salvo em registrador B
        private Int32 _reg2;
        //registrador que podera receber escrita em memoria
        private Int32 _regTarget;

        /// <summary>
        /// Register Constructor
        /// Initializes an Register object with a 32 positions array of Int32 which will be the 32 registers of general usage.
        /// regWrite flag will be set to FALSE
        /// </summary>
        public Registers()
        {
            this._registers = new Int32[32];                                            //inicializa o vetor de 32 posicoes
            Array.Fill<Int32>(this._registers, 0);                                     //inicializa todos registradores com valor 0
            _registers[29] = 0x7fffeffc;                                                //seta o valor correto de $sp
            this._regWrite = false;                                                          //Inicializa regWrite em 0
        }

        /// <summary>
        /// RegWrite setter must be set by te Control Block
        /// </summary>
        public void SetRegWrite(bool value) => this._regWrite = value;

        /// <summary>
        /// Set the number of the register that you want to use
        /// Get the value of the register set
        /// WARNING - trying to get the value without setting the register you want to get from can causa an exception or return the register on the 0 position
        /// </summary>
        public Int32 Reg1
        {
            set
            {
                this._reg1 = value;
            }
            get
            {
                return _registers[_reg1];
            }
        }
        /// <summary>
        /// Set the number of the register that you want to use
        /// Get the value of the register set
        /// WARNING - trying to get the value without setting the register you want to get from will cause an EXCEPTION
        /// </summary>
        public Int32 Reg2
        {
            set
            {
                this._reg2 = value;
            }
            get
            {
                return _registers[_reg2];
            }
        }

        /// <summary>
        /// Set the number of the register you want to write to
        /// </summary>
        public Int32 RegTarget
        {
            set
            {
                this._regTarget = value;

            }
        }

        /// <summary>
        /// Write the data on the register target
        /// Only works if regWrite is true 
        /// </summary>
        public Int32 Write
        {
            set
            {
                if (this._regWrite)
                {
                    _registers[_regTarget] = value;
                    //Print the change on console.
                    Tools.Print(String.Format("${0}", _regTarget), "0x"+Convert.ToString(value,16).PadLeft(8,'0'));
                }
            }
        }

    }
}
