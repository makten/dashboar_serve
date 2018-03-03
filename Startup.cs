using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AutoMapper;
using dashboard.Controllers;
using dashboard.Core;
using dashboard.Core.Models;
using dashboard.Hubs;
using dashboard.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace dashboard {
    public class Startup {
        public Startup (IHostingEnvironment env, IConfiguration configuration) {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors (option => {
                option.AddPolicy ("AllowEverything",
                    policy => policy.AllowAnyOrigin ()
                    .AllowAnyHeader ()
                    .AllowAnyMethod ());
            });
            services.AddSignalR ();

            services.Configure<PhotoSettings> (Configuration.GetSection ("PhotoSettings"));
            services.Configure<SpotifySettings> (Configuration.GetSection ("SpotifySettings"));
            services.Configure<LastFmSettings> (Configuration.GetSection ("LastFmSettings"));

            // Repository Injections
            services.AddScoped<ITeamRepository, TeamRepository> ();
            services.AddScoped<IGoalRepository, GoalRepository> ();
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository> ();
            services.AddScoped<ITeamEnvironmentRepository, TeamEnvironmentRepository> ();
            services.AddScoped<IClientGroupRepository, ClientGroupRepository> ();
            services.AddScoped<ISpotifyRepository, SpotifyRepository> ();
            services.AddScoped<IVehicleRepository, VehicleRepository> ();
            services.AddScoped<IUnitOfWork, UnitOfWork> ();
            services.AddScoped<IPhotosRepository, PhotosRepository> ();

            //First install automapper and extension
            services.AddAutoMapper ();

            //Dbcontext service -- Db connection
            services.AddDbContext<DashboardDbContext> (options => options.UseSqlServer (Configuration.GetConnectionString ("Default")));

            //services.AddAuthorization(options =>
            //{
            //    // options.AddPolicy("RequireAdminRole", policy => policy.RequireClaim("https://dashapp.com/roles", "Admin"));
            //    options.AddPolicy(Roles.RequireAdminRole, policy => policy.RequireClaim("https://dashapp.com/roles", "Admin"));
            //})
            //.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.Audience = "https://api.dashboardapp.com";
            //    options.Authority = "https://dashapp.eu.auth0.com/";
            //    // options.RequireHttpsMetadata = false;                
            //});

            //Fix for Circular Reference .. �nclude(v => v.Suff)
            services.AddMvc ().AddJsonOptions (options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc ();

            //Configuration for KeyCloak
            services.AddAuthentication (options => {
                    // Store the session to cookies
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    // OpenId authentication
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (o => {
                    // URL of the Keycloak server
                    o.Authority = "http://localhost:8080/auth/realms/master";
                    // Client configured in the Keycloak
                    o.Audience = "team-dashboard";

                    o.Events = new JwtBearerEvents () {
                        OnAuthenticationFailed = c => {
                            c.NoResult ();

                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            if (Environment.IsDevelopment ()) {
                                return c.Response.WriteAsync (c.Exception.ToString ());
                            }
                            return c.Response.WriteAsync ("An error occured processing your authentication.");
                        }
                    };

                    // For testing we disable https (should be true for production)
                    o.RequireHttpsMetadata = false;

                    // OpenID flow to use
                    //options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Manager", policy => policy.RequireClaim("user_roles", "[Manager]"));
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();

                //Workaround for HRM (text/mime) problem --------------- 
                app.UseWebpackDevMiddleware (new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true,
                        HotModuleReplacementEndpoint = "/dist/_webpack_hrm"
                });

                app.UseWebpackDevMiddleware (new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseAuthentication ();

            app.UseCors ("AllowEverything");

            app.UseStaticFiles ();

            app.UseSignalR (routes => {
                routes.MapHub<DashboardHub> ("PlayerUpdate");
                routes.MapHub<TeamEnvironmentHub> ("teamEnvironmentUpdate");
                routes.MapHub<GoalsHub> ("GoalUpdate");
                routes.MapHub<LastFmHub> ("UpdateTrack");
            });

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute (
                    name: "spa-fallback",
                    defaults : new { controller = "Home", action = "Index" });
            });

        }
    }

}