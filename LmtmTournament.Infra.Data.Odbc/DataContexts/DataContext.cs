using LmtmTournament.Domain.Interfaces.Repositories;
using LmtmTournamentCore.Domain.Entities;
using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace LmtmTournament.Infra.Data.Odbc.DataContexts
{
    public class DataContext : IDataContext
    {
        protected ServerSettings _serverSettings { get; set; }

        public DataContext(ServerSettings serverSettings) =>
            _serverSettings = serverSettings;

        protected string OpenConnection(IDbConnection dbConnection)
        {
            try
            {
                dbConnection.ConnectionString = _serverSettings.ConnectionString;
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                return $"Erro ao conectar no banco: {ex.Message}";
            }

            return string.Empty;
        }

        protected string HandleQuery(ref string query)
        {
            try
            {
                using (var connection = new SqliteConnection())
                {
                    string errorMessage = OpenConnection(connection);
                    if (!string.IsNullOrEmpty(errorMessage))
                        return errorMessage;

                    try
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        return ex.Message + (ex.InnerException?.Message);
                    }

                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao conectar no banco: {ex.Message} {ex.InnerException?.Message}";
            }
        }
    }
}
