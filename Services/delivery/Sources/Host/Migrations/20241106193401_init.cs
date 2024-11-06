using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Host.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dish",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    cost = table.Column<double>(type: "double precision", nullable: false),
                    restaurant_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dish", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    zip = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "delivery_address",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    zip = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    city = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    country = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_delivery_address_user_customer_id",
                        column: x => x.customer_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    order_state = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    deliverer_id = table.Column<int>(type: "integer", nullable: true),
                    delivery_adresss_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_delivery_address_delivery_adresss_id",
                        column: x => x.delivery_adresss_id,
                        principalTable: "delivery_address",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_order_user_customer_id",
                        column: x => x.customer_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_user_deliverer_id",
                        column: x => x.deliverer_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ordering",
                columns: table => new
                {
                    dish_id = table.Column<int>(type: "integer", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordering", x => new { x.order_id, x.dish_id });
                    table.ForeignKey(
                        name: "FK_ordering_dish_dish_id",
                        column: x => x.dish_id,
                        principalTable: "dish",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordering_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_delivery_address_customer_id",
                table: "delivery_address",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_customer_id",
                table: "order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_deliverer_id",
                table: "order",
                column: "deliverer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_delivery_adresss_id",
                table: "order",
                column: "delivery_adresss_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordering_dish_id",
                table: "ordering",
                column: "dish_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ordering");

            migrationBuilder.DropTable(
                name: "dish");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "delivery_address");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
