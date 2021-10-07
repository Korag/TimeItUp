using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeItUpData.Library.Migrations
{
    public partial class RefactoredEntitiesByUsingDataAnnotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDuration = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TotalPausedTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paused = table.Column<bool>(type: "bit", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    SplitsNumber = table.Column<int>(type: "int", nullable: false),
                    AlarmsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timers", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_Timers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TimerId = table.Column<int>(type: "int", nullable: false),
                    TimerId1 = table.Column<int>(type: "int", nullable: false),
                    TimerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => new { x.Id, x.TimerId });
                    table.ForeignKey(
                        name: "FK_Alarms_Timers_TimerId1_TimerUserId",
                        columns: x => new { x.TimerId1, x.TimerUserId },
                        principalTable: "Timers",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pauses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TimerId = table.Column<int>(type: "int", nullable: false),
                    TimerId1 = table.Column<int>(type: "int", nullable: false),
                    TimerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDuration = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pauses", x => new { x.Id, x.TimerId });
                    table.ForeignKey(
                        name: "FK_Pauses_Timers_TimerId1_TimerUserId",
                        columns: x => new { x.TimerId1, x.TimerUserId },
                        principalTable: "Timers",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Splits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TimerId = table.Column<int>(type: "int", nullable: false),
                    TimerId1 = table.Column<int>(type: "int", nullable: false),
                    TimerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDuration = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Splits", x => new { x.Id, x.TimerId });
                    table.ForeignKey(
                        name: "FK_Splits_Timers_TimerId1_TimerUserId",
                        columns: x => new { x.TimerId1, x.TimerUserId },
                        principalTable: "Timers",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_TimerId1_TimerUserId",
                table: "Alarms",
                columns: new[] { "TimerId1", "TimerUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Pauses_TimerId1_TimerUserId",
                table: "Pauses",
                columns: new[] { "TimerId1", "TimerUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Splits_TimerId1_TimerUserId",
                table: "Splits",
                columns: new[] { "TimerId1", "TimerUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Timers_UserId",
                table: "Timers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "Pauses");

            migrationBuilder.DropTable(
                name: "Splits");

            migrationBuilder.DropTable(
                name: "Timers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
