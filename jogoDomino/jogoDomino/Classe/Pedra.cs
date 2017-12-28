using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoDomino.Classe
{
    class Pedra
    {
        public int[] lado;
        public int pontos {get; set;}

        public Pedra(){
            lado = new int[2];
            pontos = 0;
        }

        public Pedra(int a, int b){
            lado = new int[2];
            lado[0] = a;
            lado[1] = b;
            pontos = a+b;
        }

    }
}
