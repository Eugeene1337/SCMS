using Microsoft.EntityFrameworkCore.Migrations;

namespace SCMS.API.Migrations
{
    public partial class AddPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PacketId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StripePriceId",
                table: "Packets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripeProductId",
                table: "Packets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PacketId",
                table: "Payments",
                column: "PacketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Packets_PacketId",
                table: "Payments",
                column: "PacketId",
                principalTable: "Packets",
                principalColumn: "PacketId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Packets_PacketId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PacketId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PacketId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StripePriceId",
                table: "Packets");

            migrationBuilder.DropColumn(
                name: "StripeProductId",
                table: "Packets");
        }
    }
}
