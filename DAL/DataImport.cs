using System;

namespace DAL
{
    /// <summary>
    /// The base class containing all the events for all data imports.
    /// </summary>
    public abstract class DataImport
    {
        public event EventHandler<DataExportArgs> ImportProgress;
        public event EventHandler<DataExportArgs> ImportStarting;
        public event EventHandler<DataExportArgs> ImportComplete;
        public event EventHandler<DataExportArgs> ImportError;
        public event EventHandler<Exception> Error;

        protected DataExportArgs args;
        private Stopwatch timer;
        private string cs;

        protected DataImport()
        {
            cs = connectionString;
            timer = new Stopwatch();
        }

        protected virtual void OnImportError(TableInfo table, Exception ex)
        {
            args.Table = table;
            args.Duration = timer.Elapsed;
            args.Error = ex;
            ImportError?.Invoke(this, args);
        }

        protected virtual void OnImportStarting(TableInfo tableInfo)
        {
            timer.Restart();
            args = new DataExportArgs();
            args.ImportStart = DateTime.Now;
            args.Table = tableInfo;

            ImportStarting?.Invoke(this, args);
        }

        protected virtual void OnImportComplete()
        {
            timer.Stop();

            args.ImportStop = DateTime.Now;
            args.Duration = timer.Elapsed;
            args.RowsImported = TotalRowsImported();

            ImportComplete?.Invoke(this, args);
        }

        protected virtual void OnRowsCopied(DataExportArgs e)
        {
            ImportProgress?.Invoke(this, e);
        }

        protected virtual void OnError(Exception e)
        {
            Error?.Invoke(this, e);
        }
    }
}

