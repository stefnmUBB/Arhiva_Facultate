using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBD.Lab2
{
    internal class DbUtils
    {
        SqlDataAdapter DataAdapter = new SqlDataAdapter();
        SqlConnection Connection;

        public DbUtils(SqlConnection connection)
        {
            Connection = connection;            
        }

        public void Select(DataSet ds, string sql, params (string, object)[] parameters)
        {            
            DataAdapter.SelectCommand = new SqlCommand(sql, Connection);
            foreach (var p in parameters)
                DataAdapter.SelectCommand.Parameters.AddWithValue(p.Item1, p.Item2);
            ds.Clear();
            DataAdapter.Fill(ds);            
        }

        public IEnumerable<(string TableColumn, string ReferencedTable, string ReferencedColumn)> GetForeignKeys(string tableName)
        {
            var ds = new DataSet();
            Select(ds, "SELECT * FROM dbo.GetForeignKeys(@tableName)", ("@tableName", tableName));
            foreach (DataRow row in ds.Tables[0].Rows)
                yield return (row["TableColumn"] as string, row["ReferencedTable"] as string, row["ReferencedColumn"] as string);
            yield break;
        }

        public IEnumerable<(string Name, string Type, int CharMaxLength)> GetColumns(string tableName)
        {
            var ds = new DataSet();
            Select(ds, "SELECT COLUMN_NAME, DATA_TYPE, ISNULL(CHARACTER_MAXIMUM_LENGTH,0) CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName",
                ("@tableName", tableName));
            foreach (DataRow row in ds.Tables[0].Rows)
                yield return (
                    row["COLUMN_NAME"] as string,
                    row["DATA_TYPE"] as string,
                    Convert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"]));
            yield break;
        }

        public string GetPrimaryKeyColumn(string tableName)
        {
            var ds = new DataSet();
            Select(ds, "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                "WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), " +
                "'IsPrimaryKey') = 1 AND TABLE_NAME = @tableName AND TABLE_SCHEMA = 'dbo'",
                ("@tableName", tableName));
            return ds.Tables[0].Rows[0][0] as string;
        }

        void ExecuteNonQuery(ref SqlCommand command, string sql, List<(string Parameter, object Value)> parameters)
        {
            try
            {
                Connection.Open();
                command = new SqlCommand(sql, Connection);

                foreach(var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Parameter, param.Value);
                }
                command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
        }

        public void Insert(string tableName, List<(string Field, object Value)> attributes)
        {
            var cols = attributes.Select(a => a.Field);
            var vals = attributes.Select(a => a.Value);
            var prms = cols.Select(c => $"@{c}");

            var sql = $"INSERT INTO {tableName} ({string.Join(", ", cols)}) VALUES ({string.Join(", ", prms)})";

            Console.WriteLine(sql);
            var command = DataAdapter.InsertCommand;
            ExecuteNonQuery(ref command, sql, prms.Zip(vals, (x, y) => (x, y)).ToList());
        }

        public void Update(string tableName, List<(string Field, object Value)> attributes, string pk, int pkId)
        {
            var cols = attributes.Select(a => a.Field);
            var vals = attributes.Select(a => a.Value);
            var prms = cols.Select(c => $"@{c}");

            var pairs = cols.Zip(prms, (x, y) => (x, y));

            var sql = $"UPDATE {tableName} SET {string.Join(", ", pairs.Select(x => $"{x.x}={x.y}"))} " +
                $"WHERE {pk} = @__pk";            

            Console.WriteLine(sql);
            var command = DataAdapter.UpdateCommand;
            ExecuteNonQuery(ref command, sql, prms.Append("@__pk").Zip(vals.Append(pkId), (x, y) => (x, y)).ToList());
        }

        public void Delete(string tableName, string pk, int pkId)
        {
            var sql = $"DELETE FROM {tableName} WHERE {pk} = @__pk";
            var command = DataAdapter.DeleteCommand;
            ExecuteNonQuery(ref command, sql, new List<(string, object)> { ("@__pk", pkId) });
        }
    }
}
