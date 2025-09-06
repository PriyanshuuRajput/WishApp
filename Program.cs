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

            string? connectionString = builder.Configuration.GetSection("ConnectionStrings")["Dbs"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("? Connection string 'Dbs' is missing in appsettings.json");
            }

            builder.Services.AddScoped<WishDBContext>(sp => new WishDBContext(connectionString));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();  
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Wish}/{action=List}/{id?}");

            app.Run();
        }
    }
}

