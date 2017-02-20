using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FilmFind.Migrations
{
    public partial class renamecolumnandnewcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "MyMovies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Added = table.Column<DateTime>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    MyComment = table.Column<string>(nullable: true),
                    MyRating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyMovies", x => x.Id);
                });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllMovies",
                table: "Movies",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "AllMovies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AllMovies",
                table: "AllMovies");

            migrationBuilder.DropTable(
                name: "MyMovies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "AllMovies",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "AllMovies",
                newName: "Movies");
        }
    }
}
