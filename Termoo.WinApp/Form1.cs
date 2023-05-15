using System.Reflection.Metadata.Ecma335;

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
            if (TodasLetrasImputadas() && jogo.VerificarSePalavraExiste(palpiteAnalise)) { // verificação para só enviar quando estiver a palavra completa

                TableLayoutPanel linha = SelecionarLinha(posicaoLinha);
                

                for (int i = 0; i < 5; i++) {  //passa para letra por letra para a classe do jogo analisar
                    TextBox txtBox = SelecionarTxtBox(i, linha);
                    int resultado = jogo.AnalisarPalpite(txtBox.Text, i);
                    DarDicas(txtBox, resultado);
                }

                AnalisarResultado();

            } else {
                MessageBox.Show("Palavra incompleta ou inexistente!");
            }
        }

        private bool TodasLetrasImputadas() {
            return palpiteAnalise[0] != '\0' && palpiteAnalise[1] != '\0' && palpiteAnalise[2] != '\0' && palpiteAnalise[3] != '\0' && palpiteAnalise[4] != '\0';
        }

        private void AnalisarResultado() {
            if (jogo.VerificarVitoria(palpiteAnalise)) NovoJogo("Você Venceu! Ok para jogar novamente.");
            else {
                posicaoLinha++;
                posicaoLetra = 0;
            }

            if (VerificarDerrota()) NovoJogo("Você Perdeu! Ok para jogar novamente.");
        }

        private bool VerificarDerrota() {
            return (posicaoLinha > 5);
        }

        private void NovoJogo(string mensagem) {
            MessageBox.Show(mensagem);
            ReiniciarTabela();
            jogo = new JogoTermo();
        }

        private void DarDicas(TextBox txtBox, int resultado) {

            Button botao = SelecionarBotao(txtBox.Text);

            switch (resultado) {
                
                case 0: txtBox.BackColor = Color.Black;  //Preto = não tem a letra na palavara
                        botao.BackColor = Color.Black;
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
            
            if(posicaoLetra > 0) {
            TableLayoutPanel linha = SelecionarLinha(posicaoLinha);
            posicaoLetra--;
            TextBox txtBox = SelecionarTxtBox(posicaoLetra, linha);
            txtBox.Text = "";
            }

        }

        private void InputarLetra(object? sender, EventArgs e) {  //adiciona o text do btn ao txtbox com base na posicao, comeca em zero e vai somando

            if(posicaoLetra < 5) { //verificação para parar de inputar quando ja estiver 5 palavras em tela

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

        private TableLayoutPanel SelecionarLinha(int tabIndex) {  //analisa todas as linhas no painel, seleciona pelo tabIndex e retorna
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

        private void ReiniciarTabela() { //reinicia as cores e os enables dos botos e das txtboxes e volta o jogo para a primeira linha primeira posicao

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