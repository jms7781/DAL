using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// Used to transer data between two sql server databases
    /// </summary>
    public class SqlDataExport : DataExport
    {

        public SqlDataExport() : base()  { }

        public SqlDataExport(string sourceConnection, string targetConnection, IEnumerable<TableInfo> tableInfo) : base(sourceConnection, targetConnection, tableInfo) { }

        /// <summary>
        /// Performs a SqlBulkCopy to update the destination table. The destination table is truncated prior to the update
        /// </summary>
        public void BulkImportDataReader(bool truncate)
        {
            foreach (var table in ExportTableInfo)
            {
                args = new DataExportArgs() { ImportStart = DateTime.Now, Table = table };
                OnExportStarting(table);

                using (var connection = new SqlConnection(SourceConnection))
                using (var command = connection.CreateCommand())
                {
                    command.CommandTimeout = table.CommandTimeout;
                    command.CommandText = "SELECT * FROM " + table.SourceTableName;
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            ImportDataReader(reader, table, truncate);
                        }
                    }
                    catch (Exception ex)    //source connection and reader
                    {
                        OnExportError(table, ex);
                    }
                }
                OnExportComplete();
            }

        }




    }
}
