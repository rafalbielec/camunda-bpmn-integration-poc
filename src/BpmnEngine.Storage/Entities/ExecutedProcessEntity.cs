using System.ComponentModel.DataAnnotations.Schema;
using BpmnEngine.Migrations;

namespace BpmnEngine.Storage.Entities;

[Table(DatabaseConstants.ExecutedProcessTable)]
public class ExecutedProcessEntity
{
    [Column("id")] public Guid Id { get; set; }
    [Column("created")] public DateTime Created { get; set; }
    [Column("definition_id")] public string DefinitionId { get; set; }
    [Column("business_key")] public string BusinessKey { get; set; }
    [Column("process_name")] public StorageConstants.ProcessName ProcessName { get; set; }
    [Column("variables", TypeName = "jsonb")] public string Variables { get; set; }

    [Column($"{DatabaseConstants.RequestFormsTable}_id")] public Guid RequestFormEntityId { get; set; }
}