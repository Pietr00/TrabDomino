using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoDomino.Classe
{
    class Jogo
    {
        public int[] mesa;
        private Pedra[] pedra;
        private List<Pedra> compras;
        public Jogador humano;
        public Jogador maquina;
        public Boolean jogada;
        public Boolean acabou;
        public string resultado;
        public int qAux=0;
        public int jogouUltimo=0;

        public Jogo() {
            humano = new Jogador();
            maquina = new Jogador();
            pedra = new Pedra[28];
            compras = new List<Pedra>();
            mesa = new int[2];
            acabou = false;
            resultado = "";
            gerarPedras();
            distribuirPedras();
            definirJogada();
        }

        public void gerarPedras(){
            int contador = 0;
            for(int i=0;i<=6;i++)
                for (int j=i;j<=6;j++)
                    pedra[contador++] = new Pedra(i, j);
        }

        public void distribuirPedras(){
            Random rnd = new Random();
            Pedra aux = new Pedra();
            for (int i = 0; i < 28; i++) {
                int r = rnd.Next(0, 28);
                aux = pedra[r];
                pedra[r] = pedra[i];
                pedra[i] = aux;
            }
            for (int i = 0; i < 14; i++) compras.Add(pedra[i]);
            for (int i = 14; i < 21; i++) humano.mao.Add(pedra[i]);
            for (int i = 21; i < 28; i++) maquina.mao.Add(pedra[i]);
        }

        public void definirJogada() {
            for (int i = 6; i >= 0; i--) {
                Pedra aux = new Pedra(i,i);
                for (int j = 0; j < 7; j++) 
                    if (humano.mao[j] == aux) {
                        mesa[0] = i;
                        mesa[1] = i;
                        humano.mao.Remove(humano.mao[j]);
                        jogada = false;
                        return ;
                    }
                for (int j = 0; j < 7; j++) 
                    if (maquina.mao[j] == aux) {
                        mesa[0] = i;
                        mesa[1] = i;
                        maquina.mao.Remove(maquina.mao[j]);
                        jogada = true;
                        return ;
                    }
            }
            int maior = -1;
            int usu = -1;
            int pos = -1;
            for (int i = 0; i < 7; i++) {
                if (humano.mao[i].pontos > maior) {
                    maior = humano.mao[i].pontos;
                    usu = 0;
                    pos = i;
                }
                if (maquina.mao[i].pontos > maior) {
                    maior = maquina.mao[i].pontos;
                    usu = 1;
                    pos = i;
                }
            }
            if (usu == 0) {
                mesa[0] = humano.mao[pos].lado[0];
                mesa[1] = humano.mao[pos].lado[1];
                humano.mao.Remove(humano.mao[pos]);
                jogada = false;
                return;
            }
            if (usu == 1) {
                mesa[0] = maquina.mao[pos].lado[0];
                mesa[1] = maquina.mao[pos].lado[1];
                maquina.mao.Remove(maquina.mao[pos]);
                jogada = true;
                return;
            }
        }

        public void Jogar(int I, int L) { // realizar uma jogada
            if (jogada) {
                if (humano.mao[I].lado[0] == mesa[L]) mesa[L] = humano.mao[I].lado[1];
                else mesa[L] = humano.mao[I].lado[0];
                humano.mao.Remove(humano.mao[I]);
                jogouUltimo = 0;

            } else {
                if (maquina.mao[I].lado[0] == mesa[L]) mesa[L] = maquina.mao[I].lado[1];
                else mesa[L] = maquina.mao[I].lado[0];
                maquina.mao.Remove(maquina.mao[I]);
                jogouUltimo = 1;
            }
            qAux = 0;
            jogada = !jogada;
        }
        public void Jogar(){ //faz a jogada do computador
            for (int i = 0; i < maquina.mao.Count(); i++) {
                if (maquina.mao[i].lado[0] == mesa[0] || maquina.mao[i].lado[1] == mesa[0]) {
                    Jogar(i, 0);
                    return;
                }
                if (maquina.mao[i].lado[0] == mesa[1] || maquina.mao[i].lado[1] == mesa[1]) {
                    Jogar(i, 1);
                    return;
                }
            }
        }

        public bool podeJogar() { //verificar se tem peça que pode ser baixada
            bool aux = false;
            if(jogada)
                for (int i = 0; i < humano.mao.Count(); i++) {
                    if (humano.mao[i].lado[0] == mesa[0] || humano.mao[i].lado[1] == mesa[0]) aux = true;
                    if (humano.mao[i].lado[0] == mesa[1] || humano.mao[i].lado[1] == mesa[1]) aux = true;
                }
            else for (int i = 0; i < maquina.mao.Count(); i++)
                {
                    if (maquina.mao[i].lado[0] == mesa[0] || maquina.mao[i].lado[1] == mesa[0]) aux = true;
                    if (maquina.mao[i].lado[0] == mesa[1] || maquina.mao[i].lado[1] == mesa[1]) aux = true;
                }

            return aux;
        }

        public bool comprar() { //comprar peça quando disponivel
            if (compras.Count() > 0) {
                if (jogada)humano.mao.Add(compras[0]);
                else maquina.mao.Add(compras[0]);

                compras.Remove(compras[0]);
                jogada = !jogada;
                qAux = 0;
                return true;
            }
            jogada = !jogada;
            return false;
        }

        public void pularVez() {
            jogada = !jogada;
            qAux++;
        }

        public void quemGanhou() {

            if (humano.mao.Count() == 0) {
                resultado = "Ganhou";
                acabou = true;
            }
            if (maquina.mao.Count() == 0) {
                resultado = "Perdeu";
                acabou = true;
            }
            if (qAux == 2) {
                acabou = true;
                if (humano.pontuacao() < maquina.pontuacao()) resultado = "Ganhou";
                else if (humano.pontuacao() > maquina.pontuacao()) resultado = "Perdeu";
                else if (humano.possui00()) resultado = "Ganhou";
                else if (maquina.possui00()) resultado = "Perdeu";
                else if (jogouUltimo == 0) resultado = "Perdeu";
                else resultado = "Ganhou";
            }
        }

    }
}
