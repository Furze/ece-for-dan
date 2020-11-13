using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoE.ECE.Domain.Command.Rs7;
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
    public class Rs7ActionsController : ControllerBase
    {
        private readonly ICqrs _cqrs;
        private readonly IMapper _mapper;

        public Rs7ActionsController(ICqrs cqrs, IMapper mapper)
        {
            _cqrs = cqrs;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new rs7 skeleton with the funding periods and dates pre populated.
        /// </summary>
        /// <remarks>Experimental </remarks>
        /// <returns></returns>
        [HttpPost("new-requests")]
        [RequirePermission(Rs7.NewRequestsCreate)]
        public async Task<ActionResult<Rs7NewRequestModel>> PostActionNewRequest([FromBody] CreateSkeletonRs7 request, CancellationToken cancellationToken)
        {
            var id = await _cqrs.ExecuteAsync(request, cancellationToken);

            var newRequest = _mapper.Map<Rs7NewRequestModel>(request);
            newRequest.Rs7Id = id;
            return Ok(newRequest);
        }

        /// <summary>
        /// Updates the rs7 roll information for an RS7 in draft state
        /// </summary>
        /// <remarks>
        /// Experimental
        /// </remarks>
        /// <returns></returns>
        [HttpPost("{id}/draft-changes")]
        [RequirePermission(Rs7.DraftChangesCreate)]
        public async Task<ActionResult> PostActionDraftChanges([FromRoute] int id, [FromBody] SaveRs7EntitlementDetailsDraft request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(NoContent());
        }

        /// <summary>
        /// Updates an entitlement month
        /// </summary>
        /// <remarks>
        /// Allowed during create, or after submitted.
        /// </remarks>
        /// <returns></returns>
        [HttpPut("{id}/entitlement-months/{monthNumber}")]
        [RequirePermission(Rs7.EntitlementMonthUpdate)]
        public async Task<ActionResult> PutEntitlementMonth([FromRoute] int id, [FromRoute] int monthNumber, [FromBody] UpdateRs7EntitlementMonth request, CancellationToken cancellationToken)
        {
            request.Id = id;
            request.MonthNumber = monthNumber;
            await _cqrs.ExecuteAsync(request, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Submits a RS7 for approval by an authoriser
        /// </summary>
        /// <remarks>
        /// Experimental
        /// 
        /// External admin users creating an RS7 have to go through a two step approval process.
        ///
        /// This action updates the RS7 roll data and starts a workflow for the two step approval process</remarks>
        /// <returns></returns>
        [HttpPost("{id}/submissions-for-approval")]
        [RequirePermission(Rs7.SubmissionsForApprovalCreate)]
        public async Task<ActionResult> PostActionSubmitForApproval([FromRoute] int id, [FromBody] SubmitRs7ForApproval request, CancellationToken cancellationToken)
        {
            request.Id = id;
            await _cqrs.ExecuteAsync(request, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Submits an RS7 to the ministry
        /// </summary>
        /// <remarks>
        /// Experimental
        /// 
        /// Allows a user with dual role or an internal user to create and submit an RS7 created by an external user to the ministry
        /// in a one step process.
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost("{id}/submissions")]
        [RequirePermission(Rs7.SubmissionsCreate)]
        public async Task<ActionResult> PostActionSubmit([FromRoute] int id, [FromBody] SubmitRs7 request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(NoContent());
        }

        /// <summary>
        /// Updates the RS7 declaration
        /// </summary>
        /// <remarks>
        /// You cannot update the declaration once submitted.
        /// </remarks>
        /// <returns></returns>
        [HttpPut("{id}/declaration")]
        [RequirePermission(Rs7.DeclarationUpdate)]
        public async Task<ActionResult> PutDeclaration([FromRoute] int id, [FromBody] UpdateRs7Declaration request, CancellationToken cancellationToken)
        {
            request.Id = id;
            await _cqrs.ExecuteAsync(request, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Creates and submits a zero returnRS7 to the ministry for approval
        /// </summary>
        /// <remarks>
        /// Experimental
        /// 
        /// A zero return is an RS7 with no advance days or entitlement hours entered.
        /// </remarks>
        /// <returns></returns>
        [HttpPost("new-zero-returns")]
        [RequirePermission(Rs7.ZeroReturnsCreate)]
        public async Task<ActionResult> PostActionZeroReturn([FromRoute] CreateSkeletonRs7 request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(NoContent());
        }
    }
}