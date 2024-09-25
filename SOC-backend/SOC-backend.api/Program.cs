using Microsoft.EntityFrameworkCore;
using SOC_backend.data;
using SOC_backend.data.Repositories;
using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ReactProject",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionstring, b => b.MigrationsAssembly("SOC-backend.api"));
});

var app = builder.Build();

app.UseCors("ReactProject");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
