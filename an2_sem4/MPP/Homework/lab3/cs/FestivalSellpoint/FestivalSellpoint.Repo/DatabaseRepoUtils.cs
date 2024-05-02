using FestivalSellpoint.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;

namespace FestivalSellpoint.Repo
{
    public abstract class DatabaseRepoUtils<ID, E> where E:Entity<ID>
    {
        private static readonly ILog log = LogManager.GetLogger("DatabaseRepoUtils");

        IDictionary<string, string> Properties;

        protected DatabaseRepoUtils(IDictionary<string, string> props)
        {
            log.Info("Creating DatabaseRepoUtils ");
            Properties = props;
        }

        protected abstract E DecodeReader(IDataReader reader);

        private IDbCommand CreateCommand(IDbConnection con, string sql, Dictionary<string, object> parameters = null)
        {            
            var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                foreach (var arg in parameters)
                {
                    var param = cmd.CreateParameter();
                    param.ParameterName = arg.Key;
                    param.Value = arg.Value;
                    cmd.Parameters.Add(param);
                }
            }
            return cmd;
        }
            
        protected int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            log.Info($"Executing NonQuery: {sql}");
            var con = DbConnectionUtils.getConnection(Properties);
            using (var cmd = CreateCommand(con, sql, parameters))             
                return cmd.ExecuteNonQuery();            
        }

        protected IEnumerable<E> Select(string sql, Dictionary<string, object> parameters = null)
        {            
            log.Info($"Executing Select: {sql}");
            var con = DbConnectionUtils.getConnection(Properties);
            using (var cmd = CreateCommand(con, sql, parameters))
            {
                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        yield return DecodeReader(reader);
                    }
                }
            }
            log.Info($"Done Select: {sql}");
            yield break;
        }

        protected E SelectFirst(string sql, Dictionary<string, object> parameters = null)
        {
            var con = DbConnectionUtils.getConnection(Properties);
            using (var cmd = CreateCommand(con, sql, parameters))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return default;
                    return DecodeReader(reader);                 
                }
            }            
        }
    }
}
