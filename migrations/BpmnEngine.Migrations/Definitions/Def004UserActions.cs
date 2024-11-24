using FluentMigrator;

namespace BpmnEngine.Migrations.Definitions;

[Migration(4)]
public class Def004UserActions : Migration
{
    public override void Up()
    {
        Create.Table(DatabaseConstants.UserActionsTable)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("created").AsDateTime().WithDefaultValue(DateTime.UtcNow)
            .WithColumn("topic_name").AsString()
            .WithColumn($"{DatabaseConstants.ExecutedProcessTable}_id").AsGuid();

        Create.ForeignKey("ua_to_ep_id_fk")
            .FromTable(DatabaseConstants.UserActionsTable)
            .InSchema(DatabaseConstants.Schema)
            .ForeignColumn($"{DatabaseConstants.ExecutedProcessTable}_id")
            .ToTable(DatabaseConstants.ExecutedProcessTable)
            .InSchema(DatabaseConstants.Schema)
            .PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete.ForeignKey("ua_to_ep_id_fk")
            .OnTable(DatabaseConstants.UserActionsTable)
            .InSchema(DatabaseConstants.Schema);

        Delete.Table(DatabaseConstants.UserActionsTable)
            .InSchema(DatabaseConstants.Schema);
    }
}