using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Notes.WebApi;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            string apiVersion = description.ApiVersion.ToString();
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Version = apiVersion,
                Title = $"Notes Web {apiVersion}",
                Description = "An ASP.NET Core Web API for managing notes.",
                Contact = new OpenApiContact
                {
                    Name = "Malove",
                    Email = string.Empty,
                    Url = new Uri("https://t.me/maloveee"),
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                }
            });

            options.AddSecurityDefinition($"AuthToken {apiVersion}", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = $"AuthToken {apiVersion}",
                        }
                    },
                    new string[] { }
                }
            });

            options.CustomOperationIds(apiDescription =>
                apiDescription.TryGetMethodInfo(out MethodInfo? methodInfo) 
                    ? methodInfo.Name 
                    : null);
        }
    }
}