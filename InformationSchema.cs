using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DAL
{
    public class InformationSchema
    {
        private DAL.DataQuery<SqlConnection> db;
        public InformationSchema(string connectionString)
        {
            db = new DataQuery<SqlConnection>(connectionString);
        }

        //tables
        public List<DAL.Models.Table> GetTables()
        {
            var tables = new List<Models.Table>();

            var results = db.QueryStringArray("select * from INFORMATION_SCHEMA.TABLES");

            foreach(var result in results)
            {
                var table = new Models.Table();

                table.TABLE_CATALOG = result[0];
                table.TABLE_SCHEMA = result[1];
                table.TABLE_NAME = result[2];
                table.TABLE_TYPE = result[3];

                tables.Add(table);
            }
            return tables;
        }

        public List<DAL.Models.Column> GetColumns()
        {
            var list = new List<Models.Column>();
            var results = db.QueryStringArray("select * from INFORMATION_SCHEMA.COLUMNS");

            foreach (var result in results)
            {
                var data = new Models.Column();

                data.TABLE_CATALOG = result[0];
                data.TABLE_SCHEMA = result[1];
                data.TABLE_NAME = result[2];
                data.COLUMN_NAME = result[3];
                data.ORDINAL_POSITION = result[4];
                data.COLUMN_DEFAULT = result[5];
                data.IS_NULLABLE = result[6];
                data.DATA_TYPE = result[7];
                data.CHARACTER_MAXIMUM_LENGTH = result[8];
                data.CHARACTER_OCTET_LENGTH = result[9];
                data.NUMERIC_PRECISION = result[10];
                data.NUMERIC_PRECISION_RADIX = result[11];
                data.NUMERIC_SCALE = result[12];
                data.DATETIME_PRECISION = result[13];
                data.CHARACTER_SET_CATALOG = result[14];
                data.CHARACTER_SET_SCHEMA = result[15];
                data.CHARACTER_SET_NAME = result[16];
                data.COLLATION_CATALOG = result[17];
                data.COLLATION_SCHEMA = result[18];
                data.COLLATION_NAME = result[19];
                data.DOMAIN_CATALOG = result[20];
                data.DOMAIN_SCHEMA = result[21];
                data.DOMAIN_NAME = result[22];


                list.Add(data);
            }
            return list;
        }


        //columns

        //information_schema

        //views

        //sprocs

        //databases
    }
}
