using Geodata.Application.Interfaces;
using Geodata.Application.Common.Mappings;
using System.Reflection;
using Geodata.Persistence;
using Geodata.Application;
using Geodata.Persistence.IdentityEF;
using Microsoft.AspNetCore.Identity;
using Geodata.Api;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseKestrel();

//Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<MyIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<GeodataDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
});
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAutoMapper(config =>
{
    //Get information about current assembly in progress
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IGeodataDbContext).Assembly));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        //Call initialize DB
        var context = serviceProvider.GetRequiredService<GeodataDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        //Log.Fatal(exception, "An error occurred while app initialization");
    }
}

//Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{

}

app.UseRouting();

app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
