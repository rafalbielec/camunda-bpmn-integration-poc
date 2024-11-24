using FluentMigrator;

namespace BpmnEngine.Migrations.Definitions;

[Migration(1)]
public class Def001RequestForms : Migration
{
    public override void Up()
    {
        Create.Table(DatabaseConstants.RequestFormsTable)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("name").AsString()
            .WithColumn("process_name").AsString();
    }

    public override void Down()
    {
        Delete.Table(DatabaseConstants.RequestFormsTable)
            .InSchema(DatabaseConstants.Schema);
    }
}