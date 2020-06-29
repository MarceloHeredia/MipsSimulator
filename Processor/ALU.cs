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
                    _res = _entry1 + _entry2;
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
                    _res = _entry1 << _entry2;
                    break;
                case Operation.srl:
                    _res = _entry1 >> _entry2;
                    break;
                case Operation.lui:
                    _res = _entry2 << 16;
                    break;


                default: break;
            }
            _zero = _res == 0 ? 1 : 0;
        }

        /// <summary>
        /// Seta a operacao a ser realizada pela ula
        /// </summary>
        public Operation Op
        {
            set
            {
                _op = value;
            }
        }

        /// <summary>
        /// Seta a entrada 1 da ula
        /// </summary>
        public Int32 Entrada1
        {
            set
            {
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
