using System;
using System.Collections.Generic;
using System.Text;

namespace MipsSimulator.Processor
{
    /// <summary>
    /// Classe contendo o valor e atributos de PC
    /// </summary>
    public class PC
    {
        public Int32 _pc = Tools.defaultIniPC;
        private  Boolean _writePC = false;
        private  Boolean _writeCondPC = false;
        private UInt32 _endExecution;

        public PC(UInt32 endExecution)
        {
            _endExecution = endExecution;
        }

        public Int32 GetPCValue() => this. _pc;

        /// <summary>
        /// Sinal enviado pelo Bloco de Controle PCEsc
        /// </summary>
        public void SetWritePC(bool value) => _writePC = value;

        /// <summary>
        /// Sinal enviado pelo Bloco de Controle PCEscCond
        /// </summary>
        public void SetWriteCondPC(bool value) => _writeCondPC = value;

        /// <summary>
        /// Method <c>ChangePCValue</c> foi criado para trocar o valor do PC por meio de função
        ///  se writePC estiver habilitado, salvara o valor novo em PC
        /// </summary>
        /// <param name="posPC"> Next PC Value to be used</param>
        /// <param name="bitZero">bit zero da ula</param>
        public void ChangePCValue(Int32 posPC, bool bitZero)
        {
            if (_writePC || (_writeCondPC && bitZero))
            {
                _pc = posPC;
                
                Tools.Print( "PC", "0x" + Convert.ToString(_pc, 16).PadLeft(8,'0'));

                if (_pc == _endExecution + 4)
                {
                    throw new ExecutionOverException("Fim da Execução!");
                }
            }
        }
    }
    /// <summary>
    /// Classe normal feita para iniciar os registradores A e B e para o MDR
    /// </summary>
    public class NormalReg
    {
        private Int32 _regValue;
        private string name;
        public NormalReg(string n)
        {
            name = n;
        }
        /// <summary>
        /// este getter e para uso do delegate dos mux
        /// </summary>
        /// <returns>valor do registrador</returns>
        public Int32 GetRegVal() => _regValue;

        /// <summary>
        /// Propriedades do registrador (get e set)
        /// </summary>
        public Int32 Reg
        {
            set
            {
                _regValue = value;
                Tools.Print(name, "0x" + Convert.ToString(_regValue, 16).PadLeft(8,'0'));
            }
            get
            {
                return _regValue;
            }
        }
    }

    /// <summary>
    /// classe representando o registrador de instrucao
    /// </summary>
    public class IR
    {
        //full mips instruction
        private String _fullInstruction;
        private Boolean _writeIR;

        //Receives the UnsignedInt32 instruction value from Memory and converts it to a bit string
        //then fill with zeros on the left
        //the string indexes are the reverse from the  normal indexes
        public Int32 Instr
        {
            set
            {
                if (_writeIR)
                {
                    Tools.Print("instr", "0x" + Convert.ToString(value, 16).PadLeft(8, '0'));
                    this._fullInstruction = Convert.ToString(value, 2).PadLeft(32, '0');
                }
            }
        }

        /// <summary>
        /// signal writeIR for the BC
        /// </summary>
        public void SetWriteIR(bool value) => this._writeIR = value;

        //These getters are for the delegate on the mux
        public Int32 GetRt() => this.Rt;
        public Int32 GetRd() => this.Rd;


        public Int32 GetSignalExtendedImmediate() => SignalExtendedImeddiate;

        //Shift 2 on signal extended immediate is implemented here
        public Int32 GetSignalExtendedShift2Immediate() => SignalExtendedImeddiate << 2;


        /// <summary>
        /// Getters of the string parts of the instruction
        /// </summary>
        public String OpCode => this._fullInstruction.Substring(0, 6);
        public Int32 Rs => Convert.ToInt32(this._fullInstruction.Substring(6, 5),2);
        public Int32 Rt => Convert.ToInt32(this._fullInstruction.Substring(11, 5),2);
        public Int32 Rd => Convert.ToInt32(this._fullInstruction.Substring(16, 5),2);
        public Int16 Immediate => Convert.ToInt16(this._fullInstruction.Substring(16, 16),2);
        /// <summary>
        /// Signal extender is implemented here.
        /// </summary>
        public Int32 SignalExtendedImeddiate => Convert.ToInt32(Immediate);
        /// <summary>
        /// Signal Extender and Shift left 2.
        /// </summary>
        public String Funct => this._fullInstruction.Substring(26, 6);
    }
}
