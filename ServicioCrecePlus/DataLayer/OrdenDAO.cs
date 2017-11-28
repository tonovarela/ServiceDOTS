using Contador.Models;
using ServicioCrecePlus;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class OrdenDAO
    {
        public Orden getOrden(int orden_numero)
        {
            Orden orden = null;
            using (CrecePlusEntities context = new CrecePlusEntities())
            {
                var data = context.Ordenes.FirstOrDefault(x => x.orden_numero == orden_numero);
                if (data != null)
                {
                    orden = new Orden
                    {                        
                        FechaOrden = (DateTime)data.fechaorden,
                        CostoEnvio = data.costo_envio,
                        Impuesto = data.impuesto,
                        MetodoEnvio = data.metodo_envio,
                        MetodoPago = data.metodo_pago,
                        OrdenNumero = data.orden_numero,
                        Subtotal = data.subtotal,
                        TipoTarjetaCredito = data.tipo_tarjeta_credito,
                        Total = data.total
                    };
                }
            }

            return orden;
        }
        public void insertarOrden(Orden orden, int clienteID)
        {
            using (CrecePlusEntities context = new CrecePlusEntities())
            {
                


                context.Ordenes.Add(new Ordenes()
                {
                    id_cliente = clienteID,
                    orden_numero = orden.OrdenNumero,
                    costo_envio = orden.CostoEnvio,
                    fechaorden = orden.FechaOrden,                    
                    impuesto = orden.Impuesto,
                    metodo_envio = orden.MetodoEnvio,
                    tipo_tarjeta_credito = orden.TipoTarjetaCredito,
                    total = orden.Total,
                    subtotal = orden.Subtotal,                    
                    metodo_pago = orden.MetodoPago
                       
                }
                    );
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException  e)
                {
                    Console.WriteLine(context.GetValidationErrors()); 
                }


            }
        }

    }
}
