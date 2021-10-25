using LmtmTournament.Domain.Enums;

namespace LmtmTournament.Domain.Constants
{
    public static class PermissionsContants
    {
        public const string
            Default = nameof(EPermissao.Default),
            Atleta = nameof(EPermissao.Atleta),
            Coordenador = nameof(EPermissao.Coordenador),
            Administrador = nameof(EPermissao.Administrador);
    }
}
