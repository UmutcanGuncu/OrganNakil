using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrganNakil.Application.Abstractions.Token;
using OrganNakil.Application.Dtos.TokenDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Handlers.UserHandlers;
using OrganNakil.Application.OptionsModel;
using OrganNakil.Domain.Entities;
using OrganNakil.Persistence.Context;
using OrganNakil.Persistence.Repositories;
using OrganNakil.WebAPI.Configurations.ColumnWrites;
using OrganNakil.WebAPI.Localizations;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using TokenHandler = OrganNakil.Persistence.Repositories.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(RegisterUserCommandHandlers).Assembly 
));
builder.Services.AddCors(options =>
    options.AddPolicy("myPolicy", opt =>
        opt.AllowAnyOrigin() // Belirli bir origin
            .AllowAnyHeader()
            .AllowAnyMethod()
            )); // Kimlik bilgilerini destekler

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSetting"));
Logger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("DefaultConnection"),"Logs",needAutoCreateTable:true,// Logs tablosuna yazacak logları. Uygulama açılınca logs tablosu yoksa otomatik olarak oluşturacak
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter()},
            {"message_template", new MessageTemplateColumnWriter()},
            {"level", new LevelColumnWriter()},
            {"exception", new ExceptionColumnWriter()},
            {"log_event", new LogEventSerializedColumnWriter()},
            {"timestamp", new TimestampColumnWriter()},
            {"username", new UsernameColumnWriter()}
        }).Enrich.FromLogContext() // Contexten beslenmesi gerektiğini bildiriyorum
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(logger);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua"); // kullanıcıya dair tüm bilgileri getirecek key
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
builder.Services.AddScoped<IMailRepository, MailRepository>();
builder.Services.AddScoped<IOrganDonationRepository, OrganDonationRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrganNakilDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",opt =>
    {
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = true, // oluşturulacak token değerini kimin dağıtacağını ifade edildiği alandır
            ValidateAudience = true, // Oluşturulacak token değerini hangi sitelerin/ originler kullanacağını belirleriz
            ValidateLifetime = true, // Oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır
            ValidateIssuerSigningKey = true, // gelen tokenlerda bakılacak değerleri bildiriyoruz. 
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            LifetimeValidator = (notBefore, expires,securityToken, validationParameters) => expires != null ? expires> DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name // JWT üzerinde Name claimine karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz
        };
    }); 
builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie = new CookieBuilder
    {
        Name = "AspNetCoreIdentityCookie",
        HttpOnly = false,
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.SameAsRequest
    };
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
});
builder.Services.AddIdentity<AppUser, AppRole >(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 4; // 4 tane başarısız giriş denemesinden sonra 5 dk giriş yapılamayacak
}).AddDefaultTokenProviders()
.AddRoles<AppRole>()
.AddRoleManager<RoleManager<AppRole>>()
.AddErrorDescriber<LocalizationIdentityErrorDescriber>()
.AddEntityFrameworkStores<OrganNakilDbContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging(); // hangi middlewarelareı logllamak istiyorsak o middleware'ların üstüne yaz
app.UseHttpLogging(); // yapılan requesleri de log mekanizmsında yakalarız
app.UseCors("myPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
// UseAuthentication ve UseAuthorization middlevarelerindan sonra oluşturacaz 
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : "Anonymous"; 
    LogContext.PushProperty("username", username);
    await next();
});
app.MapControllers();

app.Run();

