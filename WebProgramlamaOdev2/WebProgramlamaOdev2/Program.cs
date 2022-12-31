using Microsoft.EntityFrameworkCore;
using WebProgramlamaOdev2.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();
//builder.Services.AddDbContext<OdevContext>(options => options.UseNpgsql("Host=localhost;Database=WebProje;Username=postgres;Password=12345"));

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Urun}/{action=UrunListeleAdmin}/{id?}");

app.Run();
