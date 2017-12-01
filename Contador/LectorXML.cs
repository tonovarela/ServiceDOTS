using Contador.DataLayer;
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


        private Pliego _pliego = null;
        List<ProductoPliego> _productospliego = null;
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
                this.LeerXML(file.FullName, file.Name);
            }
        }

        private void LeerXML(String fullPathfile, string name)
        {
            XDocument xdoc = XDocument.Load(fullPathfile);
            //var productos = (from producto in xdoc.Descendants("PlacedObject")
            //                 select new
            //                 {
            //                     Grupo = producto.Attribute("Group").Value,
            //                 }).GroupBy(g => g.Grupo)
            //                    .Select(s => s.First());
            #region Lectura del Pliego
            _pliego = xdoc.Descendants("Layout")
                                            .Select(x=> new Pliego
                                            {
                                                Nombre = name,
                                                Tipo = x.Attribute("Grade").Value,
                                                Copias = (Int32.Parse(x.Attribute("Copies").Value) - Int32.Parse(x.Attribute("WastageCopies").Value)),
                                                CortesGuillotina = Int32.Parse(x.Attribute("GuillotineCutCount").Value),
                                                PorcentajeUso = x.Attribute("SheetUsage").Value
                                            }).First();
            #endregion        

            #region Lectura de los productos del pliego
            _productospliego = xdoc.Descendants("PlacedObject")
                                       .Select(x => new ProductoPliego
                                     {
                                         Id_producto = Int32.Parse(x.Attribute("ProductID").Value),
                                         Copiasporpliego = x.Descendants("Placement").Count(),                                         
                                     }).ToList();
            #endregion

            this.ListarObjetos();
            #region Interacion con la capa de Datos
            //PliegoDAO dao = new PliegoDAO();

            //if (dao.insertar(_pliego, _productospliego))
            //{
            //    Console.WriteLine("Se inserto");
            //}
            //else
            //{
            //    Console.WriteLine("No se inserto");
            //    this.quitarExtension(name);
            //}


            #endregion

        }

        private void ListarObjetos()
        {
            Console.WriteLine("----------------------Pliego------------------------");
            Console.WriteLine(this._pliego);
            Console.WriteLine("---------------------------- ------------------------");
            Console.WriteLine("----------------------------Productos ------------------------");
            foreach (var producto in this._productospliego)
                Console.WriteLine(producto);
            Console.WriteLine("--------------------------------------------------------------");
        }


    }



}
