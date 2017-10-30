using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class ItemDAO : DAO
    {


        private void InsertarCatalogo(Item item)
        {


            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            context.Items.Add(new Items
            {
                ItemID = item.ItemID,
                Descripcion = item.Descripcion,
                SKU = item.SKU
            });
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
            }       
        }
        public void insertarItem(List<RegistroItem> registrosItem, int ordenID)
        {
            foreach (RegistroItem registro in registrosItem)
            {
                //Inserta en el catalogo
                this.InsertarCatalogo(registro.item);

                context.OrdenItems.Add(new OrdenItems
                {
                    OrdenID = ordenID,
                    ItemID = registro.item.ItemID,
                    Cantidad = registro.Cantidad,
                    Costo = registro.Costo,
                    Descuento = registro.Descuento,
                    Impuesto = registro.Impuesto,
                    Especificacion = registro.Especificacion,
                    Total = registro.Total
                });
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
