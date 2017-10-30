using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TestService.Clases;

namespace TestService
{
    public partial class ServiceDOTS : ServiceBase
    {
        private static Timer timer;

        public ServiceDOTS()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("Fuente"))
            {
                System.Diagnostics.EventLog.CreateEventSource("Fuente","Log");
            }            
            eventLog1.Source = "Fuente";
            eventLog1.Log="Log";            
       }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(tarea);
            //Se ejecutara cada 5 segundos
            timer.Interval = 5000;
            timer.Enabled = true;            
            eventLog1.WriteEntry("Iniciando el servicio");           
        }

         void tarea(object sender,ElapsedEventArgs e)
        {            
            LectorXML lectorIMP = new LectorXML(@"C:\Desarrollo\IMP", @"C:\Desarrollo\Salida", eventLog1);
            lectorIMP.LeerArchivos();
            LectorXML lectorPQ = new LectorXML(@"C:\Desarrollo\PQ", @"C:\Desarrollo\Salida", eventLog1);
            lectorPQ.LeerArchivos();
        }
        protected override void OnStop()
        {
            timer.Enabled = false;
            eventLog1.WriteEntry("Se detuvo el servicio de DOTS",EventLogEntryType.Warning,500);
        }
    }
}
