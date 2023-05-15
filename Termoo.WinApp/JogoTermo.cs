using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoo.WinApp {
    public class JogoTermo {
        public string palpite;
         string palavraSecreta = ObterPalavraSecreta();
        
        

        internal int AnalisarPalpite(string text,int posicao) {
            char letra = text[0];
            if (palavraSecreta.Contains(letra) && palavraSecreta[posicao] == letra) return 2;
            else if (palavraSecreta.Contains(letra) && palavraSecreta[posicao] != letra) return 1;
            else return 0;
        }

        protected static string ObterPalavraSecreta() {

            string caminho = @"C:\Users\gabri\Desktop\Academia do Progamador\Termoo.WinApp\Termoo.WinApp\Palavras.txt";
            string arquivo = File.ReadAllText(caminho);
            string[] palavras = arquivo.Split("\r\n");
           
            int indiceAleatorio = new Random().Next(palavras.Length);

            return palavras[indiceAleatorio];
        }

        internal bool VerificarVitoria(char[] palpiteAnalise) {
            if (palpiteAnalise[0] == palavraSecreta[0] &&
                palpiteAnalise[1] == palavraSecreta[1] &&
                palpiteAnalise[2] == palavraSecreta[2] &&
                palpiteAnalise[3] == palavraSecreta[3] &&
                palpiteAnalise[4] == palavraSecreta[4]) return true;
            else return false;
        }

        public bool VerificarSePalavraExiste(char[] letras) {
            string caminho = @"C:\Users\gabri\Desktop\Academia do Progamador\Termoo.WinApp\Termoo.WinApp\Palavras.txt";
            string arquivo = File.ReadAllText(caminho);
            string[] palavras = arquivo.Split("\r\n");

            string palavra = new string(letras);

            if (palavras.Contains(palavra)) return true; else return false;

        }

        

        


    }
}
