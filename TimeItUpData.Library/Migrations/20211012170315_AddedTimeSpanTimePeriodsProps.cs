using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeItUpData.Library.Migrations
{
    public partial class AddedTimeSpanTimePeriodsProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmsNumber",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "SplitsNumber",
                table: "Timers");

            migrationBuilder.AddColumn<string>(
                name: "TotalCountdownTime",
                table: "Timers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalCountdownTimeSpan",
                table: "Timers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalDurationTimeSpan",
                table: "Timers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalPausedTimeSpan",
                table: "Timers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalDurationTimeSpan",
                table: "Splits",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalDurationTimeSpan",
                table: "Pauses",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCountdownTime",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "TotalCountdownTimeSpan",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "TotalDurationTimeSpan",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "TotalPausedTimeSpan",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "TotalDurationTimeSpan",
                table: "Splits");

            migrationBuilder.DropColumn(
                name: "TotalDurationTimeSpan",
                table: "Pauses");

            migrationBuilder.AddColumn<int>(
                name: "AlarmsNumber",
                table: "Timers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SplitsNumber",
                table: "Timers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
