using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;

namespace JwtBearerTokenAuthentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
               swagger =>
               {
                   swagger.AddSecurityDefinition(
                       "Bearer",
                       new OpenApiSecurityScheme
                       {
                           Description = "For accessing the API a valid JWT token must be passed in all the queries in the 'Authorization' header.",
                           Type = SecuritySchemeType.Http,
                           Scheme = "bearer"
                       });

               swagger.AddSecurityRequirement(
                   new OpenApiSecurityRequirement{
                       {
                           new OpenApiSecurityScheme{
                               Reference = new OpenApiReference
                               {
                                   Id = "Bearer",
                                   Type = ReferenceType.SecurityScheme
                               }
                           },
                           new List<string>()
                       }
                   });
               });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A970556E-583C-4833-89D7-FD2D74ABE8E4")),
                        ValidateIssuer = true,
                        ValidIssuer = "localhost",
                        ValidateAudience = true,
                        ValidAudience = "user"
                    };
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JwtBearerTokenAuthentication.API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            // register UseAuthentication-middleware before UseAuthorization
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
