using Microsoft.EntityFrameworkCore;
using MyAcademy.Components;
using MyAcademy.Data;

namespace MyAcademy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("PV522ImportContext")
                ?? throw new InvalidOperationException("Connection string 'PV522ImportContext' not found.");
            // Add services to the container.
            builder.Services.AddDbContextFactory<PV522ImportContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddQuickGridEntityFrameworkAdapter();

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
