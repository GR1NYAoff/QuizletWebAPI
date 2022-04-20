using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using QuizletWebAPI.Common;
using QuizletWebAPI.Resourse.Data;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

//Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = authOptions.Audience,

            ValidateLifetime = true,

            // HS256
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseCors(myAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
