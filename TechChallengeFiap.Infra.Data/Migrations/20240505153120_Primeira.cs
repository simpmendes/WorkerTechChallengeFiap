using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallengeFiap.Infra.Data.Migrations
{
    public partial class Primeira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Simbolo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    NomeUsuario = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Permissao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcaoUsuario",
                columns: table => new
                {
                    AcoesId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcaoUsuario", x => new { x.AcoesId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_AcaoUsuario_Acoes_AcoesId",
                        column: x => x.AcoesId,
                        principalTable: "Acoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcaoUsuario_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultaAcoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaAcoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultaAcoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoAcoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Id_Acao = table.Column<int>(type: "int", nullable: false),
                    qtPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorAcao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dtPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAprovacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoAcoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoAcoes_Acoes_Id_Acao",
                        column: x => x.Id_Acao,
                        principalTable: "Acoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoAcoes_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcaoUsuario_UsuariosId",
                table: "AcaoUsuario",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaAcoes_UsuarioId",
                table: "ConsultaAcoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoAcoes_Id_Acao",
                table: "PedidoAcoes",
                column: "Id_Acao");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoAcoes_Id_Usuario",
                table: "PedidoAcoes",
                column: "Id_Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcaoUsuario");

            migrationBuilder.DropTable(
                name: "ConsultaAcoes");

            migrationBuilder.DropTable(
                name: "PedidoAcoes");

            migrationBuilder.DropTable(
                name: "Acoes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
