using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeItUpData.Library.Migrations
{
    public partial class InitTimeItUpEFDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timers", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_Timers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimerId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "Splits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimerId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "Splits");

            migrationBuilder.DropTable(
                name: "Timers");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
