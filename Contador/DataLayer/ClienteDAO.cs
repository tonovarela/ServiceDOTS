using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class ClienteDAO : DAO
    {
        public void InsertarCliente(Cliente cliente)
        {
            this.context.Clientes.Add(new Clientes
            {
                id_cliente = cliente.Id_Cliente,
               
                calle = cliente.Calle,
                ciudad = cliente.Ciudad,
                codigopostal = cliente.CodigoPostal,
                compania = cliente.Compania,
                email = cliente.Email,
                estado = cliente.Estado,
                nombre = cliente.Nombre,
                pais = cliente.Pais,
                telefono = cliente.Telefono

            });
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }


        }
     
    }



}
