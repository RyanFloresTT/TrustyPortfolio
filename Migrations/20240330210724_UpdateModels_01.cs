using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustyPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Projects_ProjectId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ProjectId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Heading",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Heading",
                table: "BlogPosts");

            migrationBuilder.CreateTable(
                name: "ProjectTag",
                columns: table => new
                {
                    ProjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTag", x => new { x.ProjectsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProjectTag_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTag_TagsId",
                table: "ProjectTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTag");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Heading",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Heading",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProjectId",
                table: "Tags",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Projects_ProjectId",
                table: "Tags",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
