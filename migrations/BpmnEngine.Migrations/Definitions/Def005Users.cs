using FluentMigrator;

namespace BpmnEngine.Migrations.Definitions;

[Migration(5)]
public class Def005Users : Migration
{
    public override void Up()
    {
        Create.Table(DatabaseConstants.UsersTable)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("user_name").AsString()
            .WithColumn("password").AsString()
            .WithColumn("position").AsString();
    }

    public override void Down()
    {
        Delete.Table(DatabaseConstants.UsersTable)
            .InSchema(DatabaseConstants.Schema);
    }
}