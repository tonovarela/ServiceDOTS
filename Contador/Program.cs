using Contador.DataLayer;
using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Contador
{
    class Program
    {
        static Timer timer;



        static void Main(string[] args)
        {

            //timer = new Timer(1000);
            //timer.Elapsed += new ElapsedEventHandler(funcion);
            //timer.Interval = 5000;
            //timer.Enabled = true;
            LectorCSV lector = new LectorCSV();
            lector.LeerArchivos();
            Console.Read();

            //ClienteDAO clientedao = new ClienteDAO();
            //Cliente cliente = clientedao.getClienteID(1);

            //Console.WriteLine(cliente);

            //OrdenDAO ordendao = new OrdenDAO();
            //ordendao.insertarOrden(new Orden()
            //{
            //    OrdenID = 800,                
            //    CostoEnvio = "$55",
            //    Descuento = "60",
            //    FechaOrden = DateTime.Now,
            //    Impuesto = "$23",
            //    MetodoEnvio = "Express",
            //    MetodoPago = "Credito",
            //    Status = "Processing",
            //    Subtotal = "500",
            //    TipoTarjetaCredito = "MasterCard",
            //    Total = "800"

            //},1);
            //// Console.WriteLine(orden);

        }

        static void funcion(object sender, System.Timers.ElapsedEventArgs e)
        {


            //LectorXML lector = new LectorXML();
            //lector.LeerArchivos();            
        }
    }
}
