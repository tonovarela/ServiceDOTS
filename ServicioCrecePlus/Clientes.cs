//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicioCrecePlus
{
    using System;
    using System.Collections.Generic;
    
    public partial class Clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clientes()
        {
            this.Ordenes = new HashSet<Ordenes>();
        }
    
        public int id_cliente { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string compania { get; set; }
        public string calle { get; set; }
        public string codigopostal { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string telefono { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordenes> Ordenes { get; set; }
    }
}