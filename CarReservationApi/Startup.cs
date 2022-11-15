using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Car.DataAccess.Concrete.EntityFramework;
using Car.DataAccess.Abstract;
using Car.Bussiness.Abstract;
using Car.Bussiness.Concrete.Managers;
using CarReservationApi.Middleware;

namespace CarReservationApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            using (var client = new CarReservationContext())
            {
                var connectionString = this.Configuration.GetConnectionString("CarReservationContext");
                CarReservationContext.SetConnectionString(connectionString);
                var efLogLevel = Configuration
                    .GetSection("Logging")
                    .GetSection("LogLevel")
                    .GetValue<LogLevel>("Microsoft.EntityFrameworkCore.Database.Command");
                CarReservationContext.SetLogLevel(efLogLevel);

                client.Database.EnsureCreated();
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region AddSingleton
            services.AddSingleton<ICarModelService, CarModelManager>();
            services.AddSingleton<ICarModelDal, EfCarModelDal>();           
            #endregion AddSingleton

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.HandshakeTimeout = TimeSpan.FromMinutes(1);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.
                        WithOrigins("*").
                        WithMethods("GET", "POST", "GET, POST, PUT, DELETE, OPTIONS").
                        AllowAnyOrigin());
            });

            /*Swagger Authorization*/
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Car.WebApi",
                    Description = "Car Reservation API Swagger Surface"
                });

                //s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR")
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<OptionsMiddleware>();
            app.UseMiddleware<LocalizationMessageMiddleware>();
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("v1/swagger.json", "Car Reservation API V1");
            });
        }
    }
}
