using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Persistent.Migrations
{
    public partial class create_prova_context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PRV");

            migrationBuilder.CreateTable(
                name: "Pessoa",
                schema: "PRV",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoCivil = table.Column<string>(maxLength: 50, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomeParceiro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataNascimentoParceiro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoa",
                schema: "PRV");
        }
    }
}
