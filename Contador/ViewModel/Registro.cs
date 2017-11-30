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

       public Registro()
        {
            this.productos = new List<Producto>();
        }
        public Orden orden { get; set; }
        public Cliente cliente { get; set; }
        public List<Producto> productos { get; set; }
    }
}
