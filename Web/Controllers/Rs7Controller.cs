using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Query;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Web.Infrastructure;
using MoE.ECE.Web.Infrastructure.Authorisation;
using MoE.ECE.Web.Infrastructure.Authorisation.Resources;
using Moe.Library.Cqrs;

namespace MoE.ECE.Web.Controllers
{
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/" + Rs7.ResourceName)]
    [ApiConventionType(typeof(ApiConventions))]
    public class Rs7Controller : ControllerBase
    {
        private readonly ICqrs _cqrs;
        private readonly IMapper _mapper;

        public Rs7Controller(ICqrs cqrs, IMapper mapper)
        {
            _cqrs = cqrs;
            _mapper = mapper;
        }

        [HttpGet]
        [RequirePermission(Rs7.List)]
        public async Task<ActionResult<CollectionModel<Rs7Model>>> Index([FromQuery] ListRs7S query)
        {
            CollectionModel<Rs7Model>? rs7S = await _cqrs.QueryAsync(query);

            return Ok(rs7S);
        }

        /// <summary>
        ///     Retrieve an Rs7 form by id, and revision.
        /// </summary>
        /// <param name="id">The id of the Rs7 form</param>
        /// <param name="revisionNumber">Optional, don't set to get the current revision. Set to 1 to get the original revision.</param>
        /// Swashbuckle FromQuery params are always required https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/540
        [HttpGet("{id}")]
        [RequirePermission(Rs7.View)]
        public async Task<ActionResult<Rs7Model>> Get([FromRoute] int id, [FromQuery] int? revisionNumber)
        {
            GetRs7ByIdRevisionNumber? query = new(id, revisionNumber);
            Rs7Model? readModel = await _cqrs.QueryAsync(query);

            return Ok(readModel);
        }

        [HttpPost]
        [Obsolete("Superseded by methods on Rs7Actions")]
        [RequirePermission(RequirePermissionAttribute.ForObsoletedAction)]
        public async Task<ActionResult> Post([FromBody] Rs7Model request, CancellationToken cancellationToken)
        {
            IBeginSagaCommand command;
            if (request.IsZeroReturn.HasValue && request.IsZeroReturn.Value)
            {
                command = new CreateRs7ZeroReturn
                {
                    OrganisationId = request.OrganisationId,
                    FundingPeriod = request.FundingPeriod,
                    FundingPeriodYear = request.FundingPeriodYear
                };
            }
            else
            {
                command = new CreateSkeletonRs7
                {
                    OrganisationId = request.OrganisationId,
                    FundingPeriod = request.FundingPeriod,
                    FundingPeriodYear = request.FundingPeriodYear
                };
            }

            int id = await _cqrs.ExecuteAsync(command, cancellationToken);
            var routeValues = new {id};

            CreatedAtActionResult? result = CreatedAtAction(nameof(Get), routeValues, routeValues);

            return result;
        }

        [Route("{id}")]
        [HttpPut]
        [Obsolete("Superseded by methods on Rs7Actions")]
        [RequirePermission(RequirePermissionAttribute.ForObsoletedAction)]
        public async Task<ActionResult> Put(
            [FromRoute] int id,
            [FromBody] Rs7Model request,
            CancellationToken cancellationToken)
        {
            request.Id = id;

            async Task MapAndExecuteCommand<TCommand>() where TCommand : ICommand
            {
                TCommand? command = _mapper.Map<TCommand>(request);
                await _cqrs.ExecuteAsync(command, cancellationToken);
            }

            if (request.RollStatus == RollStatus.ExternalDraft)
            {
                await MapAndExecuteCommand<SaveAsDraft>();
            }
            else
            {
                await MapAndExecuteCommand<UpdateRs7>();
            }

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        [RequirePermission(Rs7.Delete)]
        public async Task<ActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            DiscardRs7? discardRs7 = new {Id = id};

            await _cqrs.ExecuteAsync(discardRs7, cancellationToken);

            return NoContent();
        }
    }
}