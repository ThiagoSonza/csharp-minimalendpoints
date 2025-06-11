using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MinimalEndpoints.Authorization.Requirements;

namespace MinimalEndpoints.Authorization.Handlers
{
    public class GrupoHandler : AuthorizationHandler<GrupoRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            GrupoRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, requirement.Grupo))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}