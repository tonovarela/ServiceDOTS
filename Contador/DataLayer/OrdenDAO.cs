using Contador.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class OrdenDAO:DAO
    {
        public bool existeOrden(int orden_numero) 
        {
            return context.Ordenes.Any(x=>x.orden_numero==orden_numero);
        }
     
        public void insertarOrden(Orden orden, int clienteID)
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

            });                    
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
