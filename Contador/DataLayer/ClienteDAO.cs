using Contador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class ClienteDAO :DAO
    {      
        public void InsertarCliente(Cliente cliente)
        {                                                   
               this.context.Clientes.Add(new Clientes
                {
                    ClienteID = cliente.ClienteID,
                    Nombre = cliente.Nombre,
                    Calle = cliente.Calle,
                    CodigoPostal = cliente.CodigoPostal,
                    Compania = cliente.Compania,
                    Email = cliente.Email,
                    Pais = cliente.Pais,
                    Estado = cliente.Estado,
                    Telefono = cliente.Telefono,
                    Ciudad=cliente.Ciudad
                    
                });
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                }
            

        }

        public Cliente getClienteID(int ClienteID)
        {
            Cliente cliente = null;            
                var data = context.Clientes.First(x => x.ClienteID == ClienteID);
                if (data == null)
                {
                    cliente = new Cliente
                    {
                        ClienteID = data.ClienteID,
                        Calle = data.Calle,
                        CodigoPostal = data.CodigoPostal,
                        Compania = data.Compania,
                        Email = data.Email,
                        Estado = data.Estado,
                        Nombre = data.Nombre,
                        Pais = data.Pais,
                        Telefono = data.Telefono,
                        Ciudad=data.Ciudad
                    };
                }            
            return cliente;
        }



    }



}
