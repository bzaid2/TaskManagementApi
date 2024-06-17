using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace TaskManagement.Infrastructure.OpenApi
{
    internal class ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider, IOptions<SwaggerOptions> swaggerOptions)
        : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        private readonly SwaggerOptions _swaggerOptions = swaggerOptions.Value;

        public void Configure(SwaggerGenOptions options)
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                        .Where(assembly => assembly!.FullName!.Contains("TaskManagement", StringComparison.InvariantCultureIgnoreCase));
            assemblies.ToList().ForEach(assembly =>
            {
                if (File.Exists(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml")))
                {
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"));
                }
            });
            options.EnableAnnotations();
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();
            foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            return new OpenApiInfo
            {
                Version = $"v{description.ApiVersion}",
                Title = _swaggerOptions.Title,
                Description = _swaggerOptions.Description,
                TermsOfService = _swaggerOptions.TermsOfService,
                Contact = new OpenApiContact
                {
                    Name = _swaggerOptions.ContactName,
                    Url = _swaggerOptions.ContactUrl,
                    Email = _swaggerOptions.ContactEmail
                },
                License = !_swaggerOptions.License ? null :
                new OpenApiLicense
                {
                    Name = _swaggerOptions.LicenseName,
                    Url = _swaggerOptions.LicenseUrl
                }
            };
        }
    }
}
