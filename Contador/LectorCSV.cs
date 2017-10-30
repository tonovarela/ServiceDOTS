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
        private int _clienteID = 1;
        private int _itemID = 1;
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
                string ordenid = "XXXXXXXXXX?????"; //Id que se supone no existe
                List<RegistroItem> listaItem = null;
                RegistroItem registroItem = null;
                CsvReader csv = new CsvReader(new StreamReader(file.FullName));
                #endregion

                #region Llenado de la Orden  el Cliente y los ITEMS
                //Leemos la cabecera y se omite
                csv.Read();
                while (csv.Read())
                {
                    if (ordenid != csv.GetField(0))
                    {
                        if (orden != null)
                            registros.Add(new Registro() { cliente = cliente, orden = orden, listaItem = listaItem });
                        //Se asigna la siguiente orden de compra
                        ordenid = csv.GetField(0);
                        cliente = this.getCliente(csv);
                        orden = this.getOrden(csv);
                        listaItem = new List<RegistroItem>();
                    }
                    registroItem = this.getRegistroItem(csv);
                    listaItem.Add(registroItem);

                }
                registros.Add(new Registro() { cliente = cliente, orden = orden, listaItem = listaItem });
                #endregion
            }
                #region Interaccion con la Capa de Datos

            ClienteDAO clienteDao = new ClienteDAO();
            OrdenDAO ordenDao = new OrdenDAO();
            ItemDAO itemDao = new ItemDAO();

            foreach (Registro registro in registros)
            {
                //Si la orden existe no procesarla
                if (ordenDao.getOrden(registro.orden.OrdenID) == null)
                {
                    clienteDao.InsertarCliente(registro.cliente);
                    ordenDao.insertarOrden(registro.orden, registro.cliente.ClienteID);
                    itemDao.insertarItem(registro.listaItem, registro.orden.OrdenID);
                }else
                {
                    Console.WriteLine("Esta orden ya fue ingresada a la base de datos");
                }
                this.ListarObjetosGenerados(registro);
            }
            #endregion

        }

        #region  Creacion de los Objetos con la informacion del archivo CSV
        private RegistroItem getRegistroItem(CsvReader csv)
        {
            Item item = new Item()
            {
                ItemID = this._itemID++,//Por definir
                Descripcion = csv.GetField(40),
                SKU = csv.GetField(42)
            };
            RegistroItem itemregistro = new RegistroItem()
            {
                item = item,
                orden = this.getOrden(csv),
                Cantidad = Int32.Parse(csv.GetField(46)),
                Costo = csv.GetField(45),
                Descuento = csv.GetField(52),
                Impuesto = csv.GetField(51),
                Total = csv.GetField(53),
                Especificacion=csv.GetField(43)
            };
            return itemregistro;

        }

        private Cliente getCliente(CsvReader csv)
        {
            Cliente cliente = new Cliente()
            {
                ClienteID = this._clienteID++,//Por definir
                Nombre = csv.GetField(17),
                Email = csv.GetField(18),
                Compania = csv.GetField(20),
                Calle = csv.GetField(21),
                CodigoPostal = csv.GetField(22),
                Ciudad = csv.GetField(23),
                Estado = csv.GetField(24),                
                Pais = csv.GetField(27),
                Telefono = csv.GetField(28),
                

            };
            return cliente;
        }

        private Orden getOrden(CsvReader csv)
        {

            Orden orden = new Orden()
            {
                OrdenID = Int32.Parse(csv.GetField(0)),
                FechaOrden = DateTime.Parse(csv.GetField(1)),
                Status = csv.GetField(2),
                MetodoPago = csv.GetField(4),
                TipoTarjetaCredito = csv.GetField(5),                 
                MetodoEnvio = csv.GetField(6),
                Subtotal = csv.GetField(7),
                Impuesto = csv.GetField(8),
                CostoEnvio = csv.GetField(9),
                Descuento = csv.GetField(10),
                Total = csv.GetField(12),
            };

            return orden;

        }
        #endregion


        private void ListarObjetosGenerados(Registro registro)
        {
            Console.WriteLine(registro.cliente);
            Console.WriteLine(registro.orden);
            Console.WriteLine("\n---- Total de Items[" + registro.listaItem.Count + "]-------\n");
            foreach (RegistroItem item in registro.listaItem)
                Console.WriteLine(item);
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
