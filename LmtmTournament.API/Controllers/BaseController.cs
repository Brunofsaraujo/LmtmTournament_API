using LmtmTournament.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LmtmTournament.API.Controllers
{
    public class BaseController : Controller
    {
        protected (bool invalid, IActionResult errorReturn) ValidateEntityState(BaseEntity singleEntity)
        {
            bool invalid = singleEntity.Invalid;
            IActionResult errorReturn = null;

            if (invalid)
                errorReturn = ReturnBadRequestMessageFromEntity(singleEntity);

            return (invalid, errorReturn);
        }

        protected (bool invalid, IActionResult errorReturn) ValidateEntityState(IEnumerable<BaseEntity> listEntities)
        {
            var errorEntity = listEntities?.FirstOrDefault(line => line.Invalid);
            if (errorEntity != null)
                return ValidateEntityState(errorEntity);

            return (false, null);
        }

        protected IActionResult ReturnBadRequestMessageFromEntity(BaseEntity singleEntity)
        {
            var serviceLayerError = singleEntity.Notifications.FirstOrDefault(line => line.Message.Contains("Falha: "));
            if (serviceLayerError != null)
                return BadRequest(serviceLayerError.Message);

            return BadRequest(singleEntity.Notifications.FirstOrDefault().Message);
        }
    }
}
