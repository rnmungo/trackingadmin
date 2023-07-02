using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DAL.TrackingAdmin;
using DAL.TrackingAdmin.Contracts;
using DAL.TrackingAdmin.UnitOfWork;
using BLL.TrackingAdmin.Contracts;
using BLL.TrackingAdmin.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new BLL.TrackingAdmin.Mappers.Mapper());
    mc.AddProfile(new TrackingAdmin.Mappers.Mapper());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton(mapper);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>
(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DB_TRACKING_ADMIN_CONNECTION_STRING") ?? "").EnableSensitiveDataLogging();
        options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole().AddDebug()));
    }
);

// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Business Services
builder.Services.AddScoped<IDistanceService, DistanceService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IRoadMapService, RoadMapService>();
builder.Services.AddScoped<ITruckService, TruckService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
