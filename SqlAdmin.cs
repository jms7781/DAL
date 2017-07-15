using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlAdmin
    {
        private string cs;
        private DAL.DataQuery<System.Data.SqlClient.SqlConnection> db;
        public SqlAdmin(string connectionString)
        {
            db = new DataQuery<System.Data.SqlClient.SqlConnection>(cs);
        }

        public void Backup(string path)
        {
            db.Execute("backup");
        }

        public void Restore(string path)
        {
            db.Execute("restore");
        }


    }
}
