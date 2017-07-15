using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DataQuery<TConnection> where TConnection : IDbConnection, new()
    {
        private string connectionString;
        private TConnection cnn;

        private TConnection NewConnection()
        {
            cnn = new TConnection();
            cnn.ConnectionString = connectionString;
            return cnn;
        }

        

        public DataQuery(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Execute(Action<IDbCommand> configureCommand)
        {
            return ExecuteInternal(configureCommand);
        }

        public int Execute(string sql)
        {
            return ExecuteInternal(c => c.CommandText = sql);
        }

        public int Execute(IDbCommand command)
        {
            return ExecuteInternal(c => c = command);
        }

        private int ExecuteInternal(Action<IDbCommand> configureCommand)
        {
            using (var cnn = NewConnection())
            using (var cmd = cnn.CreateCommand())
            {
                configureCommand(cmd);
                cmd.Connection = cnn;
                cnn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
        public DataTable QueryDataTable(Action<IDbCommand> configureCommand)
        {
            return QueryDataTableInternal(configureCommand);
        }

        public DataTable QueryDataTable(string sql)
        {
            return QueryDataTableInternal(c => c.CommandText = sql);
        }

        public DataTable QueryDataTable(IDbCommand command)
        {
            return QueryDataTableInternal(c => c = command);
        }

        private DataTable QueryDataTableInternal(Action<IDbCommand> configureCommand)
        {
            using (var cnn = NewConnection())
            using (var cmd = cnn.CreateCommand())
            {
                configureCommand(cmd);
                cmd.Connection = cnn;
                cnn.Open();

                var t = new DataTable();
                //t.Constraints.Clear();
                try
                {
                    t.Load(cmd.ExecuteReader());
                }
                catch (ConstraintException ex)
                {
                    t.Constraints.Clear();
                    var msg = ex.ToString();
                }
                
                return t;
            }
        }

        public T QueryValue<T>(Action<IDbCommand> configureCommand)
        {
            return QueryValueInternal<T>(configureCommand);
        }

        public T QueryValue<T>(string sql)
        {
            return QueryValueInternal<T>(c => c.CommandText = sql);
        }

        public T QueryValue<T>(IDbCommand command)
        {
            return QueryValueInternal<T>(c => c = command);
        }

        private T QueryValueInternal<T>(Action<IDbCommand> configureCommand)
        {
            using (var cnn = NewConnection())
            using (var cmd = cnn.CreateCommand())
            {
                configureCommand(cmd);
                cmd.Connection = cnn;
                cnn.Open();

                return (T)cmd.ExecuteScalar();
            }
        }

        public IEnumerable<IDataRecord> QueryDataRecord(Action<IDbCommand> configureCommand)
        {
            return QueryDataRecordInternal(configureCommand);
        }

        public IEnumerable<IDataRecord> QueryDataRecord(string sql)
        {
            return QueryDataRecordInternal(c => c.CommandText = sql);
        }

        public IEnumerable<IDataRecord> QueryDataRecord(IDbCommand command)
        {
            return QueryDataRecordInternal(c => c = command);
        }

        private IEnumerable<IDataRecord> QueryDataRecordInternal(Action<IDbCommand> configureCommand)
        {
            using (var cnn = NewConnection())
            using (var cmd = cnn.CreateCommand())
            {
                configureCommand(cmd);
                cmd.Connection = cnn;
                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader;
                    }
                }
            }
        }

        public IEnumerable<string[]> QueryStringArray(Action<IDbCommand> configureCommand)
        {
            return QueryStringArrayInternal(configureCommand);
        }

        public IEnumerable<string[]> QueryStringArray(string sql)
        {
            return QueryStringArrayInternal(c => c.CommandText = sql);
        }

        public IEnumerable<string[]> QueryStringArray(IDbCommand command)
        {
            return QueryStringArrayInternal(c => c = command);
        }

        private IEnumerable<string[]> QueryStringArrayInternal(Action<IDbCommand> configureCommand)
        {
            using (var cnn = NewConnection())
            using (var cmd = cnn.CreateCommand())
            {
                configureCommand(cmd);
                cmd.Connection = cnn;
                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] vals = new string[reader.FieldCount];

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            vals[i] = reader.IsDBNull(i) ? "" : reader[i].ToString();
                        }

                        yield return vals;
                    }
                }
            }
        }

        public IEnumerable<string> QueryString(Action<IDbCommand> configureCommand)
        {
            return QueryStringInternal(configureCommand);
        }

        public IEnumerable<string> QueryString(string sql)
        {
            return QueryStringInternal(c => c.CommandText = sql);
        }

        public IEnumerable<string> QueryString(IDbCommand command)
        {
            return QueryStringInternal(c => c = command);
        }

        private IEnumerable<string> QueryStringInternal(Action<IDbCommand> configureCommand)
        {
            using (var cnn = NewConnection())
            using (var cmd = cnn.CreateCommand())
            {
                configureCommand(cmd);
                cmd.Connection = cnn;
                cnn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        yield return reader.IsDBNull(0) ? "" : reader[0].ToString();
                    }
                }
            }
        }

        public bool Exists(Action<IDbCommand> configureCommand)
        {
            return ExistsInternal(configureCommand);
        }

        public bool Exists(string sql)
        {
            return ExistsInternal(c => c.CommandText = sql);
        }

        public bool Exists(IDbCommand command)
        {
            return ExistsInternal(c => c = command);
        }

        private bool ExistsInternal(Action<IDbCommand> configureCommand)
        {
            using (var cnn = NewConnection())
            using (var cmd = cnn.CreateCommand())
            {
                configureCommand(cmd);
                cmd.Connection = cnn;
                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

    }


}
