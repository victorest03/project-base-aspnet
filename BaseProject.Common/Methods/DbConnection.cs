using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using BaseProject.Common.Model;

namespace BaseProject.Common.Methods
{
    public static class DbConnection
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;

        /// <summary>
        /// Coneccion rapida a la Base de Datos
        /// </summary>
        /// <param name="storedProcedureName">Nombre del Procedimiento Almacendado</param>
        /// <param name="parameters">Parametros del Procedimiento ListDistionary key,value</param>
        /// <param name="istransaction">Indicar si es una transaccion</param>
        /// <param name="connectionString">conexion a base de datos instanciada</param>
        /// <param name="autoClose">cerrar conexion automaticamente</param>
        /// <returns>
        /// Retorna int resultado de ExecuteNonQuery
        /// </returns>
        public static int SqlStoredProcedure(string storedProcedureName, List<Parameter> parameters, bool istransaction = false, SqlConnection connectionString = null, bool autoClose = false)
        {
            int result;
            var optionsTransac = new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted, Timeout = TransactionManager.MaximumTimeout };
            using (var t = istransaction ? new TransactionScope(TransactionScopeOption.Required, optionsTransac) : null)
            {
                using (var cnx = connectionString == null ? new SqlConnection(ConnectionString) : null)
                {
                    cnx?.Open();

                    if (connectionString?.State == ConnectionState.Closed) connectionString.Open();
                    var cmd = (cnx ?? connectionString).CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedureName;
                    if (parameters != null)
                        foreach (var p in parameters)
                            cmd.Parameters.AddWithValue(p.Name, p.Value);

                    result = cmd.ExecuteNonQuery();

                    if (autoClose) connectionString?.Close();
                }

                t?.Complete();
            }
            return result;
        }

        /// <summary>
        /// Coneccion rapida a la Base de Datos
        /// </summary>
        /// <param name="storedProcedureName">Nombre del Procedimiento Almacendado</param>
        /// <param name="parameters">Parametros del Procedimiento ListDistionary key,value</param>
        /// <param name="outparameters">Parametros de Salida List ModelParameter</param>
        /// <param name="istransaction">Indicar si es una transaccion</param>
        /// <param name="connectionString">conexion a base de datos instanciada</param>
        /// <param name="autoClose">cerrar conexion automaticamente</param>
        /// <returns>
        /// Retorna ListDistionary key,value con los parametros de Salida
        /// </returns>
        public static Dictionary<string, object> SqlStoredProcedureAndOut(string storedProcedureName, List<Parameter> parameters, List<Parameter> outparameters, bool istransaction = false, SqlConnection connectionString = null, bool autoClose = false)
        {
            var result = new Dictionary<string, object>();
            var optionsTransac = new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted, Timeout = TransactionManager.MaximumTimeout };
            using (var t = istransaction ? new TransactionScope(TransactionScopeOption.Required, optionsTransac) : null)
            {
                using (var cnx = connectionString == null ? new SqlConnection(ConnectionString) : null)
                {
                    cnx?.Open();
                    if (connectionString?.State == ConnectionState.Closed) connectionString.Open();
                    var cmd = (cnx ?? connectionString).CreateCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedureName;
                    if (parameters != null)
                        foreach (var p in parameters)
                            cmd.Parameters.AddWithValue(p.Name, p.Value);

                    foreach (var p in outparameters)
                    {
                        cmd.Parameters.Add(p.Name, p.Type,p.Size);
                        cmd.Parameters[p.Name].Direction = p.Direction;
                    }
                    cmd.ExecuteNonQuery();

                    foreach (var p in outparameters)
                        result[p.Name] = cmd.Parameters[p.Name].Value;

                    if (autoClose) connectionString?.Close();
                }

                t?.Complete();
            }
            return result;
        }

        /// <summary>
        /// Conexion rapida a Base de Datos con Transaction
        /// </summary>
        /// <param name="action">Metodo personalizado</param>
        /// <param name="sqlConnectionString">conexion a base de datos instanciada</param>
        public static void SqlInitTransaction(Action<SqlConnection> action, SqlConnection sqlConnectionString = null)
        {
            var optionsTransac = new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted, Timeout = TransactionManager.MaximumTimeout };
            using (var t = new TransactionScope(TransactionScopeOption.Required, optionsTransac))
            {
                using (var cnx = sqlConnectionString ?? new SqlConnection(ConnectionString))
                {
                    cnx.Open();

                    action.Invoke(cnx);

                    cnx.Close();
                }
                t.Complete();
            }
        }

        /// <summary>
        /// Coneccion rapida a la Base de Datos y Lectura de SQLReader
        /// </summary>
        /// <param name="storedProcedureName">Nombre del Procedimiento Almacendado</param>
        /// <param name="parameters">Parametros del Procedimiento List ModelParameter</param>
        /// <param name="action">Metodo de Logica de Negocio</param>
        /// <param name="readFirstRow">Leer solo la primera fila del resultado</param>
        public static void SqlStoredProcedureRead(string storedProcedureName, List<Parameter> parameters, Action<SqlDataReader> action, bool readFirstRow = false)
        {
            var backWhile = true;
            using (var cnx = new SqlConnection(ConnectionString))
            {
                cnx.Open();

                var cmd = cnx.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Name, p.Value);

                var reader = cmd.ExecuteReader();

                while (reader.Read() && backWhile)
                {
                    action?.Invoke(reader);
                    if (readFirstRow) backWhile = false;
                }
            }
        }

        /// <summary>
        /// Coneccion rapida a la Base de Datos y Retorna un DateTable
        /// </summary>
        /// <param name="storedProcedureName">Nombre del Procedimiento Almacendado</param>
        /// <param name="parameters">Parametros del Procedimiento ListDistionary key,value</param>
        public static DataTable SqlStoredProcedureToDataTable(string storedProcedureName, List<Parameter> parameters)
        {
            var dataTable = new DataTable();
            using (var cnx = new SqlConnection(ConnectionString))
            {
                cnx.Open();

                var cmd = cnx.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Name, p.Value);

                dataTable.Load(cmd.ExecuteReader());
            }

            return dataTable;
        }

        public static void OverrideTransactionScopeMaximumTimeout(TimeSpan timeOut)
        {

            var oSystemType = typeof(TransactionManager);

            var oCachedMaxTimeout = oSystemType.GetField("_cachedMaxTimeout", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var oMaximumTimeout = oSystemType.GetField("_maximumTimeout", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            oCachedMaxTimeout?.SetValue(null, true);
            oMaximumTimeout?.SetValue(null, timeOut);

        }

    }
}
