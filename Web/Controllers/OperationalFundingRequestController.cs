using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using MoE.ECE.Web.Infrastructure;
using MoE.ECE.Web.Infrastructure.Authorisation;
using MoE.ECE.Web.Infrastructure.Authorisation.Resources;
using Moe.Library.Cqrs;
using System;
namespace MoE.ECE.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/" + OperationalFundingRequests.ResourceName)]
    [ApiConventionType(typeof(ApiConventions))]
    public class OperationalFundingRequestController : ControllerBase
    {
        private readonly ICqrs _cqrs;

        public OperationalFundingRequestController(ICqrs cqrs)
        {
            _cqrs = cqrs;
        }

        [HttpGet]
        [RequirePermission(OperationalFundingRequests.GetOperationalFundingRequests)]
        public async Task<ActionResult<ICollection<OperationalFundingRequestModel>>> GetOperationalFundingRequestWashUps(
            [FromQuery(Name = "business-entity-id"), Required]
            Guid businessEntityId,
            [FromQuery(Name = "revision-number")] int? revisionNumber,
            CancellationToken cancellationToken = default)
        {
            var query = new GetOperationalFundingRequestWashup(businessEntityId, revisionNumber);

            var operationalFundingRequests = await _cqrs.QueryAsync(query, cancellationToken);

            return Ok(operationalFundingRequests);
        }
    }
}