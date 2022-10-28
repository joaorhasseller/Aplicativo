using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ByteBank.Agencias
{
    // Colocar a assinatura do método na raíz do namespace
    //Todo delegate criado deve seguir o padrão e vir com Sufixo EventHandler
    public delegate void ValidacaoEventHandler(object sender, ValidacaoEventArgs e);


        

    public class ValidaTextBox : TextBox // Como nós vamos usar esse objeto de ValidaTextBox na nossa janela EdicaoAgencia?
                                         //Podemos injetar no código manualmente de forma programática ou PODEMOS (USAR MARCAÇÃO NO XML) DIZENDO QUE NÓS VAMOS USAR NOSSA CLASSE COMO OBJETO NO XML.
                                          // Utlizamos o xml namespace que indica aonde há uma classe que possa ser utilizado como objejto.
                                         //xmlns:local='nossoNameSpace.'  xmlns:local="clr-namespace:ByteBank.Agencias"
                                         //  <local:ValidaTextBox x:Name="txtNome"></local:ValidaTextBox> exemplo
    {
        // vamos fazer nosso evento de validação, o evento precisa de um assinatura formal de um método para ser invocado, para isso usamso delegate
        // para criar um evento, coloque a palavra reservada: event - para indicar que não é um atributo normal, têm um atributo que é um evento

        private ValidacaoEventHandler _validacao;
        public event ValidacaoEventHandler Validacao
        {
            add //Toda vez que adicionar um comportamento novo, será executado um método já 
            {
                _validacao+= value;
               // OnValidacao(); // Toda vez que adicionar um comportamento esse método será executado.
            }
            remove
            {
                _validacao-= value;
            }
        }
        //FUNDAMENTAL
        // Não queromos mais assinar o nosso próprio evento para executar um código, ou seja criar um função e nela colocar o nosso códig, já existe um função que, já existe um evento que guarda a função OnTextChanged,da classe base, apenas precisamos sobreescrever ela.
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e); // Já existe um comportamento implementado na base
            OnValidacao(); // Comportamento que nós queremos no nosso.
        }

        // Adicionar um função para quando NO MOMENTO ESTIVER ACONTECENDO A VALIDAÇÃO
        protected virtual void OnValidacao() // Protected para classes filhas enxergarem e virtual para classes filhas poderem sobrescrever seus comportamentos.
        {
            if (this._validacao != null) // Lebrando que a propriedade Validação armazena um "função objeto", isso siginica que pode não ter nenhum objeto dentro dela, por isso apontar para um referênica nula
            {

                var listaValidacao = _validacao.GetInvocationList(); // Não esqueçã que _validacao é uma variável que aponta para um objeto do tipo ValidacaoEventHandler, que é uma classe que deriva da Delegate que contém assim seus métodos.
                var eventArgs = new ValidacaoEventArgs(this.Text);                                //Conseguimos acessa todos os delegates que foram assinados em nosso evento.
                var ehValido = true;    // Você ESTÁ PASSANDO O TEXT DO SEU PROPRIO OBJETO, o objeto que foi instânciado, você ESTÁ PASSANDO O TEXT desse objeto que foi construído
                foreach (ValidacaoEventHandler validacao in listaValidacao)
                {

                    validacao(this, eventArgs);

                    if (!eventArgs.EhValido) // "Se dentro de EhValido for diferente de true, executa esse código", se for falso.: mesma coisa que eventArgs.EhValido == false
                    {
                        ehValido = false;  
                        break;// Parar o laço de repetição.
                    }
                }

                this.Background = ehValido
                    ? new SolidColorBrush(Colors.White) // A primeira é ser for verdade
                    : new SolidColorBrush(Colors.OrangeRed); // A segunda é ser for falso
            }
        }
    }
}
