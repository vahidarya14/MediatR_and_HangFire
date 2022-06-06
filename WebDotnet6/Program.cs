using Hangfire;
using MediatR;
using MediatR_and_HangFire;
using Microsoft.EntityFrameworkCore;
using WebDotnet6.Controllers;

string conn = "Data Source=.;Initial Catalog=hangfireMediat;User ID=sa;Password=1234;";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddHangfire(c => {  
        c.UseSqlServerStorage(conn);
        c.UseMediatR(); 
    }); 
builder.Services.AddHangfireServer();

builder.Services.AddDbContextPool<AppDbContext>(c => c.UseSqlServer(conn));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllers();

    endpoints.MapHangfireDashboard();
});
app.Run();
