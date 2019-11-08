using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoInterestCalculator.Data.Migrations
{
    public partial class Update2IdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalculatedBy",
                table: "InterestFormValues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculatedBy",
                table: "InterestFormValues");
        }
    }
}
