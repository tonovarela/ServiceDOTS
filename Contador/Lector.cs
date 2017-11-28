using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador
{
    public class Lector
    {

        protected string extension ;

        protected string ruta;

        public Lector(string ruta,string extension)
        {
            this.ruta = ruta;
            this.extension = extension;
        }

        protected FileInfo[] listarArchivos()
        {
            DirectoryInfo directorio = new DirectoryInfo(this.ruta);
            FileInfo[] archivos = directorio.GetFiles("*."+this.extension);
            return archivos;
        }

        protected bool sePuedeLeerArchivo(string filePath)
        {
            try
            {
                File.Open(filePath, FileMode.Open, FileAccess.Read).Dispose();
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        protected void EscribirError(string filePath, string detalle)
        {
            //Quita la extension y coloca el mensaje de error al final del cuerpo del archivo
            String archivoSP = filePath.Replace("." + this.extension, "");
            using (var wr = new StreamWriter(filePath, true, Encoding.UTF8))
            {

                var sb = new StringBuilder();
                sb.Append(",");
                sb.Append(",");
                sb.Append(",");
                sb = sb.Clear();
                sb.Append(String.Format("Error [ {0} ]", detalle));

                wr.WriteLine(sb.ToString());

            }
            File.Move(filePath, archivoSP);

        }
    }
}
