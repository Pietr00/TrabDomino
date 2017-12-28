using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoDomino
{
    class Program
    {
        static void Main(string[] args)
        {
            Classe.Jogo jogo = new Classe.Jogo();

            int Ipeca=0, Lado=0;

            do {
                Console.Write("Mesa : \n"+jogo.mesa[0] + "---" + jogo.mesa[1] + "\n\n");
                if (jogo.jogada) {
                    if (jogo.podeJogar()) { // Cenario 1

                        for (int i = 0; i < jogo.humano.mao.Count; i++)
                            Console.Write(i + " ");
                        Console.WriteLine();
                        for (int i = 0; i < jogo.humano.mao.Count; i++)
                            Console.Write("--");
                        Console.WriteLine();
                        for (int i = 0; i < jogo.humano.mao.Count; i++)
                            Console.Write(jogo.humano.mao[i].lado[0] + " ");
                        Console.WriteLine();
                        for (int i = 0; i < jogo.humano.mao.Count; i++)
                            Console.Write(jogo.humano.mao[i].lado[1] + " ");
                        Console.WriteLine("\n");
                        Console.Write("Escreva o indice da peça\n");
                        Ipeca = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Escreva o lado que pretende coloca-la (0-esquerdo, 1-direito)\n");
                        Lado = Convert.ToInt32(Console.ReadLine());
                        jogo.Jogar(Ipeca, Lado);
                    } else {
                        if (jogo.comprar()) { // Cenario 2
                            Console.Write("\nVocê não possui peças que podem ser jogadas\nVocê comprou uma peça - Pressione para continuar\n");
                            Console.ReadKey();
                        } else { // Cenario 3
                            Console.Write("\nVocê não possui peças que podem ser jogadas\nNão há peças para serem compradas\nPulou sua rodada - Pressione para continuar\n");
                            Console.ReadKey();
                            jogo.pularVez();
                        }
                    }

                } else {
                    if (jogo.podeJogar()) { // Cenario 5.1
                        Console.Write("\nVez do computador - Pressione para continuar\n");
                        Console.ReadKey();
                        jogo.Jogar();
                    } else {
                        if (jogo.comprar()){ // Cenario 5.2
                            Console.Write("\nComputador não possui peças que podem ser jogadas\nEle comprou peça - Pressione para continuar\n");
                            Console.ReadKey();
                        }
                        else { // Cenario 7
                            Console.Write("\nComputador não possui peças que podem ser jogadas\nNão há peças para serem compradas\nPulou rodada do computador - Pressione para continuar\n");
                            Console.ReadKey();
                            jogo.pularVez();
                        }
                    }
                }

                jogo.quemGanhou();
                Console.Clear();
            } while (!jogo.acabou);

            Console.Write(jogo.resultado+"\n");

            Console.ReadKey();
        }
    }
}
