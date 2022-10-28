using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Agencias.DAL
{
    public partial class Agencia
    {
        public override string ToString() // Agora que pensei, esté método é override, ou seja quanod for criado um objeto desse tipo, ele deve sobreescrever a implementação da classe object
        {
            return $"{Numero} - {Nome}".Trim(); ;// Método Trim apaga todos os espaço de um string
        }


    }
}
