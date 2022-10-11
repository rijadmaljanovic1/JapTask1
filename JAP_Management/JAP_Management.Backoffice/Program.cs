using AutoMapper;
using JAP_Management.Core.Helpers;
using JAP_Management.Infrastructure.AutoMapper;
using JAP_Management.Infrastructure.Database;
using JAP_Management.Repositories.Repositories.Base;
using JAP_Management.Repositories.Repositories.Users;
using JAP_Management.Repositories.Repositories.Students;
using JAP_Management.Services.Services.Students;
using JAP_Management.Services.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using JAP_Management.Repositories.Repositories.Program;
using JAP_Management.Services.Services.Program;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using JAP_Management.Repositories.Repositories.Selection;
using JAP_Management.Services.Services.Selection;
using JAP_Management.Core.Entities;
using JAP_Management.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using JAP_Management.Services.Services.EmailSender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
    options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
    }));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();


//database
builder.Services.AddDbContext<DatabaseContext>(option =>
           option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Swagger config
builder.Services.AddSwaggerGen(c =>
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
builder.Services.AddIdentity<BaseUser, IdentityRole>(x =>
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
builder.Services.AddAuthentication(x =>
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
builder.Services.AddSingleton(mapper);


//cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
        builder => builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
});


//repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<ISelectionRepository, SelectionRepository>();


//services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<ISelectionServicee, SelectionService>();
builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder =>
{
    builder.WithOrigins("https://localhost:5001")
           .AllowCredentials()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
//var scope = app.Services.CreateScope();

//var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
//var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BaseUser>>();
//DatabaseInitializer.Init(context);
//DatabaseInitializer.Initialize(context, roleManager, userManager);


app.Run();
