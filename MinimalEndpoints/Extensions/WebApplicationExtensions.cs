using Asp.Versioning;
using Asp.Versioning.Builder;

namespace MinimalEndpoints.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfiguraVersionamento(this WebApplication app)
    {
        var apiContagem = app.NewVersionedApi("MinimalEndpoints");

        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasDeprecatedApiVersion(new ApiVersion(1, 0))
            .HasApiVersion(new ApiVersion(2, 0))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder versionedGroup = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        app.MapEndpoints(versionedGroup);

        return app;
    }

    public static WebApplication ConfiguraSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(
            options =>
            {
                var descriptions = app.DescribeApiVersions();

                // build a swagger endpoint for each discovered API version
                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });

        return app;
    }
}