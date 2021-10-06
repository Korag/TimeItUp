using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeItUpData.Library.Migrations
{
    public partial class AddedPausesDbSetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_Timers_TimerId1_TimerUserId",
                table: "Alarms");

            migrationBuilder.DropForeignKey(
                name: "FK_Splits_Timers_TimerId1_TimerUserId",
                table: "Splits");

            migrationBuilder.AddColumn<int>(
                name: "AlarmsNumber",
                table: "Timers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Timers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "Timers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Timers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Timers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Paused",
                table: "Timers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SplitsNumber",
                table: "Timers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "Timers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TotalDuration",
                table: "Timers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TotalPausedTime",
                table: "Timers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "TimerUserId",
                table: "Splits",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TimerId1",
                table: "Splits",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "Splits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "Splits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TotalDuration",
                table: "Splits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "TimerUserId",
                table: "Alarms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TimerId1",
                table: "Alarms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Alarms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "Alarms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Alarms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "Alarms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Pauses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TimerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDuration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pauses", x => new { x.Id, x.TimerId });
                    table.ForeignKey(
                        name: "FK_Pauses_Timers_TimerId1_TimerUserId",
                        columns: x => new { x.TimerId1, x.TimerUserId },
                        principalTable: "Timers",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pauses_TimerId1_TimerUserId",
                table: "Pauses",
                columns: new[] { "TimerId1", "TimerUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_Timers_TimerId1_TimerUserId",
                table: "Alarms",
                columns: new[] { "TimerId1", "TimerUserId" },
                principalTable: "Timers",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Splits_Timers_TimerId1_TimerUserId",
                table: "Splits",
                columns: new[] { "TimerId1", "TimerUserId" },
                principalTable: "Timers",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_Timers_TimerId1_TimerUserId",
                table: "Alarms");

            migrationBuilder.DropForeignKey(
                name: "FK_Splits_Timers_TimerId1_TimerUserId",
                table: "Splits");

            migrationBuilder.DropTable(
                name: "Pauses");

            migrationBuilder.DropColumn(
                name: "AlarmsNumber",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "Paused",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "SplitsNumber",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "TotalDuration",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "TotalPausedTime",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "Splits");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Splits");

            migrationBuilder.DropColumn(
                name: "TotalDuration",
                table: "Splits");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Alarms");

            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "Alarms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Alarms");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Alarms");

            migrationBuilder.AlterColumn<string>(
                name: "TimerUserId",
                table: "Splits",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TimerId1",
                table: "Splits",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TimerUserId",
                table: "Alarms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TimerId1",
                table: "Alarms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_Timers_TimerId1_TimerUserId",
                table: "Alarms",
                columns: new[] { "TimerId1", "TimerUserId" },
                principalTable: "Timers",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Splits_Timers_TimerId1_TimerUserId",
                table: "Splits",
                columns: new[] { "TimerId1", "TimerUserId" },
                principalTable: "Timers",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
