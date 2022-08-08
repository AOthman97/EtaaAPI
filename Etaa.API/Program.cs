using EtaaAPI.Core.Interfaces;
using EtaaAPI.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var ConnctionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(ConnctionString), ServiceLifetime.Transient);

builder.Services.AddControllers();

// Add the models services here
// *This is the old way that do not use the Unit of Work pattern, It deals with the IBaseRepo directly
//builder.Services.AddTransient(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Add automapper here
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

//builder.Services.AddSingleton<IFileProvider>(
//                new PhysicalFileProvider(
//                    Path.Combine(Directory.GetCurrentDirectory(),
//                    "./wwwroot/ProjectFiles")));

// Add services later
//builder.Services.AddScoped<IFamiliesService, FamiliesService>();
//builder.Services.AddScoped<IFamilyMembersService, FamilyMembersService>();
//builder.Services.AddScoped<IContributorsService, ContributorsService>();

builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

// For the IdentityUser
//builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();

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
//app.UseStaticFiles();
//app.UseDirectoryBrowser(new DirectoryBrowserOptions
//{
//    FileProvider = new PhysicalFileProvider
//                (
//                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectFiles")
//                ),
//    RequestPath = "/ProjectFiles"
//});

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Families}/{action=Index}/{id?}");
//app.MapRazorPages();

app.Run();
