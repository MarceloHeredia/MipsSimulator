using System;

namespace MipsSimulator.Processor
{
    public class ALU
    {

        private int _res, _entry1, _entry2;
        private int _zero;
        private Operation _op;

        /// <summary>
        /// executa operacao na ULA
        /// </summary>
        public void Operate()
        {
            switch (_op)
            {
                case Operation.and:
                    _res = _entry1 & _entry2;
                    break;
                case Operation.or:
                    _res = _entry1 | _entry2;
                    break;
                case Operation.add:
                    _res = _entry1 + _entry2;
                    break;
                case Operation.sub:
                    _res = _entry1 - _entry2;
                    break;
                case Operation.slt:
                    if (_entry1 < _entry2)
                    {
                        _res = 1;
                        break;
                    }
                    _res = 0;
                    break;
                case Operation.xor:
                    _res = _entry1 ^ _entry2;
                    break;
                case Operation.sll:
                    //Manipula os 32 bits para que fiquem apenas os 5 bits necessarios para execucao
                    _entry2 = _entry2 << 21;
                    _entry2 = _entry2 >> 27;
                    Tools.Print("ALU (shamt)", "0x" + Convert.ToString(_entry2, 16).PadLeft(8, '0'));
                    _res = _entry1 << _entry2;
                    break;
                case Operation.srl:
                    _entry2 = _entry2 << 21;
                    _entry2 = _entry2 >> 27;
                    Tools.Print("ALU (shamt)", "0x" + Convert.ToString(_entry2, 16).PadLeft(8, '0'));
                    _res = _entry1 >> _entry2;
                    break;
                case Operation.lui:
                    _res = _entry2 << 16;
                    break;


                default: break;
            }
            _zero = _res == 0 ? 1 : 0;
            Tools.Print("ALU zero", Convert.ToString(_zero));
        }

        /// <summary>
        /// Seta a operacao a ser realizada pela ula
        /// </summary>
        public Operation Op
        {
            set
            {
                _op = value;
                Tools.Print("ALU Operation",Enum.GetName(typeof(Operation), _op));
            }
        }

        /// <summary>
        /// Seta a entrada 1 da ula
        /// </summary>
        public Int32 Entrada1
        {
            set
            {
                Tools.Print("ALU operando 1", "0x" + Convert.ToString(value, 16).PadLeft(8, '0'));
                _entry1 = value;
            }
        }

        /// <summary>
        /// Seta a entrada 2 da ula
        /// </summary>
        public Int32 Entrada2
        {
            set
            {
                Tools.Print("ALU operando 2", "0x" + Convert.ToString(value, 16).PadLeft(8, '0'));
                _entry2 = value;
            }
        }

        /// <summary>
        /// Recebe o resultado da ula
        /// </summary>
        public Int32 Result => _res;

        public Int32 GetAluResult() => _res;
        public Int32 GetAluZero() => _zero;

        public Int32 GetReverseAluZero() => Convert.ToInt32(!Convert.ToBoolean(_zero));
    }
}
