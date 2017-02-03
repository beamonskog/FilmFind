using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class userRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MothersName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserAverageRating",
                table: "AllMovies");

            migrationBuilder.AddColumn<string>(
                name: "ListUserRatings",
                table: "AllMovies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListUserRatings",
                table: "AllMovies");

            migrationBuilder.AddColumn<string>(
                name: "MothersName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAverageRating",
                table: "AllMovies",
                nullable: false,
                defaultValue: 0);
        }
    }
}
