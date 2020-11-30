using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoE.ECE.Domain.Query;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Web.Infrastructure;
using MoE.ECE.Web.Infrastructure.Authorisation;
using Moe.Library.Cqrs;

namespace MoE.ECE.Web.Controllers
{
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/rolls")]
    [ApiConventionType(typeof(ApiConventions))]
    public class RollsController : ControllerBase
    {
        private readonly ICqrs _cqrs;

        public RollsController(ICqrs cqrs)
        {
            _cqrs = cqrs;
        }

        [HttpGet]
        [RequirePermission(Infrastructure.Authorisation.Resources.Roll.List)]
        public async Task<ActionResult<CollectionModel<RollModel>[]>> List(
            [FromQuery(Name = "organisation-id")] int organisationId, 
            [FromQuery(Name = "page-size")] int? pageSize = 10,
            [FromQuery(Name = "page-number")] int? pageNumber = 1,
            CancellationToken cancellationToken = default)
        {
            var query = new ListRolls(organisationId)
            {
                PageSize = pageSize.GetValueOrDefault(),
                PageNumber = pageNumber.GetValueOrDefault()
            };

            var rolls = await _cqrs.QueryAsync(query, cancellationToken);

            return Ok(rolls);
        }
    }
}