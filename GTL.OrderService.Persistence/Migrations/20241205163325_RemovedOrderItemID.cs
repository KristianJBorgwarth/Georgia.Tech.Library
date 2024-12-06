using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTL.OrderService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedOrderItemID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderItem_OrderItemId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderItemId",
                table: "Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderItemId",
                table: "Order",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderItem_OrderItemId",
                table: "Order",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
