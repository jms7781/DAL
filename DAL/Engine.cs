using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//using System.Data.Common;


namespace DAL
{
    internal static class Engine
    {
        internal static IEnumerable<IDataRecord> QueryEnumerable(IDbConnection cnn, IDbCommand cmd)
        {
            using (cnn)
            using (cmd)
            {
                cmd.Connection = cnn;
                cnn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    yield return reader;
                }
            }
        }

        internal static IEnumerable<string[]> QueryEnumerableAsString(IDbConnection cnn, IDbCommand cmd)
        {
            using (cnn)
            using (cmd)
            {
                cmd.Connection = cnn;
                cnn.Open();

                var reader = cmd.ExecuteReader();
                

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

        internal static DataTable QueryDataTable(IDbConnection cnn, IDbCommand cmd)
        {
            using (cnn)
            using (cmd)
            {
                cmd.Connection = cnn;
                cnn.Open();

                var t = new DataTable();

                try
                {
                    t.Load(cmd.ExecuteReader());
                }
                catch (ConstraintException)
                {
                    //swallow this. we just want the data
                }

                return t;
            }

        }

        internal static int Execute(IDbConnection cnn, IDbCommand cmd)
        {
            using (cnn)
            using (cmd)
            {
                cmd.Connection = cnn;
                cnn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        
    }
}
