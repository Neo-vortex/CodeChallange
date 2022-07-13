using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeChallenge.Migrations
{
    public partial class update_maze_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Heuristics",
                table: "Mazes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoSolutionDefinitely",
                table: "Mazes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PrimitiveAnalysis",
                table: "Mazes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Heuristics",
                table: "Mazes");

            migrationBuilder.DropColumn(
                name: "NoSolutionDefinitely",
                table: "Mazes");

            migrationBuilder.DropColumn(
                name: "PrimitiveAnalysis",
                table: "Mazes");
        }
    }
}
