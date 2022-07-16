using Microsoft.EntityFrameworkCore;
using MoviesProject.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var ConnctionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(ConnctionString));

builder.Services.AddControllers();

// Add the models services here
//builder.Services.AddTransient<IGenresService, GenresService>();
//builder.Services.AddTransient<IMoviesService, MoviesService>();

// Add automapper here
//builder.Services.AddAutoMapper(typeof(Program));

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
