using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Contador;

namespace ServicioCrecePlus
{
    public partial class Service1 : ServiceBase
    {
        private static Timer timer;

        public Service1()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("Fuente"))
            {
                System.Diagnostics.EventLog.CreateEventSource("Fuente", "Log");
            }
            eventLog1.Source = "Fuente";
            eventLog1.Log = "Log";
        }

        protected override void OnStart(string[] args)
        {

            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(tarea);
            //Se ejecutara cada 5 segundos
            timer.Interval = 5000;
            timer.Enabled = true;
            eventLog1.WriteEntry("Iniciando el servicio de CrecePLus");

        }

        void tarea(object sender, ElapsedEventArgs e)
        {
            LectorPQ lector = new LectorPQ(@"C:\Desarrollo\CSV","csv");
            lector.ProcesarArchivos();
            eventLog1.WriteEntry("Se ha ejecutado el servicio de CRECE");
            //LectorXML lectorIMP = new LectorXML(@"C:\Desarrollo\IMP", @"C:\Desarrollo\Salida", eventLog1);
            //lectorIMP.LeerArchivos();

        }


        protected override void OnStop()
        {
            timer.Enabled = false;
            eventLog1.WriteEntry("Se detuvo el servicio de CRECEplus", EventLogEntryType.Warning, 500);
        }
    }
}
