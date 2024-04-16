using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EntityFrameworkCodeFirstChallenge.Data;
namespace EntityFrameworkCodeFirstChallenge {
    public class Program {
        public static SchoolDatabaseContext SchoolDatabaseContext;
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SchoolDatabaseContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("SchoolDatabaseContext") ?? throw new InvalidOperationException("Connection string 'EntityFrameworkCodeFirstChallengeContext' not found.")), ServiceLifetime.Singleton);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();
            SchoolDatabaseContext = app.Services.GetRequiredService<SchoolDatabaseContext>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
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
        }
    }
}
