using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using IsProje.Data;
using System;
using IsProje.Extensions;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.



builder.Services.AddApiServices(builder.Configuration);



var app = builder.Build();

{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}

app.UseHttpsRedirection();



app.UseAuthorization();



app.MapControllers();



app.Run();