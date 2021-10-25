using LmtmTournament.Domain.Entities;
using LmtmTournament.Domain.Enums;
using LmtmTournament.Domain.Interfaces.Repositories;
using LmtmTournament.Infra.Data.Odbc.DataContexts;
using LmtmTournament.Infra.Data.Odbc.Extensions;
using LmtmTournamentCore.Domain.Entities;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace LmtmTournament.Infra.Data.Odbc.Repositories
{
    public class AtletaRepository : DataContext, IAtletaRepository
    {
        public AtletaRepository(ServerSettings serverSettings) : base(serverSettings) { }

        public IEnumerable<Atleta> ObterAtletas()
        {
            var atletas = new List<Atleta>();

            string query = $@"
                SELECT
                    ""Codigo"",
                    ""Nome"",
                    ""Senha"",
                    ""Apelido"",
                    ""DataNascimento"",
                    ""Permissao""
                FROM ""Atletas""
            ";

            using (var connection = new SqliteConnection())
            {
                string errorMessage = OpenConnection(connection);
                if (!string.IsNullOrEmpty(errorMessage))
                    return new List<Atleta>() { new Atleta().GetEntityWithError<Atleta>(errorMessage) };

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            _ = DateTime.TryParse(dataReader["DataNascimento"]?.ToString(), out DateTime dataNascimento);
                            _ = int.TryParse(dataReader["Permissao"].ToString(), out int permissao);

                            var atleta = new Atleta(
                                codigo: dataReader["Codigo"]?.ToString(),
                                nome: dataReader["Nome"]?.ToString(),
                                senha: dataReader["Senha"]?.ToString(),
                                apelido: dataReader["Apelido"]?.ToString(),
                                dataNascimento: dataNascimento,
                                permissao: (EPermissao)permissao);

                            if (atleta.Valid)
                                atletas.Add(atleta);
                        }
                    }
                }
            }

            return atletas;
        }

        public Atleta ObterAtleta(Login login)
        {
            string query = $@"
                SELECT
                    ""Codigo"",
                    ""Nome"",
                    ""Senha"",
                    ""Apelido"",
                    ""DataNascimento"",
                    ""Permissao""
                FROM ""Atletas""
                WHERE ""Codigo"" = '{login.Codigo}'
                    AND ""Senha"" = '{login.Senha}' LIMIT 1
            ";

            using (var connection = new SqliteConnection())
            {
                string errorMessage = OpenConnection(connection);
                if (!string.IsNullOrEmpty(errorMessage))
                    return new Atleta().GetEntityWithError<Atleta>(errorMessage);

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            _ = DateTime.TryParse(dataReader["DataNascimento"]?.ToString(), out DateTime dataNascimento);
                            _ = int.TryParse(dataReader["Permissao"].ToString(), out int permissao);

                            return new Atleta(
                                codigo: dataReader["Codigo"]?.ToString(),
                                nome: dataReader["Nome"]?.ToString(),
                                senha: dataReader["Senha"]?.ToString(),
                                apelido: dataReader["Apelido"]?.ToString(),
                                dataNascimento: dataNascimento,
                                permissao: (EPermissao)permissao);
                        }
                    }
                }
            }

            return new Atleta();
        }

        public Atleta CadastrarAtleta(Atleta atleta)
        {
            string query = $@"
                INSERT INTO ""Atletas"" (Codigo, Nome, Senha, Apelido, DataNascimento, Permissao)
                VALUES ('{atleta.Codigo}', '{atleta.Nome}', '{atleta.Senha}', '{atleta.Apelido}', '{atleta.DataNascimento.DatetimeToString()}', {(int)atleta.Permissao});
            ";

            using (var connection = new SqliteConnection())
            {
                try
                {
                    string errorMessage = OpenConnection(connection);
                    if (!string.IsNullOrEmpty(errorMessage))
                        return new Atleta().GetEntityWithError<Atleta>(errorMessage);

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;

                        using (var dataReader = command.ExecuteReader())
                        {
                            if (dataReader.RecordsAffected <= 0)
                                return new Atleta().GetEntityWithError<Atleta>("Falha: Nenhum atleta foi cadastrado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new Atleta().GetEntityWithError<Atleta>($"Falha: Falha ao executar comando. Retorno: {ex.Message}");
                }
            }

            return atleta;
        }

        public Atleta ExcluirAtleta(string codigo)
        {
            string query = $@"
                DELETE FROM ""Atletas"" WHERE ""Codigo"" = '{codigo}';
            ";

            using (var connection = new SqliteConnection())
            {
                try
                {
                    string errorMessage = OpenConnection(connection);
                    if (!string.IsNullOrEmpty(errorMessage))
                        return new Atleta().GetEntityWithError<Atleta>(errorMessage);

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;

                        using (var dataReader = command.ExecuteReader())
                        {
                            if (dataReader.RecordsAffected <= 0)
                                return new Atleta().GetEntityWithError<Atleta>("Falha: Nenhum atleta foi excluido.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new Atleta().GetEntityWithError<Atleta>($"Falha: Falha ao executar comando. Retorno: {ex.Message}");
                }
            }

            return new Atleta();
        }
    }
}
