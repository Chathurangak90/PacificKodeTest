using PacificKode.Repositories;
using PacificKode.Services;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableSqlPerfCounters", false);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHome, HomeDAL>();
builder.Services.AddScoped<IDepartment, DepartmentDAL>();
builder.Services.AddScoped<IEmployee, EmployeeDAL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());


app.MapControllers();

app.Run();
