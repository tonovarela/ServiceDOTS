using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.Models
{
    public class Orden
    {
        public int OrdenID { get; set; }
        public DateTime FechaOrden { get; set; }
        public String Status { get; set; }
        public String MetodoEnvio { get; set; }

        public String MetodoPago { get; set; }
        public String TipoTarjetaCredito { get; set; }
        public String Subtotal { get; set; }
        public String  Impuesto  { get; set; }
        public String CostoEnvio { get; set; }
        public String  Descuento { get; set; }
        public String Total { get; set; }

        public override String ToString()
        {
            string output = string.Empty;
            output += "[ORDEN]";
            output += "\n";
            output += "\n     OrdenID : " + OrdenID;
            output += "\n     FechaOrden : " + FechaOrden;
            output += "\n     Status : " + Status;
            output += "\n     MetodoEnvio : " + MetodoEnvio;
            output += "\n     Metodo de Pago : " + MetodoPago;
            output += "\n     TipoTarjetaCredito : " + TipoTarjetaCredito;
            output += "\n     Subtotal : " + Subtotal;
            output += "\n     Impuesto : " + Impuesto;
            output += "\n     CostoEnvio : " + CostoEnvio;
            output += "\n     Descuento : " + Descuento;
            output += "\n     Total : " + Total;
            output += "\n";

            return output;
        }


    }
}
