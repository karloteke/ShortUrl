using ShortUrl.Business;
using ShortUrl.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShortUrl API", Version = "v1" });

    // Configure the security scheme for JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShortUrl API v1");
//         c.RoutePrefix = string.Empty; // Serve Swagger UI at the app root
//     });
// }
// else
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }

app.UseDeveloperExceptionPage();
app.UseCors("MyAllowedOrigins");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
