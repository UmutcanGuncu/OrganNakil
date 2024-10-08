using System.Net.NetworkInformation;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganNakil.Application.Mediatr.Handlers.UserHandlers;
using OrganNakil.Domain.Entities;
using OrganNakil.Persistence.Context;
using OrganNakil.WebAPI.Localizations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(RegisterUserCommandHandlers).Assembly 
));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrganNakilDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddAuthorization();
builder.Services.AddIdentity<AppUser, AppRole >(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 4; // 4 tane başarısız giriş denemesinden sonra 5 dk giriş yapılamayacak
}).AddDefaultTokenProviders()
.AddRoles<AppRole>()
.AddErrorDescriber<LocalizationIdentityErrorDescriber>()
.AddEntityFrameworkStores<OrganNakilDbContext>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

