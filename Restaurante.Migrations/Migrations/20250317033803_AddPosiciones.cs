using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class AddPosiciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_usuarios_usuario_id",
                table: "pedidos");

            migrationBuilder.AddColumn<long>(
                name: "delivery_id",
                table: "pedidos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "posiciones",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    delivery_id = table.Column<long>(type: "bigint", nullable: false),
                    pedido_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    latitud = table.Column<double>(type: "float", nullable: false),
                    longitud = table.Column<double>(type: "float", nullable: false),
                    velocidad = table.Column<int>(type: "int", nullable: false),
                    direccion = table.Column<int>(type: "int", nullable: false),
                    tiempo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posiciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_posiciones_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posiciones_usuarios_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_delivery_id",
                table: "pedidos",
                column: "delivery_id");

            migrationBuilder.CreateIndex(
                name: "IX_posiciones_created_at",
                table: "posiciones",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_posiciones_deleted_at",
                table: "posiciones",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_posiciones_delivery_id",
                table: "posiciones",
                column: "delivery_id");

            migrationBuilder.CreateIndex(
                name: "IX_posiciones_pedido_id",
                table: "posiciones",
                column: "pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_posiciones_updated_at",
                table: "posiciones",
                column: "updated_at");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_usuarios_delivery_id",
                table: "pedidos",
                column: "delivery_id",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_usuarios_usuario_id",
                table: "pedidos",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_usuarios_delivery_id",
                table: "pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_usuarios_usuario_id",
                table: "pedidos");

            migrationBuilder.DropTable(
                name: "posiciones");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_delivery_id",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "delivery_id",
                table: "pedidos");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_usuarios_usuario_id",
                table: "pedidos",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
