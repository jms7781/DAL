using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Odbc;

namespace DAL
{
    public class Odbc
    {
        public Odbc(string connectionString)
        {
            _cs = connectionString;
        }


        public IEnumerable<IDataRecord> QueryEnumerable (string sql)
        {
            var cnn = new OdbcConnection(_cs);
            var cmd = new OdbcCommand(sql);

            return Engine.QueryEnumerable(cnn, cmd);
        }

        public DataTable QueryDataTable (string sql)
        {
            var cnn = new OdbcConnection(_cs);
            var cmd = new OdbcCommand(sql);

            return Engine.QueryDataTable(cnn, cmd);
        }

        public IEnumerable<string[]> QueryEnumerableAsString (string sql)
        {
            var cnn = new OdbcConnection(_cs);
            var cmd = new OdbcCommand(sql);

            return Engine.QueryEnumerableAsString(cnn, cmd);
        }

        public OdbcDataAdapter GetDataAdapter(string sql)
        {
            var da = new OdbcDataAdapter(sql, _cs);
            var cb = new OdbcCommandBuilder(da);

            return da;
        }

        private string _cs;
    }
}
