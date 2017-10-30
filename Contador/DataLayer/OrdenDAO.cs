using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class OrdenDAO
    {
        public Orden getOrden(int ordenID)
        {
            Orden orden = null;
            using (DOTSEntities context = new DOTSEntities())
            {
                var data = context.Ordenes.FirstOrDefault(x => x.OrdenID == ordenID);
                if (data != null)
                {
                    orden = new Orden
                    {
                        OrdenID = data.OrdenID,
                        Descuento = data.Descuento,
                        CostoEnvio = data.CostoEnvio,
                        FechaOrden = (DateTime)data.FechaOrden,
                        Subtotal = data.Subtotal,
                        Impuesto = data.Impuesto,
                        MetodoEnvio = data.MetodoEnvio,
                        MetodoPago = "spei",
                        Status = data.Status,
                        TipoTarjetaCredito = data.TipoTarjetaCredito,
                        Total = data.Total
                        

                    };
                }
            }

            return orden;
        }
        public void insertarOrden(Orden orden, int clienteID)
        {
            using (DOTSEntities context = new DOTSEntities())
            {
                context.Ordenes.Add(new Ordenes()
                {
                    ClienteID = clienteID,
                    OrdenID = orden.OrdenID,
                    Descuento = orden.Descuento,
                    CostoEnvio = orden.CostoEnvio,
                    Impuesto = orden.Impuesto,
                    MetodoEnvio = orden.MetodoEnvio,
                    Status = orden.Status,
                    Subtotal = orden.Subtotal,
                    FechaOrden = orden.FechaOrden,
                    MetodoPago=orden.MetodoPago,
                    TipoTarjetaCredito = orden.TipoTarjetaCredito,
                    Total = orden.Total
                }
                    );
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                }


            }
        }

    }
}
