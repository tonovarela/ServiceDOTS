using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.Models
{
    public class ProductoPliego
    {
        public int Id_producto { get; set; }
        
        public int Copiasporpliego { get; set; }


        public override String ToString()
        {
            string output = string.Empty;
            output += "[Producto Pliego]";
            output += "\n";
            output += "\n     ID producto : " + Id_producto;
            output += "\n     Copias Requeridas : " + CopiasRequeridas;
            output += "\n     Copias por pliego : " + Copiasporpliego;
            output += "\n";



            return output;
        }

    }
}
