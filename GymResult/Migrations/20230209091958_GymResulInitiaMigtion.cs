using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymResult.Migrations
{
    /// <inheritdoc />
    public partial class GymResulInitiaMigtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExercicesDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Series = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercicesDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExercicesDto_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Series = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "Date", "Description", "Location" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 9, 10, 19, 58, 325, DateTimeKind.Local).AddTicks(2915), "Pozytywny", "Kraków" },
                    { 2, new DateTime(2023, 2, 9, 10, 19, 58, 325, DateTimeKind.Local).AddTicks(2918), "Pozytywny", "Kraków" },
                    { 3, new DateTime(2023, 2, 9, 10, 19, 58, 325, DateTimeKind.Local).AddTicks(2921), "Lekki ból", "Sandomierz" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Category", "Series", "TrainingId", "Weight" },
                values: new object[,]
                {
                    { 1, "Klatka", 1, 1, 95.5 },
                    { 2, "Klatka", 2, 1, 95.5 },
                    { 3, "Klatka", 3, 1, 95.5 },
                    { 4, "Klatka", 1, 2, 95.5 },
                    { 5, "Klatka", 2, 2, 95.5 },
                    { 6, "Klatka", 1, 3, 95.5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercicesDto_TrainingId",
                table: "ExercicesDto",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TrainingId",
                table: "Exercises",
                column: "TrainingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercicesDto");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Trainings");
        }
    }
}
