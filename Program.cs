using CrudAppUsingADO.Models;
using System; 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrudAppUsingADO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Replace this line:
            // string? connectionString = builder.Configuration.GetConnectionString("Dbs");

            // With this:
            string? connectionString = builder.Configuration.GetSection("ConnectionStrings")["Dbs"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("? Connection string 'Dbs' is missing in appsettings.json");
            }

            // ? Register WishDBContext with DI
            builder.Services.AddScoped<WishDBContext>(sp => new WishDBContext(connectionString));

            // Add MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();   // ? needed for css/js/images if you missed it
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Wish}/{action=List}/{id?}");

            app.Run();
        }
    }
}
