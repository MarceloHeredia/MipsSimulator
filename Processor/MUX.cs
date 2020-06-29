using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MipsSimulator.Processor
{
    public class MUX
    {
        private Tools.DelegateMUX[] mxEntries;
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
            mxEntries = new Tools.DelegateMUX[size];
        }

        /// <summary>
        /// The control block will use this setter to select wich value must leave the MUX
        /// </summary>
        public void Set(int value) => this.selector = value;

        //tells the datablock if the selector is set by the control block
        public Boolean isSet
        {
            get
            {
                if (this.selector != null)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Resets the selector and values of this MUX
        /// </summary>
        public void Reset() => this.selector = null;

        /// <summary>
        /// Returns the value selected from the MUX by the control block
        /// WARNING ----- It will cause an exception if the selector isn't initialized 
        /// </summary>
        public Int32 Value => mxEntries[selector.Value]();

        /// <summary>
        /// Place the delegate on the position of index
        /// </summary>
        /// <param name="index">position to put the delegate</param>
        /// <param name="mx">declared delegate</param>
        public void PlaceEntry(Int32 index, Tools.DelegateMUX mx) => mxEntries[index] = mx;
    }
}
