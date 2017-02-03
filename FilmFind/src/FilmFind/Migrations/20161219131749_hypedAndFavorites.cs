using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FilmFind.Migrations
{
    public partial class hypedAndFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "MyMovies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId2",
                table: "MyMovies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Hyped",
                table: "AllMovies",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "FavoriteMovies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMovies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HypedMovies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HypedMovies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyMovies_UserId1",
                table: "MyMovies",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_MyMovies_UserId2",
                table: "MyMovies",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId1",
                table: "MyMovies",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId2",
                table: "MyMovies",
                column: "UserId2",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId1",
                table: "MyMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId2",
                table: "MyMovies");

            migrationBuilder.DropTable(
                name: "FavoriteMovies");

            migrationBuilder.DropTable(
                name: "HypedMovies");

            migrationBuilder.DropIndex(
                name: "IX_MyMovies_UserId1",
                table: "MyMovies");

            migrationBuilder.DropIndex(
                name: "IX_MyMovies_UserId2",
                table: "MyMovies");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "MyMovies");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "MyMovies");

            migrationBuilder.DropColumn(
                name: "Hyped",
                table: "AllMovies");
        }
    }
}
