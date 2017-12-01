using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class DAO
    {
        public CrecePlusEntities context;


        public DAO()
        {
            context = new CrecePlusEntities();
        }


        public  bool CheckConnection()
        {
            try
            {
                context.Database.Connection.Open();
                context.Database.Connection.Close();
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }

    }
}
