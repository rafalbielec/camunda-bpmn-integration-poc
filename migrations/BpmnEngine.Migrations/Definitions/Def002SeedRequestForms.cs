using FluentMigrator;

namespace BpmnEngine.Migrations.Definitions;

// ReSharper disable StringLiteralTypo
[Migration(2)]
public class Def002SeedRequestForms : Migration
{
    public override void Up()
    {
        var carHireForm = new
        {
            id = new Guid("9d5aa57d-02a0-4ae7-9a4b-a9466216a1a5"),
            name = "Formularz rezerwacji samochodu służbowego",
            process_name = "CarHire"
        };

        var roomBookingForm = new
        {
            id = new Guid("22c753fc-b504-45b8-a7ed-fb27ff378727"),
            name = "Formularz rezerwacji sali",
            process_name = "RoomBooking"
        };

        Insert.IntoTable(DatabaseConstants.RequestFormsTable)
            .InSchema(DatabaseConstants.Schema)
            .Row(carHireForm);

        Insert.IntoTable(DatabaseConstants.RequestFormsTable)
            .InSchema(DatabaseConstants.Schema)
            .Row(roomBookingForm);
    }

    public override void Down()
    {
        Delete.FromTable(DatabaseConstants.RequestFormsTable)
            .InSchema(DatabaseConstants.Schema)
            .AllRows();
    }
}