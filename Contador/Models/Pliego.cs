using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.Models
{
    public class Pliego
    {
        public string Nombre { get; set; }
        public int Copias { get; set; }
        public int CortesGuillotina { get; set; }
    
        public string PorcentajeUso { get; set; }
        public string Tipo { get; set; }


        public override String ToString()
        {
            string output = string.Empty;
            output += "[Pliego]";
            output += "\n";
            output += "\n     Tipo : " + Tipo;
            output += "\n     Nombre : " +  Nombre ;
            output += "\n     Copias : " + Copias;
            output += "\n     Cortes de Guillotina : " + CortesGuillotina;
            output += "\n     PorcentajeUso : " + PorcentajeUso;                        
            return output;
        }
    }
}
