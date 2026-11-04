using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMaterials.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaterialRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialRequests_AspNetUsers_UserId",
                table: "MaterialRequests");

            migrationBuilder.DropColumn(
                name: "AdminComment",
                table: "MaterialRequests");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "MaterialRequests",
                newName: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MaterialRequests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "MaterialRequests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "RequestNumber",
                table: "MaterialRequests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialRequests_AspNetUsers_UserId",
                table: "MaterialRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialRequests_AspNetUsers_UserId",
                table: "MaterialRequests");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "MaterialRequests",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MaterialRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "MaterialRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestNumber",
                table: "MaterialRequests",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminComment",
                table: "MaterialRequests",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialRequests_AspNetUsers_UserId",
                table: "MaterialRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
