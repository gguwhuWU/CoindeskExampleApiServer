using CoindeskExampleApiServer.Handler;
using CoindeskExampleApiServer.Models.Option;
using CoindeskExampleApiServer.Protocols.Repositories;
using CoindeskExampleApiServer.Protocols.Services;
using CoindeskExampleApiServer.Repositories;
using CoindeskExampleApiServer.Services;
using CoindeskExampleApiServer.Services.SubSystem;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CoindeskExampleApiServer
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="configuration"></param>
    /// <param name="env"></param>
    public class Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        private readonly IWebHostEnvironment _env = env;

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; } = configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                // if don't set default value is: 28.6MB
                // 當前 100 MB
                options.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
            });

            services.AddHttpContextAccessor();

            services.AddControllers()
                 .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

#pragma warning disable CS8604 // 可能有 Null 參考引數。
            var supportedCultures = Configuration.GetSection("Cultures:SupportedCultures")
                                        .Get<string[]>()
                                        .Select(c => new CultureInfo(c))
                                        .ToList();
#pragma warning restore CS8604 // 可能有 Null 參考引數。
            var defaultCulture = Configuration["Cultures:DefaultCulture"];

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture!);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new[] { new AcceptLanguageHeaderRequestCultureProvider() };
            });

            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.AddHttpContextAccessor();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CoindeskExample",
                    Version = "v1"
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                options.SchemaFilter<EnumTypesSchemaFilter>(Path.Combine(AppContext.BaseDirectory, "CMS3Model.xml"));
                options.DocumentFilter<EnumTypesDocumentFilter>();
            });

            services.AddDbContext<CoindeskDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddAuthorization();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            AddHttpClient(services, Configuration);

            AddConfigurationOptions(services);

            AddServices(services);

            AddRepositories(services);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment() || _env.IsStaging() || _env.EnvironmentName == "Local")
            {
                app.UseExceptionHandler("/error-local-development");

                app.UseSwagger();
                app.UseSwaggerUI(c => { c.DocExpansion(DocExpansion.None); });
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                ;
            });
        }

        private static void AddHttpClient(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHttpClient("Coindesk", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Coindesk:url").Get<string>()!);
                client.Timeout = TimeSpan.FromSeconds(30);
            });
        }

        private void AddConfigurationOptions(IServiceCollection services)
        {
            services.Configure<AesEncryptionInfo>(Configuration.GetSection("AesEncryptionInfo"));
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICoindeskApiService, CoindeskApiService>();
            services.AddScoped<ICoindeskService, CoindeskService>();
            services.AddScoped<ICopilotService, CopilotService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IAesEncryption, AesEncryption>();

            services.AddScoped<IFacadeSystem>(provider =>
            {
                var sub1 = new SubSystem1();
                var sub2 = new SubSystem2();
                var sub3 = new SubSystem3();
                var sub4 = new SubSystem4();
                return new FacadeSystem(sub1, sub2, sub3, sub4);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICopilotRepository, CopilotRepository>();
        }
    }
}
