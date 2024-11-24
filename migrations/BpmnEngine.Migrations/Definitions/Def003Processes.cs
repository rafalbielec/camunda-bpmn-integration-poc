using FluentMigrator;

namespace BpmnEngine.Migrations.Definitions;

[Migration(3)]
public class Def003Processes : Migration
{
    public override void Up()
    {
        Create.Table(DatabaseConstants.ExecutedProcessTable)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("created").AsDateTime().WithDefaultValue(DateTime.UtcNow)
            .WithColumn("definition_id").AsString()
            .WithColumn("business_key").AsString()
            .WithColumn("process_name").AsString()
            .WithColumn("variables").AsCustom("jsonb").Nullable()
            .WithColumn($"{DatabaseConstants.RequestFormsTable}_id").AsGuid();

        Create.ForeignKey("request_form_id_fk")
            .FromTable(DatabaseConstants.ExecutedProcessTable)
            .InSchema(DatabaseConstants.Schema)
            .ForeignColumn($"{DatabaseConstants.RequestFormsTable}_id")
            .ToTable(DatabaseConstants.RequestFormsTable)
            .InSchema(DatabaseConstants.Schema)
            .PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete.ForeignKey("request_form_id_fk")
            .OnTable(DatabaseConstants.ExecutedProcessTable)
            .InSchema(DatabaseConstants.Schema);

        Delete.Table(DatabaseConstants.ExecutedProcessTable)
            .InSchema(DatabaseConstants.Schema);
    }
}