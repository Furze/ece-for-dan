using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MoE.ECE.Web.Infrastructure.Middleware.Exceptions;

namespace MoE.ECE.Web.Infrastructure
{
     //TODO(danyo): Most conventions dont match because of missing cancellation token arg.
    public static class ApiConventions
    {
        [ProducesResponseType(200)]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void List([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                                ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        {
            // No implementation required.
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [ProducesDefaultResponseType(typeof(ErrorResponse))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Find([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                                 ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        {
            // No implementation required.
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        {
            // No implementation required.
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Post([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                                 ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                           ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object cancellationToken)
        {
            // No implementation required.
        }
        
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void PostAction(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
             ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                                 ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                           ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object cancellationToken)
        {
            // No implementation required.
        }

        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Put([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                        ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                           ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object cancellationToken)
        {
            // No implementation required.
        }
        
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Delete([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any),
                        ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model)
        {
            // No implementation required.
        }
    }
}