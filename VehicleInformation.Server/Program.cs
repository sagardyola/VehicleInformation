using VehicleInformation.Business.Repository;
using VehicleInformation.Business.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IVehicleInfoRepository, VehicleInfoRepository>();


builder.Services.AddCors(o => o.AddPolicy("VehicleInformation", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("VehicleInformation");

app.UseAuthorization();

app.MapControllers();

app.Run();
