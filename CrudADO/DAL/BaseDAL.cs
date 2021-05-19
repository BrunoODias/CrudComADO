using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;

namespace CrudADO.DAL
{
    public class BaseDAL
    {
        private string _connectionString { get; set; }
        public BaseDAL(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        private SqlConnection createConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
        private SqlCommand createCommand(string comando, SqlConnection sqlConn, SqlParameter parameter = null)
        {
            var command = new SqlCommand(comando);
            addParameter(command, parameter);
            command.Connection = sqlConn;
            return command;
        }
        private SqlCommand createCommand(string comando, SqlConnection sqlConn, List<SqlParameter> parameters)
        {
            var command = new SqlCommand(comando);
            addParameter(command, parameters);
            command.Connection = sqlConn;
            return command;
        }
        private void addParameter(SqlCommand command, SqlParameter parameter)
        {
            if (parameter != null && parameter.Value != null)
            {
                command.Parameters.Add(parameter);
            }
        }
        private void addParameter(SqlCommand command, List<SqlParameter> parameters)
        {
            foreach (var current in parameters)
            {
                addParameter(command, current);
            }
        }
        public dynamic ExecuteReader(string comando, SqlParameter parameter = null)
        {
            using (var conn = createConnection())
            {
                using (var cmd = createCommand(comando, conn, parameter))
                {
                    return proccessReaderResponse(cmd.ExecuteReader());
                }
            }
        }
        public dynamic ExecuteReader(string comando, List<SqlParameter> parameters)
        {
            using (var conn = createConnection())
            {
                using (var cmd = createCommand(comando, conn, parameters))
                {
                    return proccessReaderResponse(cmd.ExecuteReader());
                }
            }
        }
        private List<dynamic> proccessReaderResponse(SqlDataReader reader)
        {
            if (reader == null)
                return null;

            if (reader.HasRows == false)
                return null;

            List<string> cols = new List<string>();
            foreach (DataRow row in reader.GetSchemaTable().Rows) cols.Add(row.Field<string>("ColumnName"));

            var retorno = new List<dynamic>();
            while (reader.Read())
            {
                ExpandoObject obj = new ExpandoObject();
                foreach (var current in cols)
                {
                    obj.TryAdd(current, reader[current]);
                }
                retorno.Add(obj);
            }
            return retorno;
        }
        public void ExecuteNonQuery(string comando, SqlParameter parameter = null)
        {
            using (var conn = createConnection())
            {
                using (var cmd = createCommand(comando, conn, parameter))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ExecuteNonQuery(string comando, List<SqlParameter> parameters = null)
        {
            using (var conn = createConnection())
            {
                using (var cmd = createCommand(comando, conn, parameters))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public string ExecuteScalar(string comando, SqlParameter parameter = null)
        {
            using (var conn = createConnection())
            {
                using (var cmd = createCommand(comando, conn, parameter))
                {
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }
        public string ExecuteScalar(string comando, List<SqlParameter> parameters = null)
        {
            using (var conn = createConnection())
            {
                using (var cmd = createCommand(comando, conn, parameters))
                {
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }
    }
}
