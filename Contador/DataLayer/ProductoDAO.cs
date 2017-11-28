using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class ProductoDAO : DAO
    {


        public bool existeSKU(string SKU)
        {
            bool existe = true;            
            if (!context.Items.Any(x => x.sku == SKU))
            {
                existe = false;
            }
            return existe;
        }


        //private void InsertarCatalogo(Item item)
        //{


        //    context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        //    context.Items.Add(new Items
        //    {
        //        ItemID = item.ItemID,
        //        Descripcion = item.Descripcion,
        //        SKU = item.SKU
        //    });
        //    try
        //    {
        //        context.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //    }       
        //}
        public void insertar(List<Producto> productos, int ordenID)
        {
            foreach (Producto producto in productos)
            {
                //Inserta en el catalogo
                //this.InsertarCatalogo(registro.item);

                context.Productos.Add(
                    new Productos()
                    {
                        sku = producto.SKU,
                        cantidadordenada = producto.Cantidad_Ordenada,
                        id_producto = producto.Id_producto,
                        orden_numero = ordenID,
                        especificacion=producto.Especificacion
                         

                    }
                    );
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("No se pudo guardar el producto "+producto.ToString());
                }

            }
        }

    }
}
