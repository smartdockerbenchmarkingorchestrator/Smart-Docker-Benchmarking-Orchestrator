using AutoMapper;
using Docker.Benchmarking.Orchestrator.Optimizer;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using Docker.Benchmarking.Orchestrator.Infrastrcture;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Data;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Services;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Settings;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using Docker.Benchmarking.Orchestrator.Web.Hubs;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Docker.Benchmarking.Orchestrator.Web
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddResponseCompression();

            services.AddAutoMapper();

            services.AddElmah(options => options.Path = "elmah");

            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddMvc(options =>
            {
                options.Filters.Add(new ServiceLayerValidationExceptionFilter());
                options.Filters.Add(new JsonExceptionFilterAttribute());

            })
            .AddXmlSerializerFormatters()
            .AddXmlDataContractSeria‌​lizerFormatters()
             .AddRazorPagesOptions(options =>
             {
             })
                .AddControllersAsServices()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Docker Benchmarking Orchestrator", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                       .WithOrigins("http://localhost:55830")
                       .AllowCredentials();
            }));

            services.AddSignalR();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.Configure<HostSettings>(Configuration.GetSection("HostSettings"));
            services.Configure<AzureSettings>(Configuration.GetSection("AzureSettings"));
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddLogging();
            services.AddCors();
            services.AddOptimzer();

            services.Configure<EnvironmentConfig>(Configuration);

            services.AddSingleton<ICurrentHostSettings, CurrentHostSettings>();

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

            services.AddMediatR(typeof(DockerImageTakenCommand).GetTypeInfo().Assembly);

            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup)); // Web
                    _.AssemblyContainingType(typeof(BaseEntity)); // Core
                    _.Assembly("Docker.Benchmarking.Orchestrator.Infrastructure");
                    //_.AssemblyContainingType<IEnumerable<Region>>(); // Our assembly with requests & handlers
                    _.WithDefaultConventions();
                    _.AddAllTypesOf(typeof(IRequestHandler<,>));
                    _.AddAllTypesOf(typeof(IRequestHandler<>));
                    _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                    _.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                    _.With(new AddRequestHandlersWithGenericParametersToRegistry());
                });

                //config.For<IMessageSender>().Use<EmailMessageSenderService>();

                //Services (Business Logic)
                config.For<IDockerRemoteService>().Use<DockerRemoteService>();
                config.For<IDockerImageService>().Use<DockerImageService>();
                config.For<IBenchmarkResultsService>().Use<BenchmarkResultsService>();
                config.For<IBenchmarkOrchestratorService>().Use<BenchmarkOrchestratorService>();
                config.For<IAzureResourceService>().Use<AzureResourceService>();
                config.For<IAzureVMService>().Use<AzureVMService>();
                config.For<IBenchmarkExperimentService>().Use<BenchmarkExperimentService>();
                config.For<IApacheJmeterFileService>().Use<ApacheJmeterFileService>();
                config.For<IAWSResourcesService>().Use<AWSResourcesService>();

                // TODO: Add Registry Classes to eliminate reference to Infrastructure

                // TODO: Move to Infrastucture Registry
                config.For(typeof(IRepository<>)).Add(typeof(EfRepository<>));

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();

        }

        public void ConfigureTesting(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            Configure(app, env, loggerFactory);
            //SeedData.PopulateTestData(app.ApplicationServices.GetService<AppDbContext>());
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger(c =>
            {

            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //elmah
            app.UseElmah();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });

            //Hangfire
            app.UseHangfireServer();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            app.UseCookiePolicy();

            app.UseResponseCompression();

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<AWSHub>("/AWSHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}