using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeItUpData.Library.Migrations
{
    public partial class UpdatedActivationTimeColumnNameInAlarmTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Alarms",
                newName: "ActivationTime");

            migrationBuilder.AlterColumn<string>(
                name: "TotalPausedTime",
                table: "Timers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivationTime",
                table: "Alarms",
                newName: "Time");

            migrationBuilder.AlterColumn<string>(
                name: "TotalPausedTime",
                table: "Timers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
