using FluentMigrator;
using Microsoft.AspNetCore.Identity;

namespace BpmnEngine.Migrations.Definitions;

[Migration(6)]
public class Def006SeedUsers : Migration
{
    public override void Up()
    {
        const string passwordSeed = "test";
        var pass = new PasswordHasher<Def006SeedUsers>().HashPassword(this, passwordSeed);

        var employee = new
        {
            id = new Guid("63387fa5-68ab-401d-8bea-d4bc09845075"),
            user_name = "employee@test.pl",
            position = "employee",
            password = pass
        };
        
        var manager = new
        {
            id = new Guid("32a43ea2-2364-49e3-b5f8-1098f60d714a"),
            user_name = "manager@test.pl",
            position = "manager",
            password = pass
        };
        
        var director = new
        {
            id = new Guid("5fa2ebdf-803f-45ed-80d6-ae1a1bd5e951"),
            user_name = "director@test.pl",
            position = "director",
            password = pass
        };

        Insert.IntoTable(DatabaseConstants.UsersTable)
            .InSchema(DatabaseConstants.Schema)
            .Row(employee);
        
        Insert.IntoTable(DatabaseConstants.UsersTable)
            .InSchema(DatabaseConstants.Schema)
            .Row(manager);
        
        Insert.IntoTable(DatabaseConstants.UsersTable)
            .InSchema(DatabaseConstants.Schema)
            .Row(director);
    }

    public override void Down()
    {
        Delete.FromTable(DatabaseConstants.UsersTable)
            .InSchema(DatabaseConstants.Schema)
            .AllRows();
    }
}
