using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallengeFiap.Infra.Data.Migrations
{
    public partial class colunaValoracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorAcao",
                table: "Acoes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorAcao",
                table: "Acoes");
        }
    }
}
