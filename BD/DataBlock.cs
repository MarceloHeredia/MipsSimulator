using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MipsSimulator.BD

{
    //delegate para enviar o método para o mux
    public delegate void DelegateMUX(Int32 value);

    class DataBlock
    {
        const Int32 defaultIniPC = 0x00400000;
        const Int32 iniMemData = 0;            //verificar valor inicial da parte de dados da memoria para uso posterior
        static Int32 _PC = defaultIniPC;     //PC esta na posicao inicial

        DelegateMUX changePC = ChangePCValue;
        //inicializar todos circuitos e fazer as respectivas conexoes de acordo 
        public DataBlock()
        {

        }

        public void RunCicle()
        {
            MUX muxAttPC = new MUX(3, changePC);
            muxAttPC.Select(10);
            Console.WriteLine(Convert.ToString(_PC,16));
        }

        /// <summary>
        /// Method <c>ChangePCValue</c> foi criado para trocar o valor do PC por meio de função
        ///  A necessidade deste metodo se da pelo fato de que, quando o MUX estiver com seus valores setados e ja tiver selecionado o valor de saida
        ///  A implementacao da saida do MUX e feita por um delegate que aponta para uma funcao no alvo, essa funcao seta o valor que deve ser modificado pelo MUX.
        /// </summary>
        /// <param name="newVal"> Next PC Value to be used</param>
        public static void ChangePCValue(Int32 newVal) => _PC = newVal;

    }
}
