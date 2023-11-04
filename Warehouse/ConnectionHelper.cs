using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            //Строка подключения к SQL SERVER
            return "Server=(localdb)\\MSSQLLocalDB;Database=Warehouse;Trusted_Connection=True;";
        }
    }

}
