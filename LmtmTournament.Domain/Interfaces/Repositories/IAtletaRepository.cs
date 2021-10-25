using LmtmTournament.Domain.Entities;
using System.Collections.Generic;

namespace LmtmTournament.Domain.Interfaces.Repositories
{
    public interface IAtletaRepository
    {
        IEnumerable<Atleta> ObterAtletas();
        Atleta ObterAtleta(Login login);
        Atleta CadastrarAtleta(Atleta atleta);
        Atleta ExcluirAtleta(string codigo);
    }
}
