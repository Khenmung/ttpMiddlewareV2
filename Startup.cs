using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using ttpMiddleware.Data;
using ttpMiddleware.Configuration;
using ttpMiddleware.Data.Entities;
using Microsoft.AspNet.OData.Extensions;
using ttpMiddleware.Models;
using ttpMiddleware.Models.DTOs;
using Newtonsoft.Json.Serialization;
using ttpMiddleware.Common;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ttpMiddleware
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
            services.AddHttpContextAccessor();
            services.AddSingleton<IRequestObject, RequestObject>();
            ////1services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ////1services.AddDbContext<ttpauthContext>();
            var enckey = "b14ca5898a4e4133bbce2ea2315a1916";
            string passwordSubstr = Configuration.GetConnectionString("constr").Split("Password")[1];
            //var decrytedstr = CommonFunctions.AesOperation.DecryptString(enckey, passwordSubstr.Substring(1));
            //var constr = Configuration.GetConnectionString("constr").Split("Password")[0] + "Password=" + decrytedstr;
            var constr = Configuration.GetConnectionString("constr");
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(constr);
            });
            services.AddDbContext<ttpauthContext>(options =>
            {
                options.UseSqlServer(constr);
            });
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ttpauthContext>()
                .AddDefaultTokenProviders();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ttpMiddleware", Version = "v1" });

            //});
            //services.Add(typeof(IExceptionLogger), new TraceExceptionLogger());

            var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                //RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParams);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParams;
            });

            //services.AddControllers()
          
            services.AddOData();

            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddMvc(opt =>
            {
                opt.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.EnvironmentName.Length > 0)
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ttpMiddleware v1"));
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads"
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("Open");

            app.UseMvc(routebuilder =>
            {
                routebuilder.EnableDependencyInjection();
                routebuilder.Select().Filter().Expand().MaxTop(100).OrderBy().SkipToken().Count();
                //routebuilder.AddRouteComponents("odata", EDMModel.getEdmModel()));
                routebuilder.MapODataServiceRoute("ODataRoute", "odata", EDMModel.getEdmModel());
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseExceptionHandlerMiddleware();
        }
    }
}
