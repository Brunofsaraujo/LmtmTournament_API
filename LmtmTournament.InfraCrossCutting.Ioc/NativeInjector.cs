using LmtmTournament.Domain.Interfaces.Repositories;
using LmtmTournament.Infra.Data.Odbc.DataContexts;
using LmtmTournament.Infra.Data.Odbc.Repositories;
using LmtmTournamentCore.Domain.Entities;
using LmtmTournamentCore.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LmtmTournament.InfraCrossCutting.Ioc
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IDataContext, DataContext>();
            services.AddTransient<IAtletaRepository, AtletaRepository>();
            services.AddTransient<IServerSettings, ServerSettings>();
        }
    }
}
