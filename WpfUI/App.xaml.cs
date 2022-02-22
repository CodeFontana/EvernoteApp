﻿using DataAccessLibrary.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Windows;
using WpfUI.ViewModels;
using WpfUI.Views;

namespace WpfUI
{
    public partial class App : Application
    {
		private IHost _appHost;

		protected override async void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			try
			{
				string env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
				bool isDevelopment = string.IsNullOrEmpty(env) || env.ToLower() == "development";

                _appHost = Host.CreateDefaultBuilder(e.Args)
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
							options.UseSqlite("Data Source=Notes.db;");
						});
						services.AddSingleton<LoginWindow>();
						services.AddSingleton<NotesWindow>();
						services.AddSingleton<LoginViewModel>();
						services.AddSingleton<NotesViewModel>();
					})
					.UseSerilog((context, services, loggerConfiguration) =>
						loggerConfiguration.ReadFrom.Configuration(context.Configuration))
					.Build();

				await _appHost.StartAsync();
				
				var notesWindow = _appHost.Services.GetService<NotesWindow>();
				notesWindow.DataContext = _appHost.Services.GetService<NotesViewModel>();
				notesWindow.Show();
			}
			catch (Exception ex)
			{
				string type = ex.GetType().Name;
				if (type.Equals("StopTheHostException", StringComparison.Ordinal))
				{
					throw;
				}

				Log.Fatal(ex, "Unexpected error");
			}
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			try
			{
				await _appHost.StopAsync();
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
}
