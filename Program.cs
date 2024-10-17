using ContactSystem.Data;
using ContactSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEntityFrameworkSqlServer()
               .AddDbContext<ContactSystemDBContext>(
                   options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
               );

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ContactService>();
            builder.Services.AddScoped<UserService>();

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
                pattern: "{controller=Contact}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
