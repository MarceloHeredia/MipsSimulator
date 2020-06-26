using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MipsSimulator.BD
{
    class MUX
    {
        private Int32[] entries;
        private Int32? selector;
        private Int32 size;

        /// <summary>
        /// MUX Constructor.
        /// Receives the size of the data array
        /// Its a MUX N to 1.
        /// </summary>
        /// <param name="size">size of the array of data</param>
        public MUX(Int32 size)
        {
            this.size = size;
            entries = new Int32[size];
        }

        /// <summary>
        /// The control block will use this setter to select wich value must leave the MUX
        /// </summary>
        public Int32 Set
        {
            set
            {
                this.selector = value;
            }
        }

        /// <summary>
        /// Resets the selector and values of this MUX
        /// </summary>
        public void Reset()
        {
            this.selector = null;
            entries = new Int32[size];
        }

        /// <summary>
        /// Returns the value selected from the MUX by the control block
        /// WARNING ----- It will cause an exception if the selector isn't initialized 
        /// </summary>
        public Int32 Value
        {
            get
            {
                return entries[selector.Value];
            }
        }
    }
}
