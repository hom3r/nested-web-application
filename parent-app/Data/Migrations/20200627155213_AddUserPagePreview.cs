using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace parent.Data.Migrations
{
    public partial class AddUserPagePreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPagePreview",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hash = table.Column<string>(nullable: true),
                    Invalidated = table.Column<bool>(nullable: false),
                    PageID = table.Column<int>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPagePreview", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPagePreview_UserPage_PageID",
                        column: x => x.PageID,
                        principalTable: "UserPage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPagePreview_PageID",
                table: "UserPagePreview",
                column: "PageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPagePreview");
        }
    }
}
