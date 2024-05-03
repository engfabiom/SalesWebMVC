using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Services;
using System.Globalization;
namespace SalesWebMVC {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            //builder services
            builder.Services.AddDbContext<SalesWebMVCContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("SalesWebMVCContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMVCContext' not found."),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesWebMVCContext")),
                    b => b.MigrationsAssembly("SalesWebMVC"))
                );
            builder.Services.AddScoped<ServiceSeeding>();
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // CultureInfo, Localization
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization/select-language-culture?view=aspnetcore-8.0
            var ciList = new List<CultureInfo> {
                new("en-US")
                ,new("pt-BR")
                ,new("ja-JP")
            };
            var localizationOptions = new RequestLocalizationOptions {
                SupportedCultures = ciList,
                SupportedUICultures = ciList,
                DefaultRequestCulture = new RequestCulture(ciList[0])
            };
            app.UseRequestLocalization(localizationOptions);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else {
                // https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-6.0&tabs=visual-studio#seed-the-database
                using (var scope = app.Services.CreateScope()) {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<SalesWebMVCContext>();
                    var seedingService = new ServiceSeeding(context);
                    seedingService.Seed();
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
