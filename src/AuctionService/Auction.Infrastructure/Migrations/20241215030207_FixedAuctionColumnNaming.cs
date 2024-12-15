using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedAuctionColumnNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Winner",
                table: "auctions",
                newName: "winner");

            migrationBuilder.RenameColumn(
                name: "SoldAmount",
                table: "auctions",
                newName: "sold_amount");

            migrationBuilder.RenameColumn(
                name: "ReservePrice",
                table: "auctions",
                newName: "reserve_price");

            migrationBuilder.RenameColumn(
                name: "CurrentHighBid",
                table: "auctions",
                newName: "current_high_bid");

            migrationBuilder.RenameColumn(
                name: "AuctionEnd",
                table: "auctions",
                newName: "auction_end");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "auctions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 3, 2, 6, 787, DateTimeKind.Utc).AddTicks(9574),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 2, 58, 8, 222, DateTimeKind.Utc).AddTicks(6123));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "auctions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 3, 2, 6, 786, DateTimeKind.Utc).AddTicks(9663),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 2, 58, 8, 221, DateTimeKind.Utc).AddTicks(6427));

            migrationBuilder.AlterColumn<string>(
                name: "winner",
                table: "auctions",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "winner",
                table: "auctions",
                newName: "Winner");

            migrationBuilder.RenameColumn(
                name: "sold_amount",
                table: "auctions",
                newName: "SoldAmount");

            migrationBuilder.RenameColumn(
                name: "reserve_price",
                table: "auctions",
                newName: "ReservePrice");

            migrationBuilder.RenameColumn(
                name: "current_high_bid",
                table: "auctions",
                newName: "CurrentHighBid");

            migrationBuilder.RenameColumn(
                name: "auction_end",
                table: "auctions",
                newName: "AuctionEnd");

            migrationBuilder.AlterColumn<string>(
                name: "Winner",
                table: "auctions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "auctions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 2, 58, 8, 222, DateTimeKind.Utc).AddTicks(6123),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 3, 2, 6, 787, DateTimeKind.Utc).AddTicks(9574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "auctions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 2, 58, 8, 221, DateTimeKind.Utc).AddTicks(6427),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 3, 2, 6, 786, DateTimeKind.Utc).AddTicks(9663));
        }
    }
}
