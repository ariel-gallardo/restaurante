using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class PedidoRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalle_pedido_pedidos_PedidoId",
                table: "detalle_pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_pedido_productos_ProductoId",
                table: "detalle_pedido");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "detalle_pedido",
                newName: "producto_id");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "detalle_pedido",
                newName: "pedido_id");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_pedido_ProductoId",
                table: "detalle_pedido",
                newName: "IX_detalle_pedido_producto_id");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_pedido_PedidoId",
                table: "detalle_pedido",
                newName: "IX_detalle_pedido_pedido_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(6653),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(5951));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(3713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(2288));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(453),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 30, DateTimeKind.Utc).AddTicks(7792));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(3482),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(4557));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(1680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(2245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(4978),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(7462),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(9591));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(8897),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(9039));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(2386),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(363));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(6004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(7792));

            migrationBuilder.AlterColumn<string>(
                name: "producto_id",
                table: "detalle_pedido",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pedido_id",
                table: "detalle_pedido",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_pedido_pedidos_pedido_id",
                table: "detalle_pedido",
                column: "pedido_id",
                principalTable: "pedidos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_pedido_productos_producto_id",
                table: "detalle_pedido",
                column: "producto_id",
                principalTable: "productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalle_pedido_pedidos_pedido_id",
                table: "detalle_pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_pedido_productos_producto_id",
                table: "detalle_pedido");

            migrationBuilder.RenameColumn(
                name: "producto_id",
                table: "detalle_pedido",
                newName: "ProductoId");

            migrationBuilder.RenameColumn(
                name: "pedido_id",
                table: "detalle_pedido",
                newName: "PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_pedido_producto_id",
                table: "detalle_pedido",
                newName: "IX_detalle_pedido_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_pedido_pedido_id",
                table: "detalle_pedido",
                newName: "IX_detalle_pedido_PedidoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(5951),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(6653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(2288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 30, DateTimeKind.Utc).AddTicks(7792),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(453));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(4557),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(3482));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(2245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(4126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(4978));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(9591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(7462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(9039),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(8897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(363),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 257, DateTimeKind.Utc).AddTicks(2386));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(7792),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 13, 33, 13, 258, DateTimeKind.Utc).AddTicks(6004));

            migrationBuilder.AlterColumn<string>(
                name: "ProductoId",
                table: "detalle_pedido",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PedidoId",
                table: "detalle_pedido",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_pedido_pedidos_PedidoId",
                table: "detalle_pedido",
                column: "PedidoId",
                principalTable: "pedidos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_pedido_productos_ProductoId",
                table: "detalle_pedido",
                column: "ProductoId",
                principalTable: "productos",
                principalColumn: "id");
        }
    }
}
