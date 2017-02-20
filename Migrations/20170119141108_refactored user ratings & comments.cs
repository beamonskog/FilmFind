using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FilmFind.Migrations
{
    public partial class refactoreduserratingscomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMovieRatings");

            migrationBuilder.AlterColumn<int>(
                name: "UserRating",
                table: "UserMovies",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MovieId",
                table: "AspNetUsers",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AllMovies_MovieId",
                table: "AspNetUsers",
                column: "MovieId",
                principalTable: "AllMovies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AllMovies_MovieId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MovieId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserRating",
                table: "UserMovies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "UserMovieRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: true),
                    UserRating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMovieRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMovieRatings_AllMovies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "AllMovies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMovieRatings_MovieId",
                table: "UserMovieRatings",
                column: "MovieId");
        }
    }
}
