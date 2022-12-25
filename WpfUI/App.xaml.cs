using DataAccessLibrary.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI;

public partial class App : Application
{
    private IHost _appHost;

    public App()
    {
        try
        {
            _appHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<NotesDbContext>(options =>
                    {
                        options.UseSqlite($@"Data Source={Environment.CurrentDirectory}\Notes.db;");
                    });
                    services.AddTransient(sp => new NotesDbContextFactory($@"Data Source={Environment.CurrentDirectory}\Notes.db;"));
                    services.AddTransient<NotesRepositoryFactory>();
                    services.AddTransient<MainViewModel>();
                    services.AddSingleton(sp => new MainWindow(sp.GetRequiredService<MainViewModel>()));
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
        Window mainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
