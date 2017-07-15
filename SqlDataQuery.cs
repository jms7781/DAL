using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class SqlDataQuery : DataQuery<SqlConnection>
    {
        public SqlDataQuery(string connectionString) : base(connectionString)
        {

        }

        public DataTable SchemaDataTable(string table)
        {
            return QueryDataTable("select * from " + table + " where 1=0");
        }

        public SqlParameter CreateSqlParameter(string parameterName, SqlDbType dbType, object value)
        {
            var p = new SqlParameter(parameterName, dbType);
            p.Value = value;
            return p;
        }
    }
}
