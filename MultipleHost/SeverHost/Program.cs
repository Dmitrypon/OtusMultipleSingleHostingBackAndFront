using Microsoft.Extensions.Configuration;
using ServerHost.Settings;
using SeverHost.Settings;

var builder = WebApplication.CreateBuilder(args);

string Origin = "MyAllowSpecificOrigins";

var config = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Origin,
                      build =>
                      {
                          build.WithOrigins(config.Origins)
                          .WithMethods(config.Methods)
                          .WithHeaders(config.Headers);
                      });
});

// Add services to the container.
builder.Services.AddControllers();
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

app.UseAuthorization();

app.UseCors(Origin);
app.MapControllers();

app.Run();
