using System;
using System.Windows;
using MentorsDiary.Desktop.Admin.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MentorsDiary.Desktop.Admin;

public partial class App : System.Windows.Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddMemoryCache();
        services.AddHttpContextAccessor();

        services.AddStackExchangeRedisCache(options => {
            options.Configuration = "localhost";
            options.InstanceName = "local";
        });

        services.AddSingleton<MainWindow>();

        #region SERVICES

        services.AddSingleton<UserService>();
        services.AddSingleton<DivisionService>();
        services.AddScoped<CuratorService>();
        services.AddSingleton<GroupService>();
        services.AddSingleton<ParentService>();
        services.AddSingleton<GroupEventService>();
        services.AddSingleton<GroupEventStudentService>();
        services.AddSingleton<ParentStudentService>();
        services.AddSingleton<EventService>();
        services.AddSingleton<StudentService>();

        #endregion

        #if DEBUG
        services.AddHttpClient("API", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7269");
        });
        #else
        services.AddHttpClient("API", client =>
        {
            client.BaseAddress = new Uri("https://curator.kai.ru/api");
        });
        #endif
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}