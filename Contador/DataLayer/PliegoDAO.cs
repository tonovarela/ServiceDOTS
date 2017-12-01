using Contador.Models;
using System;
using System.Collections.Generic;

namespace Contador.DataLayer
{
    public class PliegoDAO : DAO
    {

        public bool insertar(Pliego pliego, List<ProductoPliego> productos)
        {
            bool inserto =false;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    #region Creacion del Pliego
                    Pliegos p = new Pliegos
                    {
                        nombre = pliego.Nombre,
                        copias = pliego.Copias,
                        cortesguillotina = pliego.CortesGuillotina,
                        porcentajeuso = pliego.PorcentajeUso,
                        tipo = pliego.Tipo
                    };
                    #endregion
                    context.Pliegos.Add(p);
                    context.SaveChanges();                   
                    #region Asignacion de productos al pliego
                    productos.ForEach(producto =>
                    {
                        ProductosPliego pl = new ProductosPliego
                        {
                            id_pliego = p.id_pliego,
                            copiasporpliego = producto.Copiasporpliego,
                            id_producto = producto.Id_producto,                            
                        };
                        context.ProductosPliego.Add(pl);
                    }
                            );
                    #endregion
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                    inserto = true;
                }
                catch (Exception e)
                {
                   
                    dbContextTransaction.Rollback();
                    inserto = false;
                    Console.WriteLine(e.Message);
                }
                return inserto;

            }



        }

    }
}
