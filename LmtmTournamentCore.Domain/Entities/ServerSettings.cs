using LmtmTournamentCore.Domain.Interfaces;

namespace LmtmTournamentCore.Domain.Entities
{
    public class ServerSettings : IServerSettings
    {
        public string Secret { get; set; }
        public int ExpiracaoMinutos { get; set; }
        public string ConnectionString { get; set; }
    }
}
