using System.ComponentModel.DataAnnotations.Schema;
using BpmnEngine.Migrations;

namespace BpmnEngine.Storage.Entities;

[Table(DatabaseConstants.UserActionsTable)]
public class UserActionEntity
{
    [Column("id")] public Guid Id { get; set; }
    [Column("created")] public DateTime Created { get; set; }
    [Column("topic_name")] public string TopicName { get; set; }

    [Column($"{DatabaseConstants.ExecutedProcessTable}_id")] public Guid ExecutedProcessId { get; set; }
}