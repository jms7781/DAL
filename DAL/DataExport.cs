using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DAL
{
    public abstract class DataExport
    {
        public event EventHandler<DataExportArgs> ExportProgress;
        public event EventHandler<DataExportArgs> ExportStarting;
        public event EventHandler<DataExportArgs> ExportComplete;
        public event EventHandler<DataExportArgs> ExportError;
        public event EventHandler<Exception> Error;

        private Stopwatch timer;
        protected DataExportArgs args;
        //public DataExportOptions DataExportOptions { get; set; }

        public string SourceConnection { get; protected set; }
        public string TargetConnection { get; protected set; }

        public IEnumerable<TableInfo> ExportTableInfo { get; protected set; }

        //public int CommandTimeout { get; set; } = 30;

        protected DataExport()
        {
            //DataExportOptions = new DataExportOptions();
            timer = new Stopwatch();
        }

        protected DataExport(string sourceConnection, string targetConnection, IEnumerable<TableInfo> tableInfo) : this()
        {
            SourceConnection = sourceConnection;
            TargetConnection = targetConnection;
            ExportTableInfo = tableInfo;
        }

        protected virtual void OnExportError(TableInfo table, Exception ex)
        {
            args.Table = table;
            args.Duration = timer.Elapsed;
            args.Error = ex;
            ExportError?.Invoke(this, args);
        }

        protected virtual void OnExportStarting(TableInfo tableInfo)
        {
            timer.Restart();
            args = new DataExportArgs();
            args.ImportStart = DateTime.Now;
            args.Table = tableInfo;
      
            ExportStarting?.Invoke(this, args);
        }

        protected virtual void OnExportComplete()
        {
            timer.Stop();

            args.ImportStop = DateTime.Now;
            args.Duration = timer.Elapsed;
            args.RowsImported = TotalRowsImported();

            ExportComplete?.Invoke(this, args);
        }

        protected virtual void OnRowsCopied(DataExportArgs e)
        {
            ExportProgress?.Invoke(this, e);
        }

        protected virtual void OnError(Exception e)
        {
            Error?.Invoke(this, e);
        }

        protected virtual int TotalRowsImported()
        {
            using (var cnn = new SqlConnection(TargetConnection))
            using (var cmd = cnn.CreateCommand())
            {
                try
                {
                    cnn.Open();
                    cmd.CommandText = "SELECT COUNT(*) FROM " + args.Table.DestTableName;
                    return (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    OnError(ex);
                    return -1;
                }
            }
        }

        protected virtual void Truncate(string tableName)
        {
            using (var cnn = new SqlConnection(TargetConnection))
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

        protected virtual void ImportDataReader(DbDataReader reader, TableInfo table, bool truncate)
        {
            using (var bulk = new SqlBulkCopy(TargetConnection))
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
                    OnExportError(table, ex);
                }
            }
        }

        protected virtual void ImportDataRow(DataRow[] rows, TableInfo info, bool truncate)
        {
            using (var bulk = new SqlBulkCopy(TargetConnection))
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
                    OnExportError(info, ex);
                }
            }
        }

        protected virtual void ImportDataTable(DataTable table, TableInfo info, bool truncate)
        {
            using (var bulk = new SqlBulkCopy(TargetConnection))
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
                    OnExportError(info, ex);
                }
            }
        }



        public void BulkImportDataReader(DbDataReader reader, bool truncate)
        {
            foreach (var table in ExportTableInfo)
            {
                args = new DataExportArgs() { ImportStart = DateTime.Now, Table = table };
                OnExportStarting(table);
                try
                {
                    ImportDataReader(reader, table, truncate);

                }
                catch (Exception ex)    //source connection and reader
                {
                    OnExportError(table, ex);
                }
                OnExportComplete();
            }

        }

        //import via reader

        //import via datatable

        //import via datarow[]


    }
}
