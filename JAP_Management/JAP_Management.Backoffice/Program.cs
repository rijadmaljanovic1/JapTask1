using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using JAP_Management.Backoffice.Extensions;
using JAP_Management.Backoffice.Middlewares;

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

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

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
