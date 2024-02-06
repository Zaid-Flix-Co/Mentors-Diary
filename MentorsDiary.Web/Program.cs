using MentorsDiary.Web.DependencyInjection;

namespace MentorsDiary.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IConfigurationBuilder, ConfigurationManager>();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddAntDesign();
        builder.Services.AddWebCollection();

        var app = builder.Build();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.Map("/mentors-diary-client/", subapp => {
            subapp.UsePathBase("/mentors-diary-client/");
            subapp.UseRouting();
            subapp.UseEndpoints(endpoints => endpoints.MapBlazorHub());
        });

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}