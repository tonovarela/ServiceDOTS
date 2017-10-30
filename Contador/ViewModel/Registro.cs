using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.ViewModel
{
    public class Registro
    {
        public Orden orden { get; set; }
        public Cliente cliente { get; set; }
        public List<RegistroItem> listaItem { get; set; }
    }
}
