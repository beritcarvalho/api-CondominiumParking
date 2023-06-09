using CondominiumParkingApi.Infrastructure.IoC.DependencyInjections;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjection(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fluent => fluent.RegisterValidatorsFromAssemblyContaining(typeof(CondominiumParkingApi.Applications.Validators.ApplicationValidatorsAssemblyMarker)))
    .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
