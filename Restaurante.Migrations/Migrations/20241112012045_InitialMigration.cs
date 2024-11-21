using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "domicilios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numero = table.Column<int>(type: "int", nullable: false),
                    localidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(7966)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_domicilios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ingredientes",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio_compra = table.Column<double>(type: "float", nullable: false),
                    precio_venta = table.Column<double>(type: "float", nullable: false),
                    stock_actual = table.Column<double>(type: "float", nullable: false),
                    stock_alerta = table.Column<double>(type: "float", nullable: false),
                    unidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(1750)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(4157)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio_compra = table.Column<double>(type: "float", nullable: false),
                    precio_venta = table.Column<double>(type: "float", nullable: false),
                    stock_actual = table.Column<double>(type: "float", nullable: false),
                    stock_alerta = table.Column<double>(type: "float", nullable: false),
                    unidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(2997)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(6766)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "telefonos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo_area = table.Column<int>(type: "int", nullable: false),
                    numero = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(8766)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telefonos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "detalle_pedido",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    cantidad = table.Column<double>(type: "float", nullable: false),
                    subtotal = table.Column<double>(type: "float", nullable: false),
                    PedidoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(3541)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_pedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_pedido_pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "pedidos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_detalle_pedido_productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "productos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "producto_ingrediente",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ingrediente_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    producto_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    unidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cantidad = table.Column<double>(type: "float", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(2359)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto_ingrediente", x => x.id);
                    table.ForeignKey(
                        name: "FK_producto_ingrediente_ingredientes_ingrediente_id",
                        column: x => x.ingrediente_id,
                        principalTable: "ingredientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_producto_ingrediente_productos_producto_id",
                        column: x => x.producto_id,
                        principalTable: "productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono_id = table.Column<long>(type: "bigint", nullable: true),
                    domicilio_id = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 794, DateTimeKind.Utc).AddTicks(9490)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.id);
                    table.ForeignKey(
                        name: "FK_personas_domicilios_domicilio_id",
                        column: x => x.domicilio_id,
                        principalTable: "domicilios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_personas_telefonos_telefono_id",
                        column: x => x.telefono_id,
                        principalTable: "telefonos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rol_id = table.Column<long>(type: "bigint", nullable: true, defaultValue: 1L),
                    persona_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 12, 1, 20, 44, 795, DateTimeKind.Utc).AddTicks(249)),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "domicilios",
                columns: new[] { "id", "calle", "deleted_at", "localidad", "numero", "updated_at" },
                values: new object[,]
                {
                    { 1L, "Espejo", null, "Capital", 450, null },
                    { 2L, "Patricias Mendocinas", null, "Capital", 890, null },
                    { 3L, "España", null, "Capital", 305, null },
                    { 4L, "Godoy Cruz", null, "Capital", 210, null },
                    { 5L, "Avenida San Martín", null, "Capital", 1200, null }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "deleted_at", "descripcion", "updated_at" },
                values: new object[,]
                {
                    { 1L, null, "Cliente", null },
                    { 2L, null, "Delivery", null },
                    { 3L, null, "Recepcionista", null },
                    { 4L, null, "Cocinero", null },
                    { 5L, null, "Administrador", null }
                });

            migrationBuilder.InsertData(
                table: "telefonos",
                columns: new[] { "id", "codigo_area", "deleted_at", "numero", "updated_at" },
                values: new object[,]
                {
                    { 1L, 261, null, 1235670m, null },
                    { 2L, 261, null, 9875420m, null },
                    { 3L, 261, null, 6548320m, null },
                    { 4L, 261, null, 9852010m, null },
                    { 5L, 261, null, 7116429m, null }
                });

            migrationBuilder.InsertData(
                table: "personas",
                columns: new[] { "id", "apellido", "deleted_at", "domicilio_id", "nombre", "telefono_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, "Fernández", null, 1L, "Lucía", 1L, null },
                    { 2L, "González", null, 2L, "Mateo", 2L, null },
                    { 3L, "López", null, 3L, "Sofía", 3L, null },
                    { 4L, "Rodríguez", null, 4L, "Tomás", 4L, null },
                    { 5L, "Gallardo", null, 5L, "Ariel", 5L, null }
                });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id", "deleted_at", "email", "password", "persona_id", "rol_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, null, "cliente@restaurante.com", "$2a$11$Md2.ACBRv9JENhYZo/OsdOPem7J1VyKhjCQK4/xjthVy7Hb/kxd0i", 1L, 1L, null },
                    { 2L, null, "delivery@restaurante.com", "$2a$11$Md2.ACBRv9JENhYZo/OsdOPem7J1VyKhjCQK4/xjthVy7Hb/kxd0i", 2L, 2L, null },
                    { 3L, null, "recepcionista@restaurante.com", "$2a$11$Md2.ACBRv9JENhYZo/OsdOPem7J1VyKhjCQK4/xjthVy7Hb/kxd0i", 3L, 3L, null },
                    { 4L, null, "cocinero@restaurante.com", "$2a$11$Md2.ACBRv9JENhYZo/OsdOPem7J1VyKhjCQK4/xjthVy7Hb/kxd0i", 4L, 4L, null },
                    { 5L, null, "administrador@restaurante.com", "$2a$11$Md2.ACBRv9JENhYZo/OsdOPem7J1VyKhjCQK4/xjthVy7Hb/kxd0i", 5L, 5L, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedido_PedidoId",
                table: "detalle_pedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedido_ProductoId",
                table: "detalle_pedido",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_personas_domicilio_id",
                table: "personas",
                column: "domicilio_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_telefono_id",
                table: "personas",
                column: "telefono_id");

            migrationBuilder.CreateIndex(
                name: "IX_producto_ingrediente_ingrediente_id",
                table: "producto_ingrediente",
                column: "ingrediente_id");

            migrationBuilder.CreateIndex(
                name: "IX_producto_ingrediente_producto_id",
                table: "producto_ingrediente",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_descripcion",
                table: "roles",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_persona_id",
                table: "usuarios",
                column: "persona_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_rol_id",
                table: "usuarios",
                column: "rol_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalle_pedido");

            migrationBuilder.DropTable(
                name: "producto_ingrediente");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "ingredientes");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "domicilios");

            migrationBuilder.DropTable(
                name: "telefonos");
        }
    }
}
