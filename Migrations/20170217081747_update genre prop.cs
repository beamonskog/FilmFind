using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FilmFind.Migrations
{
    public partial class updategenreprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AllMovies_MovieId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Banana_MyFruitId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Banana");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MyFruitId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllMovies",
                table: "AllMovies");

            migrationBuilder.DropColumn(
                name: "MyFruitId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "AllMovies");

            migrationBuilder.RenameTable(
                name: "AllMovies",
                newName: "Movies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Genre = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieId",
                table: "Genres",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Movies_MovieId",
                table: "AspNetUsers",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Movies_MovieId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "AllMovies");

            migrationBuilder.AddColumn<int>(
                name: "MyFruitId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "AllMovies",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllMovies",
                table: "AllMovies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Banana",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banana", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MyFruitId",
                table: "AspNetUsers",
                column: "MyFruitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AllMovies_MovieId",
                table: "AspNetUsers",
                column: "MovieId",
                principalTable: "AllMovies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Banana_MyFruitId",
                table: "AspNetUsers",
                column: "MyFruitId",
                principalTable: "Banana",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
