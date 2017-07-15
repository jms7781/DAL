using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public static class Extensions
    {
        public static SqlParameter CreateSqlParameter(string parameterName, SqlDbType dbType, object value)
        {
            var p = new SqlParameter(parameterName, dbType);
            p.Value = value;
            return p;
        }

        public static IEnumerable<string> Columns(this IDataRecord record)
        {
            for (int i = 0; i < record.FieldCount; i++) yield return record.GetName(i);
        }

        public static string SQL(this IDbCommand cmd)
        {
            var sql = new StringBuilder(cmd.CommandText);

            foreach(IDataParameter p in cmd.Parameters)
            {
                switch(p.DbType)
                {
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.Date:
                    case DbType.DateTime:
                    case DbType.DateTime2:
                    case DbType.DateTimeOffset:
                    case DbType.Guid:
                    case DbType.String:
                    case DbType.StringFixedLength:
                    case DbType.Time:
                    case DbType.Xml:
                        sql.Replace(p.ParameterName, string.Format("'{0}'", p.Value.ToString().Replace("'", "''")));
                        break;
                    default:
                        //number
                        sql.Replace(p.ParameterName, p.Value.ToString());
                        break;
                }
            }
            return sql.ToString();
        }

        public static void GenerateCommands(this SqlDataAdapter da)
        {
            var cb = new SqlCommandBuilder(da);

            da.UpdateCommand = cb.GetUpdateCommand(true);
            da.InsertCommand = cb.GetInsertCommand(true);
            da.DeleteCommand = cb.GetDeleteCommand(true);
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            for (int i = 0; i <= props.Count - 1; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];

            foreach (T item in data)
            {
                for (int i = 0; i <= values.Length - 1; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            //return the table
            // afsdalfsadlfsadjkl
            return table;
        }

        public static String ParameterValueForSQL(this SqlParameter sp)
        {
            String retval = "";

            switch (sp.SqlDbType)
            {
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.Time:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    retval = "'" + sp.Value.ToString().Replace("'", "''") + "'";
                    break;

                case SqlDbType.Bit:
                    retval = (bool)sp.Value ? "1" : "0";
                    break;

                default:
                    retval = sp.Value.ToString().Replace("'", "''");
                    break;
            }

            return retval;
        }

        public static String CommandAsSql(this SqlCommand sc)
        {
            StringBuilder sql = new StringBuilder();
            Boolean FirstParam = true;

            sql.AppendLine("use " + sc.Connection.Database + ";");
            switch (sc.CommandType)
            {
                case CommandType.StoredProcedure:
                    sql.AppendLine("declare @return_value int;");

                    foreach (SqlParameter sp in sc.Parameters)
                    {
                        if ((sp.Direction == ParameterDirection.InputOutput) || (sp.Direction == ParameterDirection.Output))
                        {
                            sql.Append("declare " + sp.ParameterName + "\t" + sp.SqlDbType.ToString() + "\t= ");

                            sql.AppendLine(((sp.Direction == ParameterDirection.Output) ? "null" : sp.ParameterValueForSQL()) + ";");

                        }
                    }

                    sql.AppendLine("exec [" + sc.CommandText + "]");

                    foreach (SqlParameter sp in sc.Parameters)
                    {
                        if (sp.Direction != ParameterDirection.ReturnValue)
                        {
                            sql.Append((FirstParam) ? "\t" : "\t, ");

                            if (FirstParam) FirstParam = false;

                            if (sp.Direction == ParameterDirection.Input)
                                sql.AppendLine(sp.ParameterName + " = " + sp.ParameterValueForSQL());
                            else

                                sql.AppendLine(sp.ParameterName + " = " + sp.ParameterName + " output");
                        }
                    }
                    sql.AppendLine(";");

                    sql.AppendLine("select 'Return Value' = convert(varchar, @return_value);");

                    foreach (SqlParameter sp in sc.Parameters)
                    {
                        if ((sp.Direction == ParameterDirection.InputOutput) || (sp.Direction == ParameterDirection.Output))
                        {
                            sql.AppendLine("select '" + sp.ParameterName + "' = convert(varchar, " + sp.ParameterName + ");");
                        }
                    }
                    break;
                case CommandType.Text:
                    sql.AppendLine(sc.CommandText);
                    break;
            }

            return sql.ToString();
        }


        /// <summary>
        /// Performs a pivot on a DataTable. The column names will be the first entry in each row. Each column represents the row number.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable Pivot(this DataTable table)
        {
            var pivot = new DataTable(table.TableName);
            var colNames = table.Columns.Cast<DataColumn>().OrderBy(o => o.Ordinal);

            //create columns
            pivot.Columns.Add("Column Name");
            for(int i = 0; i < table.Rows.Count; i++)
            {
                pivot.Columns.Add("Row " + (i + 1).ToString());
            }

            //add rows
            foreach(var col in colNames)
            {
                var row = pivot.NewRow();
                row[0] = col;
               
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var ix = i + 1;
                    row[ix] = table.Rows[i][col];
                }

                pivot.Rows.Add(row);
            }

            return pivot;
        }
    }


}
