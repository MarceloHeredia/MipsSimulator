using System;
using System.Collections.Generic;
using System.Text;

namespace MipsSimulator.BD
{
    class MUX
    {
        private Int32[] entries;
        private DelegateMUX data_out;

        public MUX(Int32 size, DelegateMUX data_out )
        {
            entries = new Int32[size];
            this.data_out = data_out;
        }

        /// <summary>
        /// Este metodo funciona da seguinte forma.
        /// Quando o MUX recebe o sinal de selecao do Bloco de Controle, ele instantaneamente devolve o valor referente por meio de um delegate
        /// Este delegate consiste em uma chamada de funcao, esta funcao eh uma funcao criada no Bloco de Dados que faz uma modificacao onde o MUX deve modificar
        /// Assim que for selecionado, o valor é setado no fio instantaneamente.
        /// </summary>
        /// <param name="selector">sinal de selecao vindo do BC</param>
        public void Select(Int32 selector)
        {
            data_out(10);
        }
    }
}
