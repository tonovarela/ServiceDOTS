using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador.DataLayer
{
    public class DAO
    {
        public DOTSEntities context;


        public DAO()
        {
            context = new DOTSEntities();
        }

    }
}
