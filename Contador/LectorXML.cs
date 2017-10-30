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

    public class LectorXML
    {

        String directorioIMP = @"C:\Desarrollo\IMP";
        String DirectorioSalida = @"C:\Desarrollo\Salida";
        public void LeerArchivos()
        {
            String info;
            foreach (FileInfo item in this.listarArchivosIMP())
            {    
                //Se lee la informacion del archivo          
                info=this.LeerXML(item.FullName);
                //Se envia a la carpeta de procesado
                this.crearArchivo(info);                
                 item.Delete();
            }
        }
        

        
        private FileInfo[] listarArchivosIMP()
        {
            DirectoryInfo directorio = new DirectoryInfo(this.directorioIMP);
            FileInfo[] archivos = directorio.GetFiles("*.xml");
            return archivos;
        }
        private String LeerXML(String path)
        {
            StringBuilder resultado = new StringBuilder();
            foreach (XElement item in XElement.Load(path).Elements("Brand"))
            {
                resultado.AppendLine(item.Attribute("name").Value);
                foreach (XElement item2 in item.Elements("product"))
                    resultado.AppendLine(" " + item2.Attribute("name").Value);
            }
            return resultado.ToString();
        }

        private void crearArchivo(String info)
        {
            string path = this.DirectorioSalida+"\\"+ String.Format("ArchivoProcesado_{0}_{1}_{2}_{3}_{4}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Minute, DateTime.Now.Millisecond);
            File.AppendAllLines(path, new [] {info});
            Thread.Sleep(1000);
        }
        
    }

    

}
