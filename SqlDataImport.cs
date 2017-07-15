using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace DAL
{
    /// <summary>
    /// Provides methods for importing data into a SqlServer database using SqlBulkCopy
    /// </summary>
    class SqlDataImport : DataImport
    {
        
        public SqlDataImport(string connectionString)
        {
            
        }

        public SqlDataImport(string server, string database)
        {
            //build connection string
            var cb = new SqlConnectionStringBuilder();
            cb.DataSource = server;
            cb.InitialCatalog = database;
            cb.IntegratedSecurity = true;
        }

        protected virtual void ImportDataReader(DbDataReader reader, TableInfo table, bool truncate)
        {
            using (var bulk = new SqlBulkCopy(cs))
            {
                bulk.BatchSize = table.BatchSize;
                bulk.NotifyAfter = table.BatchSize;
                bulk.DestinationTableName = table.DestTableName;

                foreach (var field in reader.Columns())
                {
                    bulk.ColumnMappings.Add(field, field);
                }

                try
                {
                    if (truncate)
                        Truncate(bulk.DestinationTableName);

                    bulk.WriteToServer(reader);
                }
                catch (Exception ex)
                {
                    OnImportError(table, ex);
                }
            }
        }

        protected virtual void ImportDataRow(DataRow[] rows, TableInfo info, bool truncate)
        {
            using (var bulk = new SqlBulkCopy(cs))
            {
                bulk.BatchSize = info.BatchSize;
                bulk.NotifyAfter = info.BatchSize;
                bulk.DestinationTableName = info.DestTableName;

                try
                {
                    if (truncate)
                        Truncate(bulk.DestinationTableName);

                    bulk.WriteToServer(rows);
                }
                catch (Exception ex)
                {
                    OnImportError(info, ex);
                }
            }
        }

        protected virtual void ImportDataTable(DataTable table, TableInfo info, bool truncate)
        {
            using (var bulk = new SqlBulkCopy(cs))
            {
                bulk.BatchSize = info.BatchSize;
                bulk.NotifyAfter = info.BatchSize;
                bulk.DestinationTableName = info.DestTableName;

                foreach (DataColumn col in table.Columns)
                {
                    bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                }

                try
                {
                    if (truncate)
                        Truncate(bulk.DestinationTableName);

                    bulk.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    OnImportError(info, ex);
                }
            }
        }

        protected virtual void Truncate(string tableName)
        {
            using (var cnn = new SqlConnection(cs))
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = "TRUNCATE TABLE " + tableName;
                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }
            }
        }



    }
}
