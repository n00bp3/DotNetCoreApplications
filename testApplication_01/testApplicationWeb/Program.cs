using Jaeger.Samplers;
using Jaeger;
using Microsoft.EntityFrameworkCore;
using OpenTracing.Util;
using OpenTracing;
using Jaeger.Reporters;
using testApplication.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddOpenTracing();
builder.Services.AddSingleton<ITracer>(serviceProvider =>
{
    string serviceName = serviceProvider.GetRequiredService<IWebHostEnvironment>().ApplicationName;

    // This will log to a default localhost installation of Jaeger.
    var tracer = new Tracer.Builder(serviceName)
       .WithSampler(new ConstSampler(true))
       .Build();

    // Allows code that can't use DI to also access the tracer.
    GlobalTracer.Register(tracer);

    return tracer;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
