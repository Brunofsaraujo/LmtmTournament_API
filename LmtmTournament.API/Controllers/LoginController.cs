using LmtmTournament.API.Extensions;
using LmtmTournament.API.Interfaces;
using LmtmTournament.API.Models;
using LmtmTournament.Domain.Constants;
using LmtmTournament.Domain.Entities;
using LmtmTournament.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LmtmTournament.API.Controllers
{
    [Route("v1")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly AppSettings _appSettings;

        private readonly IAtletaRepository _atletaRepository;
        private readonly IJwtService _jwtService;

        public LoginController(
            IOptionsMonitor<AppSettings> appSettings,
            IAtletaRepository atletaRepository,
            IJwtService jwtService)
            : base()
        {
            _appSettings = appSettings.CurrentValue;
            _atletaRepository = atletaRepository;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult AuthenticateUser(LoginViewModel loginViewModel)
        {
            var login = (Login)loginViewModel;
            Atleta atleta = new();

            if(login.Valid)
                atleta = _atletaRepository.ObterAtleta(login);

            if (string.IsNullOrEmpty(atleta.Codigo))
                return BadRequest("Código ou senha incorreto.");

            var (invalid, returnResult) = ValidateEntityState(atleta);
            if (invalid)
                return returnResult;

            var token = _jwtService.CreateJsonWebToken(atleta, _appSettings);

            atleta.LimparSenhaAposLogin();

            return Ok(new {
                Codigo = atleta.Codigo,
                Nome = atleta.Nome,
                Token = token,
                Tempo = _appSettings.ExpiracaoMinutos });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Version")]
        public object GetCurrentVersion()
        {
            return new { version = "Version 0.0.1" };
        }
    }
}
