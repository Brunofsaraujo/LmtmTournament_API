using LmtmTournament.API.Extensions;
using LmtmTournament.Domain.Entities;

namespace LmtmTournament.API.Interfaces
{
    public interface IJwtService
    {
        string CreateJsonWebToken(Atleta atleta, AppSettings appSettings);
    }
}
