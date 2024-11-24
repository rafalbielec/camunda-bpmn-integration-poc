using System.ComponentModel.DataAnnotations.Schema;
using BpmnEngine.Migrations;

namespace BpmnEngine.Storage.Entities;

[Table(DatabaseConstants.RequestFormsTable)]
public class RequestFormEntity
{
    [Column("id")] public Guid Id { get; set; }
    [Column("name")] public string Name { get; set; }
    [Column("process_name")] public StorageConstants.ProcessName ProcessName { get; set; }
}