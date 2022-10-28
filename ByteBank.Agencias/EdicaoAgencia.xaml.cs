using ByteBank.Agencias.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace ByteBank.Agencias
{
    /// <summary>
    /// Lógica interna para EdicaoAgencia.xaml
    /// </summary>
    public partial class EdicaoAgencia : Window
    {
        private Agencia _agencia { get;}
        public EdicaoAgencia(Agencia agencia) // Para colocar a agencia na tela, você PRECISA RECEBER A AGENCIA VIA CONSTRUTOR
        {
            InitializeComponent();

            _agencia = agencia ?? throw new ArgumentNullException(nameof(agencia)); // Se ela for nulo, mande um exceção.
            AtualizarCamposDeTexto();
            AtualizarControles();
           
           // ValidarSomenteDigito(); //Verificar se o campo têm espaço ou se é digito, antes mesmo ter haver a mudança de evento com TextChanged
        }


        // Vamos preencher a janela com texto agora:
        private void AtualizarCamposDeTexto()
        {
            txtNumero.Text = _agencia.Numero;
            txtNome.Text = _agencia.Nome;
            txtTelefone.Text = _agencia.Telefone;
            txtEndereco.Text = _agencia.Endereco;
            txtDescrivao.Text = _agencia.Descricao;

        }

        // Nessa função nós iremos atualizar as funcionalidades dos eventos que devem ocorrer para o botão de Ok e Cancelar
        private void AtualizarControles()
        {
            RoutedEventHandler dialogResultTrue = (x, y) => { DialogResult = true; }; // Criando um expressã Lambda

            RoutedEventHandler dialogResultFalse = (x, y) => { DialogResult = false; }; // Criando um expressã Lambda

            RoutedEventHandler fechar = (x, y) => { Close(); };




            //btn precisa fechar a janela e indicar para quem abriu a janela se o usuário quer continuar ou parar as alterações que ele fez

            btnOk.Click += new RoutedEventHandler(dialogResultTrue) + new RoutedEventHandler(fechar); // Estamos passando dois comportamento para o evento de clicar no botão
            btnCancelar.Click += new RoutedEventHandler(dialogResultFalse) + new RoutedEventHandler(fechar); // Estamos passando dois comportamento para o evento de clicar no botão

            txtNome.Validacao += ConstruirDelegateDeValidacaoCampoNulo; // Adicionar uma função para o event de o texto mudar a cor.

            txtDescrivao.Validacao += ConstruirDelegateDeValidacaoCampoNulo;

            txtEndereco.Validacao += ConstruirDelegateDeValidacaoCampoNulo;

            txtNumero.Validacao += ConstruirDelegateDeValidacaoCampoNulo;

            txtTelefone.Validacao += ConstruirDelegateDeValidacaoCampoNulo;


            txtNumero.Validacao += ValidarSomenteDigito;










        }
        // Função para validar somente digito
        private void ValidarSomenteDigito(object sender, ValidacaoEventArgs e)
        {

            var ehValido = e.Text.All((x) => { return Char.IsDigit(x); });
            e.EhValido = ehValido;
                
        }
        

        // Vamos construir um método que constroí delegates:


        private void ConstruirDelegateDeValidacaoCampoNulo(object sender, ValidacaoEventArgs e)
        {


            var ehValido =  !String.IsNullOrEmpty(e.Text); // Descobrir se um texto está vazio / !isso indica negação do que o método irá retornar, se o método retorna true para vazio, então eu quremo que retorne diferrente de nulo ou vazio
            e.EhValido = ehValido;

        }




    
    }
}
