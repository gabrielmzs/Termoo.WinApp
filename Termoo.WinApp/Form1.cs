﻿using System.Reflection.Metadata.Ecma335;

namespace Termoo.WinApp {
    public partial class Form1 : Form {

        private JogoTermo jogo;
        private int posicaoLetra = 0;
        private int posicaoLinha = 0;
        public char[] palpiteAnalise = new char[5];
        public Form1() {
            InitializeComponent();
            jogo = new JogoTermo();
            RegistrarEventos();
        }

        private void RegistrarEventos() {

            foreach (Button botao in teclado1.Controls) {
                botao.Click += InputarLetra;

            }
            foreach (Button botao in teclado2.Controls) {

                if (botao.Text != "⌫")
                    botao.Click += InputarLetra;

            }
            foreach (Button botao in teclado3.Controls) {

                if (botao.Text != "ENTER")
                    botao.Click += InputarLetra;

            }

            btnDel.Click += ExcluirLetra;

            btnEnter.Click += ConfirmarPalpite;

        }

        private void ConfirmarPalpite(object? sender, EventArgs e) {
            if(palpiteAnalise.Length == 5) { 

            TableLayoutPanel linha = SelecionarLinha(posicaoLinha);
            linha.Enabled = false;

                for (int i = 0; i < 5; i++) {
                    TextBox txtBox = SelecionarTxtBox(i, linha);
                    int resultado = jogo.AnalisarPalpite(txtBox.Text,i);
                    DarDicas(txtBox, resultado);
                }

                if (jogo.VerificarVitoria(palpiteAnalise)) {
                    MessageBox.Show("Você Venceu! Ok para jogar novamente.");
                    ReiniciarTabela();
                    jogo = new JogoTermo();
                }

               else {posicaoLinha++;
                    posicaoLetra = 0;
                }

                if(posicaoLinha > 5) {
                    MessageBox.Show("Você Perdeu! Ok para jogar novamente.");
                    ReiniciarTabela();
                    jogo = new JogoTermo();
                }
            }
        }

        private void DarDicas(TextBox txtBox, int resultado) {
            Button botao = SelecionarBotao(txtBox.Text);

            switch (resultado) {
                
                case 0: txtBox.BackColor = Color.Black;  //Preto = não tem a letra na palavara
                        botao.Enabled = false;
                        break; 
                    
                case 1: txtBox.BackColor = Color.FromArgb(211, 173, 105); //Laranja = Letra na posição errada
                        botao.BackColor = Color.FromArgb(211, 173, 105);
                        break;
                    
                case 2: txtBox.BackColor= Color.FromArgb(58, 163, 148);  //Verde = Letra na posição correta
                        botao.BackColor = Color.FromArgb(58, 163, 148);
                        
                        break;  


            }
        }

        private void ExcluirLetra(object? sender, EventArgs e) {
            textBox1.Text = "";
        }

        private void InputarLetra(object? sender, EventArgs e) {  //adiciona o text do btn ao txtbox com base na posicao, comeca em zero e vai somando

            if(posicaoLetra < 5) { 
            Button botaoClicado = (Button)sender;
            string palpite = Convert.ToString(botaoClicado.Text[0]);
            palpiteAnalise[posicaoLetra] = Convert.ToChar(palpite);
            TableLayoutPanel linha = SelecionarLinha(posicaoLinha);
            TextBox txtBox = SelecionarTxtBox(posicaoLetra,linha);
            txtBox.Text = palpite;

            posicaoLetra++;
            }

        }

        private TextBox SelecionarTxtBox(int tabIndex, TableLayoutPanel linha) {  //analisa todos os txtbox na linha, seleciona pelo tabIndex e retorna
            foreach (TextBox textBox in linha.Controls) {
                if (textBox.TabIndex == tabIndex) {
                    return textBox;
                }
            }

            return null;
        }

        private TableLayoutPanel SelecionarLinha(int tabIndex) {  //analisa todos os txtbox na linha, seleciona pelo tabIndex e retorna
            foreach (TableLayoutPanel linha in panel2.Controls) {
                if (linha.TabIndex == tabIndex) {
                    return linha;
                }
            }

            return null;
        }

        private Button SelecionarBotao(string letra) {  //analisa todos os botoes do teclado, seleciona pelo text e retorna

            foreach (Button b in teclado1.Controls) {
                if (b.Text == letra) {
                    return b;
                }
            }
            foreach (Button b in teclado2.Controls) {
                if (b.Text == letra) {
                    return b;
                }
            }
            foreach (Button b in teclado3.Controls) {
                if (b.Text == letra) {
                    return b;
                }
            }
            return null;

        }

        private void ReiniciarTabela() {
            foreach (TableLayoutPanel linha in panel2.Controls) {
                linha.Enabled = true;
                foreach (TextBox textBox in linha.Controls) {
                    textBox.BackColor = Color.FromArgb(64, 64, 64);
                    textBox.Text = "";
                }
            }

            foreach (Button b in teclado1.Controls) {
                b.Enabled = true;
                b.BackColor = Color.FromArgb(64, 64, 64);
            }
            foreach (Button b in teclado2.Controls) {
                b.Enabled = true;
                b.BackColor = Color.FromArgb(64, 64, 64);
            }
            foreach (Button b in teclado3.Controls) {
                b.Enabled = true;
                b.BackColor = Color.FromArgb(64, 64, 64);
            }

            posicaoLetra = 0;
            posicaoLinha = 0;


        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }
    }

}