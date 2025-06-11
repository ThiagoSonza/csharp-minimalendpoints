using MinimalEndpoints.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AdicionaVersionamento();
builder.Services.AdicionaAutenticacao();
builder.Services.AdicionaSwagger();
builder.Services.AdicionaDependencias();
builder.Services.AddCors();
builder.Services.AddEndpoints(typeof(Program).Assembly);

WebApplication app = builder.Build();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.ConfiguraVersionamento();
app.ConfiguraSwagger();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
