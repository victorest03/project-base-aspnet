using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Common.Methods
{
    public static class DbConnection
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;

        /// <summary>
        /// Coneccion rapida a la Base de Datos
        /// </summary>
        /// <param name="storedProcedureName">Nombre del Procedimiento Almacendado</param>
        /// <param name="parameters">Parametros del Procedimiento ListDistionary key,value</param>
        /// <returns>
        /// Retorna int resultado de ExecuteNonQuery
        /// </returns>
        public static int SqlStoredProcedure(string storedProcedureName, Dictionary<string, string> parameters)
        {
            int result;
            using (var cnx = new SqlConnection(ConnectionString))
            {
                cnx.Open();

                var cmd = cnx.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        /// <summary>
        /// Coneccion rapida a la Base de Datos y Lectura de SQLReader
        /// </summary>
        /// <param name="storedProcedureName">Nombre del Procedimiento Almacendado</param>
        /// <param name="parameters">Parametros del Procedimiento ListDistionary key,value</param>
        /// <param name="action">Metodo de Logica de Negocio</param>
        public static List<T> SqlStoredProcedureRead<T>(string storedProcedureName, Dictionary<string, string> parameters, Action<SqlDataReader,List<T>> action)
        {
            var ls = new List<T>();
            using (var cnx = new SqlConnection(ConnectionString))
            {
                cnx.Open();

                var cmd = cnx.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    action?.Invoke(reader,ls);
                }
            }

            return ls;
        }

        /// <summary>
        /// Coneccion rapida a la Base de Datos y Retorna un DateTable
        /// </summary>
        /// <param name="storedProcedureName">Nombre del Procedimiento Almacendado</param>
        /// <param name="parameters">Parametros del Procedimiento ListDistionary key,value</param>
        public static DataTable SqlStoredProcedureToDataTable(string storedProcedureName, Dictionary<string, string> parameters)
        {
            var dataTable = new DataTable();
            using (var cnx = new SqlConnection(ConnectionString))
            {
                cnx.Open();

                var cmd = cnx.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value);

                dataTable.Load(cmd.ExecuteReader());
            }

            return dataTable;
        }


    }
}
