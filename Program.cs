using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using auth.Data;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using auth.Services;
using auth.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Invalid")));
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>(); 
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<IMailService, MailService>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<auth.Utilities.IUtility, auth.Utilities.Utility>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.Use(async (context, next) => {
//     Console.WriteLine("I am middleware");

//     // to get refresh token from header 
//     // context.Request.Cookies.TryGetValue("nekot", out var refreshToken);

//     try
//     {
//         var accessToken = context.Request.Headers["Authorization"][0].Replace("Bearer ", "");
//         if (accessToken != null)
//         {
//             var userId = new JwtSecurityTokenHandler()
//                 .ReadJwtToken(accessToken)
//                 .Claims.FirstOrDefault(claim => claim.Type == "Id")
//                 .Value;
//             // Console.WriteLine(accessToken);
//             // Console.WriteLine(userId);
//             var currentCookie = context.Request.Cookies["userId"];
//             // Console.Write(currentCookie);
//             if (currentCookie != userId)
//             {
//                 Console.WriteLine(true);
//                 var cookieOptions = new CookieOptions
//                 {
//                     HttpOnly = true,
//                     Expires = DateTime.Now.AddDays(7)
//                 };
//                 context.Response.Cookies.Append("userId", userId, cookieOptions);
//             }
//         }
//     }
//     catch
//     {
        
//     }
//     await next(context);
// });

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
