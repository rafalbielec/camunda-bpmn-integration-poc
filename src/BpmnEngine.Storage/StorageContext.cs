using BpmnEngine.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace BpmnEngine.Storage;

public class StorageContext : DbContext
{
    private readonly string _connectionString;

    public StorageContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<RequestFormEntity> RequestForms { get; set; }
    public DbSet<ExecutedProcessEntity> ExecutedProcess { get; set; }
    public DbSet<UserActionEntity> UserActions { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<RequestFormEntity>()
            .Property(e => e.ProcessName)
            .HasConversion<string>();
        
        modelBuilder
            .Entity<ExecutedProcessEntity>()
            .Property(e => e.ProcessName)
            .HasConversion<string>();
    }
}