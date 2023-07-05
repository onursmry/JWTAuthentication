using IsProje.Data;
using IsProje.Models;
using IsProje.Services;
using IsProje.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace IsProje.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectContext>(options => options.UseSqlite(configuration.GetConnectionString("Default")));
            //using (var scope = configuration.ApplicationServices.CreateScope()) { var context = scope.ServiceProvider.GetRequiredService<ProjectContext>(); context.Database.EnsureCreated(); context.SaveChanges(); }
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(optitions =>
            {
                optitions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });


            //add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                                       builder => builder.AllowAnyOrigin()
                                                          .AllowAnyMethod()
                                                          .AllowAnyHeader());
            });

            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<JwtService>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });



            return services;
        }
    }
}
