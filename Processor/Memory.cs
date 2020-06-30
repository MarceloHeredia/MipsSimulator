using System;
using System.Collections.Generic;

namespace MipsSimulator.Processor
{
    public class Memory
    {
        //implementacao com Dictionary[endereco][conteudo] para uma coleta direta dos dados.
        private Dictionary<UInt32, Int32> _memory;
        //variavel para acesso a memoria
        private UInt32 _adress;
        //signals from CB
        private Boolean _readMem;
        private Boolean _writeMem;

        /// <summary>
        /// Constructor for Memory class, initializes an empty memory
        /// </summary>
        public Memory()
        {
            _memory = new Dictionary<uint, int>();
            this._readMem = false;
            this._writeMem = false;
        }

        /// <summary>
        /// sets memory adress pointer
        /// </summary>
        public UInt32 Adress
        {
            set
            {
                this._adress = value;
                //Print the new adress pointer on the memory
                Tools.Print("Endereço", "0x" + Convert.ToString(_adress,16).PadLeft(8, '0'));
            }
            get
            {
                return this._adress;
            }
        }

        /// <summary>
        /// Tries to read Memory
        /// If the signal _readMem is not true will throw an exception
        /// </summary>
        public Int32 MemData => Convert.ToInt32($"{(_readMem ? _memory[_adress] : new Dictionary<UInt32, Int32>()[0xfffffff])}");

        /// <summary>
        /// Writes a value in memory on the Adress position
        /// </summary>
        /// <param name="value">value to be written</param>
        /// <returns></returns>
        public void WriteInMem(Int32 value)
        {
            if (_writeMem)
            {
                _memory[_adress] = value;
                Tools.Print("Escrito em memoria", "0x" + Convert.ToString(value, 16).PadLeft(8,'0'));
            }
        }


        /// <summary>
        /// writeMem Signal from BC
        /// </summary>
        public void SetWriteMem(bool value) => this._writeMem = value;

        /// <summary>
        /// readMem signal from BC
        /// </summary>
        public void SetReadMem(bool value) => this._readMem = value;

        public bool ReadAllowed => this._readMem;

        /// <summary>
        /// Writes memory from the file reader (only used to fill memory with instructions and .data at the start of the execution)
        /// </summary>
        public void WMFFR(UInt32 adr, Int32 data)
        {
            this._memory[adr] = data;
        }
    }
}
