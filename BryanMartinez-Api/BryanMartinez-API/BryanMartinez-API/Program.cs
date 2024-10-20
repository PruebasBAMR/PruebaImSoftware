using CapaLogica.clase;
using CapaLogica.interfaz;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//se agregarn reglas cors
var misReglasCors = "ReglasCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: misReglasCors,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("settings").GetSection("secretKey").ToString();// "=Codig0Estudiant3=";
var audience = builder.Configuration.GetSection("Jwt").GetSection("Audience").ToString();
var issuer = builder.Configuration.GetSection("Jwt").GetSection("Issuer").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey!);

builder.Services.AddAuthentication(config =>
{

    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = audience,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
    };
});

//Agregamos las interfaces
builder.Services.AddScoped<IPersona, PersonaLogica>();
builder.Services.AddScoped<IToken, TokenLogica>();
builder.Services.AddScoped<IValidaCampos, ValidaCampos>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS
app.UseCors(misReglasCors);

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
