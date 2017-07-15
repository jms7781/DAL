using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DataExportArgs : EventArgs
    {
        public TableInfo Table { get; set; }
        public TimeSpan Duration { get; set; }
        public Exception Error { get; set; }
        public DateTime ImportStop { get; set; }
        public int RowsImported { get; set; }
        public DateTime ImportStart { get; set; }
    }

    public class TableInfo
    {
        public string SourceTableName { get; set; }
        public string DestTableName { get; set; }

        public int BatchSize { get; set; } = 50000;
        public int CommandTimeout { get; set; } = 120;

        public string BuildSqlCommand(string table)
        {
            //return "select * from " + table;

            return BuildSqlCommand(table, "*");
        }

        public string BuildSqlCommand(string table, string columns)
        {
            return "select " + columns + " from " + table;
        }
    }

    public class DataExportOptions
    {
        public string SourceConnectionString { get; set; }
        public string DestConnectionString { get; set; }

        public List<TableInfo> Tables { get; set; } = new List<TableInfo>();
    }
}
