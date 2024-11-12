using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class ProductoIngrediente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(4138),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(249));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(1076),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(8766));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 152, DateTimeKind.Utc).AddTicks(7228),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(6766));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "productos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(9840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(2997));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "producto_ingrediente",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(7982),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(2359));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(2523),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(9490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 154, DateTimeKind.Utc).AddTicks(3241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(4157));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "ingredientes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(6486),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 152, DateTimeKind.Utc).AddTicks(9551),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(7966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 154, DateTimeKind.Utc).AddTicks(1804),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(3541));

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_created_at",
                table: "usuarios",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_deleted_at",
                table: "usuarios",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_updated_at",
                table: "usuarios",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_telefonos_created_at",
                table: "telefonos",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_telefonos_deleted_at",
                table: "telefonos",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_telefonos_updated_at",
                table: "telefonos",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_roles_created_at",
                table: "roles",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_roles_deleted_at",
                table: "roles",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_roles_updated_at",
                table: "roles",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_productos_created_at",
                table: "productos",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_productos_deleted_at",
                table: "productos",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_productos_unidad",
                table: "productos",
                column: "unidad");

            migrationBuilder.CreateIndex(
                name: "IX_productos_updated_at",
                table: "productos",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_producto_ingrediente_created_at",
                table: "producto_ingrediente",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_producto_ingrediente_deleted_at",
                table: "producto_ingrediente",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_producto_ingrediente_unidad",
                table: "producto_ingrediente",
                column: "unidad");

            migrationBuilder.CreateIndex(
                name: "IX_producto_ingrediente_updated_at",
                table: "producto_ingrediente",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_personas_created_at",
                table: "personas",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_personas_deleted_at",
                table: "personas",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_personas_updated_at",
                table: "personas",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_created_at",
                table: "pedidos",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_deleted_at",
                table: "pedidos",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_updated_at",
                table: "pedidos",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_ingredientes_created_at",
                table: "ingredientes",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_ingredientes_deleted_at",
                table: "ingredientes",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ingredientes_unidad",
                table: "ingredientes",
                column: "unidad");

            migrationBuilder.CreateIndex(
                name: "IX_ingredientes_updated_at",
                table: "ingredientes",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_domicilios_created_at",
                table: "domicilios",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_domicilios_deleted_at",
                table: "domicilios",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_domicilios_updated_at",
                table: "domicilios",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedido_created_at",
                table: "detalle_pedido",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedido_deleted_at",
                table: "detalle_pedido",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedido_updated_at",
                table: "detalle_pedido",
                column: "updated_at");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuarios_created_at",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_deleted_at",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_updated_at",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_telefonos_created_at",
                table: "telefonos");

            migrationBuilder.DropIndex(
                name: "IX_telefonos_deleted_at",
                table: "telefonos");

            migrationBuilder.DropIndex(
                name: "IX_telefonos_updated_at",
                table: "telefonos");

            migrationBuilder.DropIndex(
                name: "IX_roles_created_at",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_roles_deleted_at",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_roles_updated_at",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_productos_created_at",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_productos_deleted_at",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_productos_unidad",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_productos_updated_at",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_producto_ingrediente_created_at",
                table: "producto_ingrediente");

            migrationBuilder.DropIndex(
                name: "IX_producto_ingrediente_deleted_at",
                table: "producto_ingrediente");

            migrationBuilder.DropIndex(
                name: "IX_producto_ingrediente_unidad",
                table: "producto_ingrediente");

            migrationBuilder.DropIndex(
                name: "IX_producto_ingrediente_updated_at",
                table: "producto_ingrediente");

            migrationBuilder.DropIndex(
                name: "IX_personas_created_at",
                table: "personas");

            migrationBuilder.DropIndex(
                name: "IX_personas_deleted_at",
                table: "personas");

            migrationBuilder.DropIndex(
                name: "IX_personas_updated_at",
                table: "personas");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_created_at",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_deleted_at",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_updated_at",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_ingredientes_created_at",
                table: "ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_ingredientes_deleted_at",
                table: "ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_ingredientes_unidad",
                table: "ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_ingredientes_updated_at",
                table: "ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_domicilios_created_at",
                table: "domicilios");

            migrationBuilder.DropIndex(
                name: "IX_domicilios_deleted_at",
                table: "domicilios");

            migrationBuilder.DropIndex(
                name: "IX_domicilios_updated_at",
                table: "domicilios");

            migrationBuilder.DropIndex(
                name: "IX_detalle_pedido_created_at",
                table: "detalle_pedido");

            migrationBuilder.DropIndex(
                name: "IX_detalle_pedido_deleted_at",
                table: "detalle_pedido");

            migrationBuilder.DropIndex(
                name: "IX_detalle_pedido_updated_at",
                table: "detalle_pedido");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(249),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(4138));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(8766),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(1076));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(6766),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 152, DateTimeKind.Utc).AddTicks(7228));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "productos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(2997),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(9840));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "producto_ingrediente",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(2359),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(7982));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(9490),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(2523));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(4157),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 154, DateTimeKind.Utc).AddTicks(3241));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "ingredientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(1750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(6486));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(7966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 152, DateTimeKind.Utc).AddTicks(9551));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(3541),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 154, DateTimeKind.Utc).AddTicks(1804));
        }
    }
}
