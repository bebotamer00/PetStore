using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableWorkHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDay",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "StartDay",
                table: "WorkingHours");

            migrationBuilder.AddColumn<string>(
                name: "WeekDay",
                table: "WorkingHours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "WorkingHours");

            migrationBuilder.AddColumn<int>(
                name: "EndDay",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartDay",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
