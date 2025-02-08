using App.Extensions;
using App.Middlewares;
using Common.DependencyInjection;
using Common.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(SecurityPolices.DoctorsOnly, x => x.RequireRole(UserRoles.Doctor))
    .AddPolicy(SecurityPolices.PatientsOnly, x => x.RequireRole(UserRoles.Patient));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration.GetValue<string>("Jwt:Secret")!)),
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

app.UseApplicationServices();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
