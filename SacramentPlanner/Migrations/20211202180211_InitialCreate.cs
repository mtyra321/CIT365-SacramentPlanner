using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentPlanner.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hymn",
                columns: table => new
                {
                    HymnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hymn", x => x.HymnId);
                });

            migrationBuilder.CreateTable(
                name: "SacramentPlanner",
                columns: table => new
                {
                    SacramentPlannerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Presiding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conducting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningHymnHymnId = table.Column<int>(type: "int", nullable: true),
                    SacramentHymnHymnId = table.Column<int>(type: "int", nullable: true),
                    IntermediateHymnHymnId = table.Column<int>(type: "int", nullable: true),
                    ClosingHymnHymnId = table.Column<int>(type: "int", nullable: true),
                    OpeningPrayer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosingPrayer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SacramentPlanner", x => x.SacramentPlannerId);
                    table.ForeignKey(
                        name: "FK_SacramentPlanner_Hymn_ClosingHymnHymnId",
                        column: x => x.ClosingHymnHymnId,
                        principalTable: "Hymn",
                        principalColumn: "HymnId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SacramentPlanner_Hymn_IntermediateHymnHymnId",
                        column: x => x.IntermediateHymnHymnId,
                        principalTable: "Hymn",
                        principalColumn: "HymnId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SacramentPlanner_Hymn_OpeningHymnHymnId",
                        column: x => x.OpeningHymnHymnId,
                        principalTable: "Hymn",
                        principalColumn: "HymnId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SacramentPlanner_Hymn_SacramentHymnHymnId",
                        column: x => x.SacramentHymnHymnId,
                        principalTable: "Hymn",
                        principalColumn: "HymnId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SacramentPlanner_ClosingHymnHymnId",
                table: "SacramentPlanner",
                column: "ClosingHymnHymnId");

            migrationBuilder.CreateIndex(
                name: "IX_SacramentPlanner_IntermediateHymnHymnId",
                table: "SacramentPlanner",
                column: "IntermediateHymnHymnId");

            migrationBuilder.CreateIndex(
                name: "IX_SacramentPlanner_OpeningHymnHymnId",
                table: "SacramentPlanner",
                column: "OpeningHymnHymnId");

            migrationBuilder.CreateIndex(
                name: "IX_SacramentPlanner_SacramentHymnHymnId",
                table: "SacramentPlanner",
                column: "SacramentHymnHymnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SacramentPlanner");

            migrationBuilder.DropTable(
                name: "Hymn");
        }
    }
}
