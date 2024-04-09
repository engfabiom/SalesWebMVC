using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
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

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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
