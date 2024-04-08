using Application.MessageServices;
using Application.RoleServices;
using Application.UserService;
using Application.UserServices.UserService;
using Domain.Foundation;
using Domain.JwtModels;
using Infrastructure.Foundation;
using Infrastructure.Providers;
using MessengerServer.Extensions;
using MessengerServer.Hubs;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddApiAuth(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDbContext<ApplicationContext>(c => c.UseSqlite(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddSignalR();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Some API v1", Version = "v1" });
//    // here some other configurations maybe...
//    options.AddSignalRSwaggerGen();
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHub<MessageHub>("/messages");
app.MapHub<UserHub>("/user");

app.Run();
