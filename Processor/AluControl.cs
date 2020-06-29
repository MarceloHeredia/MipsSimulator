using System;
using System.Collections.Generic;
using System.Text;

namespace MipsSimulator.Processor
{
    /// <summary>
    /// Alu Control Input
    /// </summary>
    public enum Operation
    {
        and, or, add, sub, slt, xor, sll, srl, lui
    }
    /// <summary>
    /// Controle de Operacao da UlA
    /// </summary>
    public class AluControl
    {

        private Operation controle;
        private byte aluOp;
        private string funct;

        /// <summary>
        /// Getter do da operacao da ula
        /// </summary>
        public Operation Op => controle;


        public Byte AluOp => aluOp;

        /// <summary>
        /// Setter que vem do BLoco de Controle 
        /// </summary>
        public void sAluOP(byte value) => this.aluOp = value;

        /// <summary>
        /// bits de funcao que vem do IR
        /// </summary>
        public string Funct
        {
            get
            {
                return funct;
            }
            set
            {
                this.funct = value;
            }
        }

        /// <summary>
        /// Select 
        /// </summary>
        public void SetAluOp()
        {
            switch (aluOp)
            {
                // lw or sw.
                case 0b000:
                    controle = Operation.add; //lw sw addiu
                    break;
                // beq 
                case 0b001:
                    controle = Operation.sub; //beq bne
                    break;
                // Tipo- R
                case 0b010:
                    byte byteFunct = Convert.ToByte(funct, 2);
                    switch (byteFunct)
                    {
                        case 0x26:
                            controle = Operation.xor;
                            break;
                        case 0x21: //addu
                            controle = Operation.add;
                            break;
                        case 0x24:
                            controle = Operation.and;
                            break;
                        case 0x2A:
                            controle = Operation.slt;
                            break;
                        case 0x0:
                            controle = Operation.sll;
                            break;
                        case 0x2:
                            controle = Operation.srl;
                            break;
                        default: break;
                    }
                    break;
                case 0b011:
                    controle = Operation.and; //andi
                    break;
                case 0b100:
                    controle = Operation.or; //ori
                    break;
                case 0b101:
                    controle = Operation.lui; //lui
                    break;
            }
        }
    }
}
