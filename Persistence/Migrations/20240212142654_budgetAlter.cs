using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class budgetAlter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Budget");

            migrationBuilder.UpdateData(
                table: "Budget",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 2, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(6060), new DateTime(2024, 3, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(6036), new DateTime(2024, 2, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(6029) });

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(1050));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(1066));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(1067));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(1068));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(1070));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 350, DateTimeKind.Local).AddTicks(1071));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 351, DateTimeKind.Local).AddTicks(616));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 351, DateTimeKind.Local).AddTicks(621));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 351, DateTimeKind.Local).AddTicks(623));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 351, DateTimeKind.Local).AddTicks(624));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 351, DateTimeKind.Local).AddTicks(625));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 351, DateTimeKind.Local).AddTicks(626));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 351, DateTimeKind.Local).AddTicks(627));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 17, 26, 54, 351, DateTimeKind.Local).AddTicks(628));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Budget",
                type: "decimal(16,2)",
                precision: 16,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Budget",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "EndDate", "StartDate", "TotalAmount" },
                values: new object[] { new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(9441), new DateTime(2024, 3, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(9421), new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(9418), 3000m });

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5403));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5416));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5419));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5421));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3506));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3511));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3513));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3514));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3515));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3516));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3517));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3518));
        }
    }
}
