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
       
        static void Main(string[] args)
        {

            //timer = new Timer(1000);
            //timer.Elapsed += new ElapsedEventHandler(funcion);
            //timer.Interval = 5000;
            //timer.Enabled = true;
            //LectorCSV lector = new LectorCSV(@"C:\Desarrollo\CSV","csv");
            //lector.ProcesarArchivos();



            //LectorXML lector = new LectorXML(@"C:\Desarrollo\IMP","xml");
            //lector.ProcesarArchivos();

            LectorPQ lector = new LectorPQ(@"C:\Desarrollo\CSV", "csv");
            lector.ProcesarArchivos();
            Console.Read();



        }

        static void funcion(object sender, System.Timers.ElapsedEventArgs e)
        {


            //LectorXML lector = new LectorXML();
            //lector.LeerArchivos();            
        }
    }
}
