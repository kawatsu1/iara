using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iara.Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cotacao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJComprador = table.Column<string>(type: "VARCHAR(14)", maxLength: 14, nullable: false),
                    CNPJFornecedor = table.Column<string>(type: "VARCHAR(14)", maxLength: 14, nullable: false),
                    NumeroCotacao = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    DataCotacao = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    DataEntregaCotacao = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    CEP = table.Column<string>(type: "VARCHAR(9)", maxLength: 9, nullable: false),
                    Logradouro = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    Complemento = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    UF = table.Column<string>(type: "VARCHAR(2)", maxLength: 2, nullable: true),
                    Observacao = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CotacaoItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CotacaoId = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    NumeroItem = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Preco = table.Column<double>(type: "FLOAT", nullable: false),
                    Quantidade = table.Column<int>(type: "INT", nullable: false),
                    Marca = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Unidade = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotacaoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CotacaoItem_Cotacao_CotacaoId",
                        column: x => x.CotacaoId,
                        principalTable: "Cotacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CotacaoItem_CotacaoId",
                table: "CotacaoItem",
                column: "CotacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CotacaoItem");

            migrationBuilder.DropTable(
                name: "Cotacao");
        }
    }
}
