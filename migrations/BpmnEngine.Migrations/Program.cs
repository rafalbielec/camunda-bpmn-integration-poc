using BpmnEngine.Migrations.Definitions;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BpmnEngine.Migrations;

public class Program
{
    private static void Main()
    {
        var serviceProvider = CreateServices();

        using var scope = serviceProvider.CreateScope();

        RunMigrations(scope.ServiceProvider);
    }

    private static IServiceProvider CreateServices()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres11_0()
                .WithGlobalConnectionString(configuration.GetConnectionString(DatabaseConstants.ConnectionStringName))
                .ScanIn(typeof(Def001RequestForms).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }

    private static void RunMigrations(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Remove all tables, reset to 0
        //runner.MigrateDown(0);

        // Run migrations up to the last one
        runner.MigrateUp();
    }
}