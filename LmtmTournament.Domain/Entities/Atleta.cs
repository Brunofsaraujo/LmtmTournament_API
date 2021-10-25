using Flunt.Validations;
using LmtmTournament.Domain.Enums;
using System;

namespace LmtmTournament.Domain.Entities
{
    public class Atleta : BaseEntity
    {
        public Atleta() { }

        public Atleta(
            string codigo,
            string nome,
            string senha,
            string apelido,
            DateTime dataNascimento,
            EPermissao permissao = EPermissao.Default)
        {
            Codigo = codigo;
            Nome = nome;
            Senha = senha;
            Apelido = string.IsNullOrEmpty(apelido) ? nome : apelido;
            DataNascimento = dataNascimento;
            Permissao = permissao;

            Validate();
        }

        public string Codigo { get; init; }
        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public string Apelido { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public EPermissao Permissao { get; private set; }

        protected override void Validate()
        {
            AddNotifications(new Contract()
               .Requires()
               .IsNotNullOrEmpty(Codigo, nameof(Codigo), "Campo \"Código\" é obrigatório.")
               .HasMaxLen(Codigo, 8, nameof(Codigo), "Campo \"Código\" deve ter no máximo 8 characteres.")
               .IsNotNullOrEmpty(Nome, nameof(Nome), "Campo \"Nome\" é obrigatório.")
               .IsNotNullOrEmpty(Senha, nameof(Senha), "Campo \"Senha\" é obrigatório."));
        }

        public void LimparSenhaAposLogin() => Senha = string.Empty;
    }
}
