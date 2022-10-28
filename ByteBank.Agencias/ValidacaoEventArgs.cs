using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Agencias
{
    public class ValidacaoEventArgs : EventArgs
    {
        public string Text { get; private set; }
        public bool EhValido { get; set; }
        //Tornar obrigatório a passar o texto
        public ValidacaoEventArgs(string txt)
        {
            this.Text = txt;
            this.EhValido = true;   
        }
    }
}
