using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DAnalytics.UTIL
{
    /// <summary>
    /// Utility Layer class for managing all the Database interactions 
    /// </summary>
    public sealed class SqlHelper
    {
        #region Member Variables

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private SqlHelper()
        {

        }

        #endregion

        #region Execute Non Query

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connectionDetails, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionDetails, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionDetails))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connectionDetails, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connectionDetails, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            int val = 0;
            if (trans != null)
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            return val;
        }

        #endregion

        #region Execute Reader

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connectionDetails, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connectionDetails, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionDetails, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection connection = new SqlConnection(connectionDetails);
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connectionDetails, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            SqlDataReader rdr = null;
            if (trans != null)
            {
                try
                {
                    PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                }
                catch
                {
                    throw;
                }
            }

            return rdr;
        }

        #endregion

        #region Execute Scalar

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connectionDetails, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionDetails, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionDetails))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connectionDetails, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();

            return val;
        }

        #endregion

        #region Execute Query
        /// <summary>
        /// Execute a command
        /// </summary>
        /// <param name="connectionDetails"> connection string</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="commandText">CommandText of the command object</param>
        /// <param name="commandParameters">All the command parameters</param>
        /// <returns> The result of Execution (dataset)</returns>
        public static DataSet ExecuteDataset(string connectionDetails, CommandType commandType,
            string commandText, params SqlParameter[] commandParameters)
        {

            using (SqlConnection connection = new SqlConnection(connectionDetails))
            {
                // Create a command and prepare it for execution
                SqlCommand command = new SqlCommand();

                PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet);

                // Detach the SqlParameters from the command object, so they can be used again
                command.Parameters.Clear();

                return dataSet;
            }
        }

        /// <summary>
        /// Execute a command for a given timeout period.
        /// </summary>
        /// <param name="connectionDetails"> connection string</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="commandText">CommandText of the command object</param>
        /// <param name="connectionTimeout">Connection timeout for the command object</param>
        /// <param name="commandParameters">All the command parameters</param>
        /// <returns> The result of Execution (dataset)</returns>
        public static DataSet ExecuteDataset(string connectionDetails, CommandType commandType,
            string commandText, int connectionTimeout, params SqlParameter[] commandParameters)
        {

            using (SqlConnection connection = new SqlConnection(connectionDetails))
            {
                // Create a command and prepare it for execution
                SqlCommand command = new SqlCommand();

                //Set the command timeout.
                command.CommandTimeout = connectionTimeout;

                PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet);

                // Detach the SqlParameters from the command object, so they can be used again
                command.Parameters.Clear();

                return dataSet;
            }
        }

        #endregion

        #region Cache Parameters

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="commandParameters">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        #endregion

        #region Prepare Command

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="connection">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="commandParameters">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection connection, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            cmd.Connection = connection;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (commandParameters != null)
            {
                foreach (SqlParameter parm in commandParameters)
                    if (parm != null)
                    {
                        cmd.Parameters.Add(parm);
                    }
            }
        }

        #endregion

        #region Create Parameter

        /// <summary>
        /// CreateParameter
        /// </summary>
        /// <param name="dataType">An object of SqlDbType</param>
        /// <param name="direction">An object of ParameterDirection</param>
        /// <param name="name">A string that denotes name</param>
        /// <param name="dataValue">an instance of object type</param>
        /// <returns>SqlParameter</returns>
        public static SqlParameter CreateParameter(SqlDbType dataType, ParameterDirection direction, string name, object dataValue)
        {
            SqlParameter oParameter = new SqlParameter(name, dataType);

            oParameter.Direction = direction;

            if (dataValue == DBNull.Value)
            {
                oParameter.Value = DBNull.Value;
                return oParameter;
            }


            if (dataValue == null)
            {
                oParameter.Value = DBNull.Value;
                return oParameter;
            }

            switch (dataType)
            {
                case SqlDbType.Decimal:
                    if ((dataValue.ToString()).Length == 0)
                        oParameter.Value = DBNull.Value;
                    else
                        oParameter.Value = dataValue;
                    break;
                case SqlDbType.Float:
                    if ((dataValue.ToString()).Length == 0)
                        oParameter.Value = DBNull.Value;
                    else
                        oParameter.Value = dataValue;
                    break;
                case SqlDbType.SmallInt:
                    if ((dataValue.ToString()).Length == 0)
                        oParameter.Value = DBNull.Value;
                    else
                        oParameter.Value = Convert.ToInt16(dataValue);
                    break;
                case SqlDbType.Int:
                    if ((dataValue.ToString()).Length == 0)
                        oParameter.Value = DBNull.Value;
                    else
                        oParameter.Value = Convert.ToInt32(dataValue);
                    break;
                case SqlDbType.DateTime:
                    if ((dataValue.ToString()).Length == 0 || dataValue.ToString() == "//")
                        oParameter.Value = DBNull.Value;
                    else
                        oParameter.Value = dataValue;
                    break;
                case SqlDbType.Bit:
                    oParameter.Value = Convert.ToBoolean(dataValue.ToString());
                    break;
                case SqlDbType.VarChar:
                    if (dataValue != DBNull.Value)
                    {
                        if ((dataValue.ToString()).Length == 0)
                            oParameter.Value = DBNull.Value;
                        else
                            oParameter.Value = dataValue.ToString();
                    }
                    break;
                case SqlDbType.Text:
                    if (dataValue != DBNull.Value)
                    {
                        if ((dataValue.ToString()).Length == 0)
                            oParameter.Value = DBNull.Value;
                        else
                            oParameter.Value = dataValue.ToString();
                    }

                    break;
                case SqlDbType.UniqueIdentifier:
                    if (dataValue != DBNull.Value)
                    {
                        if ((dataValue.ToString()).Length == 0)
                            oParameter.Value = DBNull.Value;
                        else
                            oParameter.Value = dataValue;
                    }
                    break;
                case SqlDbType.BigInt:
                    if (dataValue != DBNull.Value)
                    {
                        if ((dataValue.ToString()).Length == 0)
                            oParameter.Value = DBNull.Value;
                        else
                            oParameter.Value = Convert.ToInt64(dataValue);
                    }
                    break;
                case SqlDbType.Money:
                    if (dataValue != DBNull.Value)
                    {
                        if ((dataValue.ToString()).Length == 0)
                            oParameter.Value = DBNull.Value;
                        else
                            oParameter.Value = Convert.ToDecimal(dataValue);
                    } break;
            }

            return oParameter;
        }

        /// <summary>
        /// Create sqlprameter object
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="type"></param>
        /// <param name="paramDir"></param>
        /// <returns></returns>
        public static SqlParameter CreateParameter(string parameterName, object parameterValue, SqlDbType type, ParameterDirection paramDir)
        {
            SqlParameter sqlParam = new SqlParameter();
            sqlParam.ParameterName = parameterName;
            sqlParam.Direction = paramDir;
            sqlParam.Value = parameterValue;
            sqlParam.SqlDbType = type;

            return sqlParam;

        }

        /// <summary>
        /// Create sqlprameter object
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="type"></param>
        /// <param name="paramDir"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static SqlParameter CreateParameter(string parameterName, object parameterValue, SqlDbType type, ParameterDirection paramDir, int size)
        {
            SqlParameter sqlParam = new SqlParameter();
            sqlParam.ParameterName = parameterName;
            sqlParam.Direction = paramDir;
            sqlParam.Value = parameterValue;
            sqlParam.SqlDbType = type;
            sqlParam.Size = size;
            return sqlParam;

        }

        #endregion

        #region Fill DataSet

        /// <summary>
        /// Execute the command and fill the given dataset.
        /// </summary>
        /// <param name="connectionDetails">Connection string.</param>
        /// <param name="commandType">Connection type.</param>
        /// <param name="commandText">Command text of the command object.</param>
        /// <param name="dataSet">Dataset to be filled.</param>
        /// <param name="tableName">Table name to fill the table.</param>
        /// <param name="commandParameters">All the command parameters.</param>
        public static void FillDataSet(string connectionDetails, CommandType commandType, string commandText, DataSet dataSet, string tableName, params SqlParameter[] commandParameters)
        {

            using (SqlConnection connection = new SqlConnection(connectionDetails))
            {
                // Create a command and prepare it for execution
                SqlCommand command = new SqlCommand();

                PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

                //Fill the given dataset.
                sqlDataAdapter.Fill(dataSet, tableName);

                // Detach the SqlParameters from the command object, so they can be used again
                command.Parameters.Clear();
            }
        }

        #endregion
    }
}
