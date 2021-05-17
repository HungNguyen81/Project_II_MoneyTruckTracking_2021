using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MoneyTruckTrackingAPI.Handlers;
using MoneyTruckTrackingAPI.Helpers;
using MoneyTruckTrackingAPI.Models;
using MoneyTruckTrackingAPI.Requirements;
using MoneyTruckTrackingAPI.ServiceInterfaces;
using MoneyTruckTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTruckTrackingAPI
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
            services.AddDbContext<AccountDbContext>(
                op => op.UseSqlServer(
                    Configuration.GetConnectionString("MoneyTruckTrackingDb")
                    )
                );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(op =>
                {
                    op.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = TokenHelper.ISSUER,
                        ValidAudience = TokenHelper.AUDIENCE,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Convert.FromBase64String(TokenHelper.SECRET))
                    };
                });

            services.AddAuthorization(op =>
            {
                op.AddPolicy("OnlyAdmin", policy =>
                {
                    policy.Requirements.Add(new AccountAdminRequirement(true));
                });
            });
            services.AddSingleton<IAuthorizationHandler, AccountAdminHandler>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddCors();
            services.AddControllers();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            //app.UseCors();
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
