using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ApiVersioning.WebApi
{
    public static class ConfigureSwaggerExtension
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),  
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"WebApi {description.GroupName.ToUpperInvariant()}");

                options.DefaultModelExpandDepth(2);
                options.DefaultModelRendering(ModelRendering.Model);
                options.DefaultModelsExpandDepth(-1);
                options.DisplayOperationId();
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.List);
                options.EnableDeepLinking();
                options.EnableFilter();
                options.MaxDisplayedTags(5);
                options.ShowExtensions();
                options.ShowCommonExtensions();
                options.EnableValidator();
            });

            return app;
        }

        public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider provider;

            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            {
                this.provider = provider;
            }

            public void Configure(SwaggerGenOptions options)
            {
                // add swagger document for every API version discovered
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
                }
            }

            public void Configure(string name, SwaggerGenOptions options)
            {
                Configure(options);
            }

            private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
            {
                var info = new OpenApiInfo()
                {
                    Title = $"Web Api Documentation [{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}] {description.ApiVersion}",
                    Version = description.ApiVersion.ToString()
                };

                if (description.IsDeprecated)
                {
                    info.Description += " This API version has been deprecated.";
                }

                return info;
            }
        }
    }
}