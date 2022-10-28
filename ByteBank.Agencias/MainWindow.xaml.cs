using ByteBank.Agencias.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ByteBank.Agencias
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ByteBankEntities _contextoBancoDeDados = new ByteBankEntities(); // Conexão com banco de dados
        private ListBox lstAgencias { get; } // Propriedade readonly

        public MainWindow() // Construtor da janela principal
        {

            InitializeComponent();
            lstAgencias = new ListBox();  // Esse this, se refere ao objeto do tipo MainWindow e também agora Main windows têm acesso a esse objeto e as suas propriedades.

            AtualizarControles(); // Esse método irá atualizar os itens da nossa tela
            AtualizarListaDeAgencia();

        }

        private void AtualizarControles()
        {
           lstAgencias.Background = Brushes.DarkGray;
            lstAgencias.BorderBrush = Brushes.Gray;
            lstAgencias.Width = 270;
            lstAgencias.Height = 290;
            Canvas.SetTop(lstAgencias, 15); // Distânica que queremos do top da janela do Canvas
            Canvas.SetLeft(lstAgencias, 15);

            lstAgencias.SelectionChanged+= new SelectionChangedEventHandler(lstAgencias_SelectionChanges); // Sempre quanod for adcionar um comportamento em um atribuito de evento precesar ser atribuito +=

            container.Children.Add(lstAgencias); // Estamos adicioanndo o objeto no canvas da mains window, para nós podemos utlizar sua propriedade. Estamos fazendo para poder adicionar um funcionalidade no envento de clicar.

            // Vamos adicionar um novo comportamento para o event de click no botão de editar

            btnEditar.Click+= new RoutedEventHandler(btnEditar_Click);




        }
        
        // Função: Abrir uma nova janela, para o usuário editar as agênicias
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
           var janelaEdicao = new EdicaoAgencia((Agencia)lstAgencias.SelectedItem); // Criar a nova janela
          var resutado =   janelaEdicao.ShowDialog().Value;  // Abre a nova janela.

            if (resutado == true)
            {
                //Usuario clicou em ok
            }
            if (resutado == false)
            {
                //Usuario clicou em false
            }
        }





        // Quando falamos de eventos, estamos falando de manipular métodos; no .Net para definir métodos sua assinatura usamos delegates
        // O método abaixo ele implementa a assinatura do delegate do tipo SelectionChangedEventHandler


        private void lstAgencias_SelectionChanges(object sender, SelectionChangedEventArgs e)
        {
            var agenciaSelecionada = (Agencia)lstAgencias.SelectedItem;

            txtNome.Text = agenciaSelecionada.Nome;
            txtNumero.Text = agenciaSelecionada.Numero;
            txtEndereco.Text = agenciaSelecionada.Endereco;
            txtTelefone.Text = agenciaSelecionada.Telefone;
            txtDescrivao.Text = agenciaSelecionada.Descricao;
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Essa função fica dentro do atributo de evento Click da classe Button, isso é uma função objeto, que é atribuído para a propriedade Click e é disparada a função no momento do click do mouse, e a função de dentro do  evento acontece quando o usuário dá um click, através de um fonte externa do click do mouse do usuário    
        {
           MessageBoxResult confirmacao =  MessageBox.Show("Você deseja realmente excluir esse item","Confirmação",MessageBoxButton.YesNo);

            if(confirmacao == MessageBoxResult.Yes)
            {

            }
            else
            {

            }
        }


        private void AtualizarListaDeAgencia()
        {
            lstAgencias.Items.Clear(); // Irá limpar qualquer dados que carregar antecipadamente.

            var agencias = _contextoBancoDeDados.Agencias.ToList(); // Retorna um lista de Agencia.
            foreach (var agencia in agencias)
            {

                lstAgencias.Items.Add(agencia);   // Acesso a propriedade Items da nossa lista do objeto, nela podemos adcionar qualquer coisa

            }
        }
    }
}
