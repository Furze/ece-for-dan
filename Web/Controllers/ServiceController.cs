using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoE.ECE.Domain.Query;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Services;
using MoE.ECE.Web.Infrastructure;
using MoE.ECE.Web.Infrastructure.Authorisation;
using MoE.ECE.Web.Infrastructure.Authorisation.Resources;
using Moe.Library.Cqrs;

namespace MoE.ECE.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/" + Services.ResourceName)]
    [ApiConventionType(typeof(ApiConventions))]
    public class ServiceController : ControllerBase
    {
        private readonly ICqrs _cqrs;

        public ServiceController(ICqrs cqrs)
        {
            _cqrs = cqrs;
        }

        [HttpGet("{id}")]
        [RequirePermission(Services.GetService)]
        public async Task<ActionResult<ECEServiceModel>> Get(int id, CancellationToken cancellationToken = default)
        {
            var result = await _cqrs.QueryAsync(new GetServiceById(id), cancellationToken);
            
            return Ok(result);
        }
        
        [HttpGet]
        [RequirePermission(Services.ListServices)]
        public async Task<ActionResult<CollectionModel<SearchEceServiceModel>[]>> Find(
            [FromQuery(Name = "search-term")] string? searchTerm, 
            [FromQuery(Name = "page-size")] int? pageSize = 10,
            [FromQuery(Name = "page-number")] int? pageNumber = 1,
            CancellationToken cancellationToken = default)
        {
            //TODO: Need to Add Auth Checks to make to this endpoint.
            
            var query = new FindServices(searchTerm ?? string.Empty)
            {
                PageSize = pageSize.GetValueOrDefault(),
                PageNumber = pageNumber.GetValueOrDefault()
            };

            var services = await _cqrs.QueryAsync(query, cancellationToken);

            return Ok(services);
        }
    }
}