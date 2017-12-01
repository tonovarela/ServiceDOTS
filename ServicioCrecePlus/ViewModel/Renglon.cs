using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.ViewModel
{
    public class Renglon
    {

        public Orden Orden { get; set; }
        public Cliente Cliente { get; set; }
        public Producto Producto { get; set; }
    }
}
