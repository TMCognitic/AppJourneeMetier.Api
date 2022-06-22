using System.Data;

namespace AppJourneeMetier.Api.Tools
{
    public static class DbConnectionExtensions
    {
        public static object? ExecuteScalar(this IDbConnection dbConnection, Command command)
        {
            ArgumentNullException.ThrowIfNull(command);

            using (IDbCommand dbCommand = dbConnection.CreateCommand(command))
            {
                return dbConnection.ExecuteScalar(dbCommand);
            }
        }

        public static object? ExecuteScalar(this IDbConnection dbConnection, IDbCommand dbCommand)
        {
            ArgumentNullException.ThrowIfNull(dbCommand);

            dbConnection.Open();
            object? result = dbCommand.ExecuteScalar();
            return (result is DBNull) ? null : result;            
        }

        public static int ExecuteNonQuery(this IDbConnection dbConnection, Command command)
        {
            ArgumentNullException.ThrowIfNull(command);

            using (IDbCommand dbCommand = dbConnection.CreateCommand(command))
            {                
                return dbConnection.ExecuteNonQuery(dbCommand);
            }
        }

        public static int ExecuteNonQuery(this IDbConnection dbConnection, IDbCommand dbCommand)
        {
            ArgumentNullException.ThrowIfNull(dbCommand);

            dbConnection.Open();
            return dbCommand.ExecuteNonQuery();
        }

        public static IEnumerable<TResult> ExecuteReader<TResult>(this IDbConnection dbConnection, Command command, Func<IDataRecord, TResult> selector)
        {
            ArgumentNullException.ThrowIfNull(command);
            ArgumentNullException.ThrowIfNull(selector);

            using (IDbCommand dbCommand = dbConnection.CreateCommand(command))
            {
                return dbConnection.ExecuteReader(dbCommand, selector);               
            }
        }

        public static IEnumerable<TResult> ExecuteReader<TResult>(this IDbConnection dbConnection, IDbCommand dbCommand, Func<IDataRecord, TResult> selector)
        {
            ArgumentNullException.ThrowIfNull(dbCommand);
            ArgumentNullException.ThrowIfNull(selector);
            
            dbConnection.Open();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    yield return selector(dataReader);
                }
            }            
        }

        public static IDbCommand CreateCommand(this IDbConnection dbConnection, Command command)
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = command.Query;
            foreach(KeyValuePair<string, object> parameter in command.Parameters)
            {
                IDataParameter dataParameter = dbCommand.CreateParameter();
                dataParameter.ParameterName = parameter.Key;
                dataParameter.Value = parameter.Value;
                dbCommand.Parameters.Add(dataParameter);
            }
            return dbCommand;
        }
    }
}
