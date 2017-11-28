﻿using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contador.DataLayer
{
    public class ProductoDAO : DAO
    {
        public bool existeSKU(string SKU)
        {
            return context.Items.Any(x => x.sku == SKU);
        }
        public void insertar(List<Producto> productos, int ordenID)
        {
            foreach (Producto producto in productos)
            {
                context.Productos.Add(
                    new Productos()
                    {
                        sku = producto.SKU,
                        cantidadordenada = producto.Cantidad_Ordenada,
                        id_producto = producto.Id_producto,
                        orden_numero = ordenID,
                        especificacion = producto.Especificacion
                    }
                    );
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("No se pudo guardar el producto " + producto.ToString());
                }

            }
        }

    }
}
