using AutoMapper;
using JAP_Management.Core.Entities;
using JAP_Management.Core.Helpers;
using JAP_Management.Infrastructure.AutoMapper;
using JAP_Management.Infrastructure.Database;
using JAP_Management.Repositories.Repositories.Base;
using JAP_Management.Repositories.Repositories.Program;
using JAP_Management.Repositories.Repositories.Ranks;
using JAP_Management.Repositories.Repositories.Selection;
using JAP_Management.Repositories.Repositories.Students;
using JAP_Management.Repositories.Repositories.Users;
using JAP_Management.Services.Services.EmailSender;
using JAP_Management.Services.Services.Program;
using JAP_Management.Services.Services.Ranks;
using JAP_Management.Services.Services.Selection;
using JAP_Management.Services.Services.Students;
using JAP_Management.Services.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace JAP_Management.Backoffice.Extensions
{
    public static class ProgramExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
            IConfiguration config)
        {
            //database
            services.AddDbContext<DatabaseContext>(option =>
                       option.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            //Swagger config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                },
                new string[] {}
            }
                });

            });
            //identity
            services.AddIdentity<BaseUser, IdentityRole>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

            //Jwt config
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = JwtHelper.Issuer,
                    ValidAudience = JwtHelper.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtHelper.SecretKey)),
                };
            });


            //auto mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            //cors policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin());
            });


            //repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<ISelectionRepository, SelectionRepository>();
            services.AddScoped<IRankRepository, RankRepository>();


            //services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<ISelectionServicee, SelectionService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IRankService, RankService>();

            return services;
        }
    }
}
