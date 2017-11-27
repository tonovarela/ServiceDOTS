using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.Models
{
    public class Producto
    {

        public int Id_producto { get; set; }
        public int Cantidad_Ordenada { get; set; }
        public string SKU { get; set; }

          
        public override string ToString()
        {
            string output = String.Empty;
            output += "\n                                 Producto";
            output += "\n                                  ID_producto : " + Id_producto; ;
            output += "\n                                  SKU : " + SKU; 
            output += "\n                                 Cantidad Ordenada : " +  Cantidad_Ordenada ;
            output += "\n                                 ---------------------------------";

            return output;

        }


    }
}
