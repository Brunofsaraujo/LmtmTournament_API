using LmtmTournament.API.Extensions;
using LmtmTournament.Domain.Entities;

namespace LmtmTournament.API.Models
{
    public class LoginViewModel
    {
        public string Codigo { get; set; }
        public string Senha { get; set; }

        public static implicit operator Login(LoginViewModel loginViewModel)
            => new(
                codigo: loginViewModel.Codigo,
                senha: loginViewModel.Senha.EncryptPassword()
            );
    }
}
