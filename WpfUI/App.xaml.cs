using DataAccessLibrary.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Windows;
using WpfUI.Services;
using WpfUI.Stores;
using WpfUI.ViewModels;
using WpfUI.Views;

namespace WpfUI;

public partial class App : Application
{
    private IHost _appHost;

    public App()
    {
        try
        {
            string env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            bool isDevelopment = string.IsNullOrEmpty(env) || env.ToLower() == "development";

            _appHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", true, true);
                    config.AddJsonFile($"appsettings.{env}.json", true, true);
                    config.AddUserSecrets<App>(optional: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<NotesDbContext>(options =>
                    {
                        options.UseSqlite($@"Data Source={Environment.CurrentDirectory}\Notes.db;");
                    });
                    services.AddTransient<LoginViewModel>();
                    services.AddTransient<NotesViewModel>();
                    services.AddSingleton<NavigationStore>();
                    services.AddTransient<NavigationService<LoginViewModel>>();
                    services.AddTransient<NavigationService<NotesViewModel>>();
                    services.AddTransient<Func<LoginViewModel>>((sp) => () => sp.GetRequiredService<LoginViewModel>());
                    services.AddTransient<Func<NotesViewModel>>((sp) => () => sp.GetRequiredService<NotesViewModel>());
                    services.AddSingleton<LoginView>(sp => new LoginView()
                    {
                        DataContext = sp.GetRequiredService<LoginViewModel>()
                    });
                    services.AddSingleton<NotesView>(sp => new NotesView()
                    {
                        DataContext = sp.GetRequiredService<NotesViewModel>()
                    });
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>(sp => new MainWindow()
                    {
                        DataContext = sp.GetRequiredService<MainViewModel>()
                    });
                })
                .UseSerilog((context, services, loggerConfiguration) =>
                    loggerConfiguration.ReadFrom.Configuration(context.Configuration))
                .Build();
        }
        catch (Exception ex)
        {
            string type = ex.GetType().Name;
            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }

            Log.Fatal(ex, "Unexpected error");
            Application.Current.Shutdown();
        }
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _appHost.StartAsync();
        using IServiceScope scope = _appHost.Services.CreateScope();
        NotesDbContext db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
        db.Database.Migrate();
        NavigationService<NotesViewModel> navService = _appHost.Services.GetRequiredService<NavigationService<NotesViewModel>>();
        navService.Navigate();
        MainWindow mainWindow = _appHost.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        try
        {
            if (_appHost != null)
            {
                await _appHost.StopAsync();
                _appHost.Dispose();
            }

            Log.CloseAndFlush();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Unexpected error");
        }
        finally
        {
            base.OnExit(e);
        }
    }
}
