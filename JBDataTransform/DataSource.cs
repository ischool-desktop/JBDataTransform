using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JBDataTransform
{
    class DataSource : IDisposable
    {
        private SqlConnection Connection { get; set; }

        public DataSource(string connectionString)
        {
            string connstr = connectionString;

            Connection = new SqlConnection(connstr);
            Connection.Open();
        }

        public List<T> Execute<T>(string cmd)
            where T : ValueObjectBase, new()
        {
            List<T> records = new List<T>();

            using (SqlCommand objcmd = new SqlCommand(cmd, Connection))
            {
                using (SqlDataAdapter sqlda = new SqlDataAdapter(objcmd))
                {
                    DataTable table = new DataTable();
                    sqlda.Fill(table);

                    List<string> columns = new List<string>();
                    foreach (DataColumn column in table.Columns)
                    {
                        columns.Add(column.ColumnName);
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        T newobj = new T();
                        newobj.InitData(row, columns);
                        records.Add(newobj);
                    }
                }
            }

            return records;
        }

        #region IDisposable 成員

        public void Dispose()
        {
            try
            {
                Connection.Close();
            }
            catch
            {
            }
        }

        #endregion
    }
}
