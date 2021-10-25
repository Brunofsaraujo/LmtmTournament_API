using LmtmTournament.API.Models;
using LmtmTournament.Domain.Constants;
using LmtmTournament.Domain.Entities;
using LmtmTournament.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmtmTournament.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AtletaController : BaseController
    {
        private readonly IAtletaRepository _atletaRepository;

        public AtletaController(
            IAtletaRepository atletaRepository)
            : base()
        {
            _atletaRepository = atletaRepository;
        }

        [HttpGet]
        [Route("Obter")]
        [Authorize(Roles = PermissionsContants.Administrador)]
        public IActionResult ObterAtletas()
        {
            var atletas = _atletaRepository.ObterAtletas();

            var (invalid, returnResult) = ValidateEntityState(atletas);
            if (invalid)
                return returnResult;

            return Ok(atletas);
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = PermissionsContants.Administrador)]
        public IActionResult CadastrarAtleta([FromBody] AtletaViewModel atletaViewModel)
        {
            var atleta = (Atleta)atletaViewModel;

            var (invalid, returnResult) = ValidateEntityState(atleta);
            if (invalid)
                return returnResult;

            _atletaRepository.CadastrarAtleta(atleta);

            (invalid, returnResult) = ValidateEntityState(atleta);
            if (invalid)
                return returnResult;

            return Ok(atleta);
        }

        [HttpDelete]
        [Route("Excluir")]
        [Authorize(Roles = PermissionsContants.Administrador)]
        public IActionResult ExcluirAtleta([FromQuery] string codigo)
        {
            var atletaExcluido = _atletaRepository.ExcluirAtleta(codigo);

            var (invalid, returnResult) = ValidateEntityState(atletaExcluido);
            if (invalid)
                return returnResult;

            return Ok($"Atleta \"{codigo}\" deletado com sucesso.");
        }
    }
}
