using Microsoft.AspNetCore.Authorization;

namespace MinimalEndpoints.Authorization;

public class IdadeMinimaRequirement(int idadeMinima) : IAuthorizationRequirement
{
    public int IdadeMinima { get; } = idadeMinima;
}