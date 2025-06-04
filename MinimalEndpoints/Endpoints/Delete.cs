using MinimalEndpoints.Abstractions;
using MinimalEndpoints.Authorization.Requirements;

namespace MinimalEndpoints.Endpoints;

public class DeleteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/customers")
            .WithTags("Customers")
            .MapToApiVersion(2)
            .RequireAuthorization("IdadeMinimaRequirement")
            .RequireAuthorization(policy =>
                    policy.Requirements.Add(new GrupoRequirement("Admin")))
            ;

        group.MapDelete("delete-1", Delete);
        group.MapDelete("delete-2", Delete);
    }

    private static IResult Delete(int id)
    {
        return Results.Ok($"Customer {id}");
    }
}