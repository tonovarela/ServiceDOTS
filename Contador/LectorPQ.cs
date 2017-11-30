using Contador.DataLayer;
using Contador.Models;
using Contador.ViewModel;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador
{
    public class LectorPQ : Lector
    {
        public LectorPQ(string ruta, string extension) : base(ruta, extension)
        {

        }

        public void ProcesarArchivos()
        {
            foreach (FileInfo file in this.listarArchivos())
            {
                //Si el Archivo esta abierto no procesarlo
                if (!this.sePuedeLeerArchivo(file.FullName))
                {
                    continue;
                }
                List<Renglon> renglones = new List<Renglon>();
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    CsvReader csv = new CsvReader(reader);

                    csv.Read();
                    while (csv.Read())
                    {
                        renglones.Add(this.leerRenglon(csv));
                    }
                }
                
                List<String> mensajes = new List<String>();
                List<Registro> registros = new List<Registro>();

                #region llenado de productos por orden
                //var numeroOrdenes = (from p in renglones
                //                     select p.Orden.OrdenNumero).Distinct().ToList();
                var numeroOrdenes = renglones.Select(x => x.Orden.OrdenNumero).Distinct().ToList();
                foreach (var numeroOrden in numeroOrdenes)
                    {
                    Registro registro = new Registro();

                    var data = renglones.Where(x => x.Orden.OrdenNumero == numeroOrden).First();                                                                                                 
                        registro.cliente = data.Cliente;
                        registro.orden = data.Orden;                        
                        renglones.Where(x => x.Orden.OrdenNumero == numeroOrden)
                                 .ToList()
                                 .ForEach(renglon => registro.productos.Add(renglon.Producto));

                    var ordendao = new OrdenDAO();
                    var productodao = new ProductoDAO();
                        if (ordendao.existeOrden(numeroOrden))
                        {
                            mensajes.Add(String.Format("La orden {0} ya fue procesada anteriormente", numeroOrden));
                        }
                        registro.productos.ForEach(producto =>
                        {
                            if (!productodao.existeSKU(producto.SKU))
                            {
                                mensajes.Add(String.Format("EL SKU {0} no esta registrado en la Sistema", producto.SKU));
                            }
                            if (productodao.existeID(producto.Id_producto))
                            {
                                mensajes.Add(String.Format("EL Producto con SKU {0}  ya fue registrado anteriormente", producto.SKU));
                            }
                        }
                        );

                    registros.Add(registro);
                                                             
                    }
                #endregion


                #region Interaccion con la capa de Datos
                if (mensajes.Count > 0)
                {
                    Console.WriteLine("Hubo errores en el archivo "+file.Name);
                    this.EscribirErrores(file.FullName, mensajes);
                    this.quitarExtension(file.FullName);
                }else
                {
                    Console.WriteLine("Se guardaron los registros del archivo" + file.Name);
                    foreach (Registro registro in registros)
                    {
                        ClienteDAO clienteDao = new ClienteDAO();
                        OrdenDAO ordenDao = new OrdenDAO();
                        ProductoDAO productoDao = new ProductoDAO();
                        clienteDao.InsertarCliente(registro.cliente);
                        ordenDao.insertarOrden(registro.orden, registro.cliente.Id_Cliente);
                        productoDao.insertar(registro.productos, registro.orden.OrdenNumero);
                        //this.ListarObjetosGenerados(registro);
                        
                    }
                    File.Delete(file.FullName);

                }
                #endregion


            }



        }


        private Renglon leerRenglon(CsvReader csv)
        {
            return new Renglon()
            {
                Cliente = this.leerCliente(csv),
                Orden = this.leerOrden(csv),
                Producto = this.leerProducto(csv)
            };
        }

        private Producto leerProducto(CsvReader csv)
        {
            Producto producto = new Producto()
            {
                Id_producto = Int32.Parse(csv.GetField(41)),
                Cantidad_Ordenada = Int32.Parse(csv.GetField(49)),
                SKU = csv.GetField(45),
                Especificacion = csv.GetField(46)

            };

            return producto;
        }

        private Cliente leerCliente(CsvReader csv)
        {
            Cliente cliente = new Cliente()
            {
                Id_Cliente = Int32.Parse(csv.GetField(17)),
                Nombre = csv.GetField(18),
                Email = csv.GetField(19),
                Compania = csv.GetField(21),
                Calle = csv.GetField(22),
                CodigoPostal = csv.GetField(23),
                Ciudad = csv.GetField(24),
                Estado = csv.GetField(26),
                Pais = csv.GetField(28),
                Telefono = csv.GetField(29),


            };
            return cliente;
        }

        private Orden leerOrden(CsvReader csv)
        {

            Orden orden = new Orden()
            {
                OrdenNumero = Int32.Parse(csv.GetField(0)),
                FechaOrden = DateTime.Parse(csv.GetField(1)),
                MetodoPago = csv.GetField(4),
                TipoTarjetaCredito = csv.GetField(5),
                MetodoEnvio = csv.GetField(6),
                Subtotal = csv.GetField(7),
                Impuesto = csv.GetField(8),
                CostoEnvio = csv.GetField(9),
                Total = csv.GetField(12),
            };

            return orden;

        }
        private void ListarObjetosGenerados(Registro registro)
        {
            Console.WriteLine(registro.cliente);
            Console.WriteLine(registro.orden);
            Console.WriteLine("\n---- Total de Productos[" + registro.productos.Count + "]-------\n");
            foreach (Producto producto in registro.productos)
                Console.WriteLine(producto);
        }

    }
}
