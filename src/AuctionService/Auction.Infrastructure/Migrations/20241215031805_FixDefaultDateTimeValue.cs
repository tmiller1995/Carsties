using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDefaultDateTimeValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "auctions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW() AT TIME ZONE 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 3, 2, 6, 787, DateTimeKind.Utc).AddTicks(9574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "auctions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW() AT TIME ZONE 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 3, 2, 6, 786, DateTimeKind.Utc).AddTicks(9663));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "auctions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 3, 2, 6, 787, DateTimeKind.Utc).AddTicks(9574),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW() AT TIME ZONE 'UTC'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "auctions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 3, 2, 6, 786, DateTimeKind.Utc).AddTicks(9663),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW() AT TIME ZONE 'UTC'");
        }
    }
}
