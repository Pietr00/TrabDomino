using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoDomino.Classe
{
    class Jogador
    {
        public List<Pedra> mao { get; set; }

        public Jogador(){
            mao = new List<Pedra>();
        }

        public int pontuacao() {
            int aux = 0;
            for (int i = 0; i < mao.Count(); i++) {
                aux += mao[i].pontos;
            }
            return aux;
        }

        public bool possui00() {
            Pedra aux = new Pedra(0, 0);
            for (int i = 0; i < mao.Count(); i++){
                if (mao[i] == aux) return true;
            }
            return false;
        }
    }
}
