using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Checkout.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CurrentOrderStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ContactInfo_FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContactInfo_LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContactInfo_Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DeliveryAddress_Street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeliveryAddress_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DeliveryAddress_Region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DeliveryAddress_PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CurrentPaymentMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CardDetails_CardName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CardDetails_CardNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CardDetails_Expiration = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CardDetails_Cvv = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CurrentPaymentStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CatalogItemName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountName",
                table: "Orders",
                column: "AccountName");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CurrentOrderStatus",
                table: "Orders",
                column: "CurrentOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CurrentPaymentStatus",
                table: "Orders",
                column: "CurrentPaymentStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
