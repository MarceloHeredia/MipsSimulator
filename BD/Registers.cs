using System;
using System.Collections.Generic;
using System.Text;

namespace MipsSimulator.BD
{
    class Registers
    {
        // banco de registradores configurado em 32bits com sinal
        private Int32[] registers; 
        // sinal RegWrite vindo do BC configurado como booleano, 1 = true, 0 = false
        private Boolean regWrite;

        public Registers()
        {
            this.registers = new Int32[32];                                            //inicializa o vetor de 32 posicoes
            Array.Fill<Int32>(this.registers, 0);                                     //inicializa todos registradores com valor 0
            this.regWrite = false;                                                          //Inicializa regWrite em 0
        }



    }
}
