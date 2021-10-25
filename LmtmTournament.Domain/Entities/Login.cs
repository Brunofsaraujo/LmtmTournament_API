using Flunt.Validations;
using System;

namespace LmtmTournament.Domain.Entities
{
    public class Login : BaseEntity
    {
        public Login(
            string codigo,
            string senha)
        {
            Codigo = codigo;
            Senha = senha;

            Validate();
        }

        public string Codigo { get; }
        public string Senha { get; }
        public DateTime LoginDate { get; }
        public double LoginLifeTimeInMinutes { get { return ObterTempoSessao(); }}

        protected override void Validate()
        {
            AddNotifications(new Contract()
               .Requires()
               .IsNotNullOrEmpty(Codigo, nameof(Codigo), "Campo \"Código\" é obrigatório.")
               .IsNotNullOrEmpty(Senha, nameof(Senha), "Campo \"Senha\" é obrigatório."));
        }

        public double ObterTempoSessao() =>
            (DateTime.Now - LoginDate).TotalMinutes;
    }
}
