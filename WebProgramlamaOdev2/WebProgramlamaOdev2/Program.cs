using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebProgramlamaOdev2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using WebProgramlamaOdev2.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.Host;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
//<<<<<<< Updated upstream

//=======
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddDefaultIdentity<IdentityUser>
//>>>>>>> Stashed changes
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/returnUrl";
        });
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RegisterModel",
//         policy => policy.RequireRole("Admin"));
//});
//builder.Services.AddAuthentication(RegisterModel model);
//builder.Services.AddAuthentication(RegisterModel.AuthenticationScheme)
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
//        options => builder.Configuration.Bind("JwtSettings", options))
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//        options => builder.Configuration.Bind("CookieSettings", options));


builder.Services.AddSingleton<LanguageService>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {

            var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);

            return factory.Create("ShareResource", assemblyName.Name);

        };

    });



builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
            {
                            new CultureInfo("en-US"),
                            new CultureInfo("tr-TR"),
            };



        options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");

        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());

    }
);



builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql("Host=localhost;Database=WebProje;Username=postgres;Password=12345"));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddRoles<IdentityRole>().AddDefaultTokenProviders(); //resetlemede benzersiz bir token verir



builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; //sessioný tanýma yetkigerektiðinde gidilecek alan
    //options.LogoutPath = "/account/logout"; //sessiondan çýkma
    //options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true; //sessin def olarak 20 dk bunu true yaparsak her istekte 20 dk tekrar baþlar
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie.Name = "WebProgramlama.cookie";
    //AddAuthentication(Action < AuthenticationOptions > configureOptions);

});

var app = builder.Build();
//builder.Services.AddDbContext<OdevContext>(options => options.UseNpgsql("Host=localhost;Database=WebProje;Username=postgres;Password=12345"));
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount =
//true)
//    .AddRoles<IdentityRole>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization(); 


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Urun}/{action=UrunListeleAdmin}/{id?}");

app.Run();
