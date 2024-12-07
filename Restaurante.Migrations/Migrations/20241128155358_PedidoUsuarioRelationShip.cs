using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class PedidoUsuarioRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(3726),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(6653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(450),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 665, DateTimeKind.Utc).AddTicks(6629),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(453));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 667, DateTimeKind.Utc).AddTicks(678),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(3482));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(8624),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(2108),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(4978));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 667, DateTimeKind.Utc).AddTicks(4824),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(7462));

            migrationBuilder.AddColumn<long>(
                name: "usuario_id",
                table: "pedidos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(6220),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(8897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 665, DateTimeKind.Utc).AddTicks(8687),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(2386));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 667, DateTimeKind.Utc).AddTicks(3171),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(6004));

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_usuario_id",
                table: "pedidos",
                column: "usuario_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_usuarios_usuario_id",
                table: "pedidos",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_usuarios_usuario_id",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_usuario_id",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "pedidos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(6653),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(3726));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(3713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(453),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 665, DateTimeKind.Utc).AddTicks(6629));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(3482),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 667, DateTimeKind.Utc).AddTicks(678));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(1680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(8624));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(4978),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(2108));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(7462),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 667, DateTimeKind.Utc).AddTicks(4824));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(8897),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 666, DateTimeKind.Utc).AddTicks(6220));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(2386),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 665, DateTimeKind.Utc).AddTicks(8687));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(6004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 15, 53, 57, 667, DateTimeKind.Utc).AddTicks(3171));
        }
    }
}
