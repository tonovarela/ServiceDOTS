using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.Models
{
    public class Cliente
    {
        public int Id_Cliente { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public String Compania { get; set; }
        public String Calle { get; set; }
        public String CodigoPostal { get; set; }
        public String Estado { get; set; }        
        public String Pais { get; set; }    
        public String Telefono { get; set; }
        public String Ciudad { get; set; }


        public override String ToString()
        {
            string output = String.Empty;
            output += "[CLIENTE]";
            output += "\n";
            output += "\n      Id_Cliente:  " +  Id_Cliente;
            output += "\n      Nombre:  " + Nombre;
            output += "\n      Correo:  " + Email;            
            output += "\n      Compañia: " + Compania;
            output += "\n      Calle:   " + Calle;
            output += "\n      Codigo Postal:  " + CodigoPostal;            
            output += "\n      Estado: " + Estado;
            output += "\n      Ciudad: " + Ciudad;
            output += "\n      Pais: " + Pais;
            output += "\n      Telefono: " + Telefono;
            output += "\n";
            
            return output;
        }
    }
}
