using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.Models
{
    public class RegistroItem
    {
        
        public int Cantidad { get; set; }
        public string Costo { get; set; }
        public string Impuesto { get; set; }
        public string  Total { get; set; }   
        public string Descuento { get; set; }    
        public Orden orden { get; set; }
        public Item  item{ get; set; }


        public String Especificacion { get; set; }
        public override string ToString()
        {
            string output = String.Empty;
            output += "\n                                 ItemRegistro";
            output += "\n                                 ---------------------------------";
            output += "\n                                 Cantidad : " + Cantidad;
            output += "\n                                 Costo : " + Costo;
            output += "\n                                 Impuesto : " + Impuesto;
            output += "\n                                 Total: " + Total;
            output += "\n                                 Descuento : " + Descuento;
            output += "\n                                 OrdenID : " + orden.OrdenID;
                                                          
            output += "\n                                 ItemID : " + item.ItemID;
            output += "\n                                 Item Descripcion : " + item.Descripcion; ;
            output += "\n                                 SKU : " + item.SKU ;
            output += "\n                                 ---------------------------------";

            return output;

        }


    }
}
