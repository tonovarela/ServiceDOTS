using Contador.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using System.Xml.Linq;

namespace Contador
{
    public class LectorXML : Lector
    {
        public LectorXML(string ruta, string extension) : base(ruta, extension)
        {
            this.ruta = ruta;
            this.extension = extension;
        }
        public void ProcesarArchivos()
        {
            foreach (FileInfo file in this.listarArchivos())
            {
                Console.WriteLine(String.Format("[{0}]", file.FullName));
                Console.WriteLine("-------------------------------------");
                this.LeerXML(file.FullName);
            }
        }

        private void LeerXML(String path)
        {
            XDocument xdoc = XDocument.Load(path);
            //var productos = (from producto in xdoc.Descendants("PlacedObject")
            //                 select new
            //                 {
            //                     Grupo = producto.Attribute("Group").Value,
            //                 }).GroupBy(g => g.Grupo)
            //                    .Select(s => s.First());



            var pliego = (from p in xdoc.Descendants("Layout")
                          select new
                          {
                              Id = p.Attribute("ID").Value,
                              Grade = p.Attribute("Grade").Value,
                              Copias = (Int32.Parse(p.Attribute("Copies").Value) - Int32.Parse(p.Attribute("WastageCopies").Value)),
                              CortesGuillotina = p.Attribute("GuillotineCutCount").Value,
                              UsodelPliego = p.Attribute("SheetUsage").Value
                          }).First();

            Console.WriteLine("----------------------Pliego------------------------");
            Console.WriteLine(pliego);
            Console.WriteLine("---------------------------- ------------------------");



            var productos = (from producto in xdoc.Descendants("PlacedObject")
                             select new
                             {
                                 ComponentName = producto.Attribute("ComponentName").Value,
                                 Grupo = producto.Attribute("Group").Value == "" ? "SinSubpliego" : producto.Attribute("Group").Value,
                                 CopiasRequeridas = producto.Attribute("RequiredCopies").Value,
                                 CopiasProducidas = producto.Attribute("ProducedCopies").Value,
                                 ProductosporPliego = producto.Descendants("Placement").Count()
                             }).OrderBy(x => x.ProductosporPliego);
            Console.WriteLine("----------------------------Productos ------------------------");
            foreach (var producto in productos)
                Console.WriteLine(producto);

            Console.WriteLine("--------------------------------------------------------------");


        }



    }



}
