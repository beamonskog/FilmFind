using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class renamefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyRating",
                table: "AllMovies");

            migrationBuilder.AddColumn<int>(
                name: "UserAverageRating",
                table: "AllMovies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAverageRating",
                table: "AllMovies");

            migrationBuilder.AddColumn<int>(
                name: "MyRating",
                table: "AllMovies",
                nullable: false,
                defaultValue: 0);
        }
    }
}
