using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.Security;
using Core.Security.Encryiption;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Contexts;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(
        opt =>
            opt.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd"))
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();
builder.Services.RegisterExceptionHandler();
builder.Services.AddSecurityServices();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions!.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddCors(
    opt =>
        opt.AddDefaultPolicy(p => { p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); })
);

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
        }
    );
    opt.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new string[] { }
            }
        }
    );
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionMiddleware();
app.UseSwagger();
app.UseSwaggerUI();
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BaseDbContext>();

    var dbExists = context.Database.CanConnect();

    if (!dbExists)
    {
        context.Database.EnsureCreated();
    }
    else
    {
        context.Database.Migrate();
    }
}
*/
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();