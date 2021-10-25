using LmtmTournament.API.Extensions;
using LmtmTournament.Domain.Entities;
using LmtmTournament.Domain.Enums;
using System;

namespace LmtmTournament.API.Models
{
    public class AtletaViewModel
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Apelido { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Permissao { get; set; }

        public static implicit operator Atleta(AtletaViewModel atletaViewModel)
            => new(
                codigo: atletaViewModel.Codigo,
                nome: atletaViewModel.Nome,
                senha: atletaViewModel.Senha.EncryptPassword(),
                apelido: atletaViewModel.Apelido,
                dataNascimento: atletaViewModel.DataNascimento,
                permissao: (EPermissao) Enum.Parse(typeof(EPermissao), atletaViewModel.Permissao)
            );
    }
}
