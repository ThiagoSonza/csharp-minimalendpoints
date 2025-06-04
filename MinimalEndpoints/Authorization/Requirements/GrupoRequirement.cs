using Microsoft.AspNetCore.Authorization;

namespace MinimalEndpoints.Authorization.Requirements;

public class GrupoRequirement(string grupo) : IAuthorizationRequirement
{
    public string Grupo { get; } = grupo;
}
