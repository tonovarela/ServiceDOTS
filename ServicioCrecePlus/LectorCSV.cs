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
    public class LectorCSV
    {
        private string ruta = @"C:\Desarrollo\CSV";
      
        public void LeerArchivos()
        {
            List<Registro> registros = new List<Registro>();
            //Para cada Archivo csv            
            foreach (FileInfo file in this.listarArchivos())
            {
                //Si el Archivo esta abierto no procesarlo
                if (!this.sePuedeLeerArchivo(file.FullName))
                {
                    Console.WriteLine("El archivo no se puede Leer");
                    continue;
                }
                #region Inicializan los objetos
                Orden orden = null;
                Cliente cliente = null;
                string ordenid = "XXXXXXXXXX"; //Id que se supone no existe
                List<Producto> listaProductos = null;
                Producto producto = null;
                CsvReader csv = new CsvReader(new StreamReader(file.FullName));
                #endregion

                #region Llenado de la Orden  el Cliente y los Productos
                //Leemos la cabecera y se omite
                csv.Read();
                while (csv.Read())
                {
                    if (ordenid != csv.GetField(0))
                    {
                        if (orden != null)
                            registros.Add(new Registro() { cliente = cliente, orden = orden,  productos = listaProductos });
                        //Se asigna la siguiente orden de producción
                        ordenid = csv.GetField(0);
                        cliente = this.getCliente(csv);
                        orden = this.getOrden(csv);
                        listaProductos = new List<Producto>();
                    }
                   
                    producto = this.getProducto(csv);

                    var existeSKU = (new ProductoDAO()).existeSKU(producto.SKU);

                    if (!existeSKU)
                    {
                        
                        Console.WriteLine("SKU no se existe"+ producto.SKU);
                        String archivoSP = file.FullName.Replace(".csv", String.Format("[{0:yyyy-MM-dd_HHmmss}]",DateTime.Now));
                        csv.Dispose();
                        File.Move(file.FullName,archivoSP);
                        return;
                        
                    }else
                    {
                        listaProductos.Add(producto);
                    }
                    
                }
                
                

                registros.Add(new Registro() { cliente = cliente, orden = orden, productos = listaProductos });
                #endregion

                csv.Dispose();
                File.Delete(file.FullName);


            }
                #region Interaccion con la Capa de Datos

            ClienteDAO clienteDao = new ClienteDAO();
            OrdenDAO ordenDao = new OrdenDAO();
            ProductoDAO productoDao = new ProductoDAO();

            foreach (Registro registro in registros)
            {
                //Si la orden existe no procesarla
                if (ordenDao.getOrden(registro.orden.OrdenNumero) == null)
                {
                      clienteDao.InsertarCliente(registro.cliente);
                      ordenDao.insertarOrden(registro.orden, registro.cliente.Id_Cliente);
                      productoDao.insertar(registro.productos,registro.orden.OrdenNumero);
                //    //itemDao.insertarItem(registro.productos, registro.orden.OrdenNumero);
                }
            else
                {
                    Console.WriteLine("Esta orden ya fue ingresada a la base de datos");
                }
                this.ListarObjetosGenerados(registro);
            }
            #endregion

        }

        #region  Creacion de los Objetos con la informacion del archivo CSV

        private Producto getProducto(CsvReader csv)
        {
            Producto producto = new Producto()
            {
                Id_producto = Int32.Parse(csv.GetField(42)),
                Cantidad_Ordenada = Int32.Parse(csv.GetField(49)),
                SKU = csv.GetField(45)                                             
            };
            
            return producto;
        }
        

        private Cliente getCliente(CsvReader csv)
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

        private Orden getOrden(CsvReader csv)
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
        #endregion


        private void ListarObjetosGenerados(Registro registro)
        {
            Console.WriteLine(registro.cliente);
            Console.WriteLine(registro.orden);
            Console.WriteLine("\n---- Total de Productos[" + registro.productos.Count + "]-------\n");
            foreach (Producto producto in registro.productos)
                Console.WriteLine(producto);
        }
        private FileInfo[] listarArchivos()
        {
            DirectoryInfo directorio = new DirectoryInfo(this.ruta);
            FileInfo[] archivos = directorio.GetFiles("*.csv");
            return archivos;
        }
        private bool sePuedeLeerArchivo(string path)
        {
            try
            {
                File.Open(path, FileMode.Open, FileAccess.Read).Dispose();
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

       

    }
}
