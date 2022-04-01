using Calindra.Desafio.Domain.Commands.Handlers;
using Calindra.Desafio.Domain.Commands.Inputs;
using Calindra.Desafio.Domain.Services;
using Calindra.Desafio.WebApi.Filters;
using Calindra.Desafio.WebApi.Middlewares;
using Calindra.Desafio.WebApi.Serializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Calindra.Desafio.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new ModelValidationFilter());
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.UseCamelCasing(true);
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
            });

            services.AddHttpClient<IGeolocationApiService, GoogleGeolocationApiService>((provider, httpClient) =>
            {
                httpClient.BaseAddress = new System.Uri(Configuration["GoogleApi:Url:Base"]);
            });

            services.AddScoped<DistanceCalculatorService, DistanceCalculatorService>();
            services.AddScoped<ICommandHandler<Addresses>, CalculatorHandler>();

            services.AddSingleton<JsonHelperSerializer, JsonHelperSerializer>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMiddleware<NotFoundMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
