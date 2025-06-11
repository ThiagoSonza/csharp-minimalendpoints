using MinimalEndpoints.Abstractions;
using MinimalEndpoints.Services;

namespace MinimalEndpoints.Endpoints;

public record UserLogin(string Username, string Password);

public class Auth(TokenService tokenService) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login", Login);
    }

    private IResult Login(UserLogin user)
    {
        // Validação simples de usuário (apenas exemplo)
        if (user.Username == "admin" && user.Password == "senha123")
        {
            var token = tokenService.GenerateJwtToken(user.Username);
            return Results.Ok(new { token });
        }

        return Results.Unauthorized();
    }
}