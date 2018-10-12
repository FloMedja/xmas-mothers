using AutoMapper;
using ChristmasMothers.Business.Extensions;
using ChristmasMothers.Dal.EntityFramework.Extensions;
using ChristmasMothers.Web.Api.Configurations;
using ChristmasMothers.Web.Api.Extensions;
using ChristmasMothers.Web.Api.Operationfilters;
using ChristmasMothers.Web.Api.Private.Extensions;
using ChristmasMothers.Web.Api.V2.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace ChristmasMothers.Web.Application
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile("identityserver.json", false, true)
                .AddJsonFile($"identityserver.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables("ChristmasMothers_");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var identityServerOptions = Configuration.GetSection(IdentityServerOptions.CONFIGURATION_SECTION).Get<IdentityServerOptions>();
            identityServerOptions.RefreshOptions();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddIdentityServerAuthentication(o =>
            {
                o.Authority = identityServerOptions.Authority;
                o.RequireHttpsMetadata = identityServerOptions.RequireHttpsMetadata;

                //o.AllowedScopes = new List<string> { "Api" };
                //o.AdditionalScopes = identityServerOptions.AllowedScopes?.ToList();
                o.LegacyAudienceValidation = identityServerOptions.LegacyAudienceValidation;
            });

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.AllowCredentials();
            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
                options.DefaultPolicyName = "SiteCorsPolicy";
            });

            services.AddOptions();
            services.AddMemoryCache();
            services.AddDal(Configuration);
            services.AddBusinesses();
            services.AddAutoMapper();

            // Setup mappings
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddViewModelMapperConfigurationPrivate();
                cfg.AddViewModelMapperConfigurationV2();
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddApi(Configuration);
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "ChristmasMother REST Api V1", Version = "v1" });
                options.SwaggerDoc("v2", new Info { Title = "ChristmasMother REST Api V2", Version = "v2" });
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    In = "header",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                options.OperationFilter<ResponseContentTypeOperationFilter>();
                options.OperationFilter<TagByApiExplorerSettingsOperationFilter>();
            });

            services.AddMvc(options =>
            {
                // apply authorization filter on endpoints
                if (identityServerOptions.EnableAuthorization)
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                }
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddFile("AppData/Logs/ChristmasMothers-{Date}.txt");
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApi();
            app.UseCors("SiteCorsPolicy");
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ChristmasMother REST Api V1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "ChristmasMother REST Api V2");

            });
            app.UseMvc();
        }
    }
}
