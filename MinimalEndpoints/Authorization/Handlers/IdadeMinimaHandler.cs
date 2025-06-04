using Microsoft.AspNetCore.Authorization;

namespace MinimalEndpoints.Authorization;

public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        IdadeMinimaRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "idade"))
        {
            // Sem a claim, falha na autorização
            return Task.CompletedTask;
        }

        var idade = int.Parse(context.User.FindFirst("idade")!.Value);

        if (idade >= requirement.IdadeMinima)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}