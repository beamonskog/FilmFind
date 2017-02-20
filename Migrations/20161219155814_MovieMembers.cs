using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class MovieMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Hyped",
                table: "AllMovies",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<int>(
                name: "Favorited",
                table: "AllMovies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorited",
                table: "AllMovies");

            migrationBuilder.AlterColumn<double>(
                name: "Hyped",
                table: "AllMovies",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
