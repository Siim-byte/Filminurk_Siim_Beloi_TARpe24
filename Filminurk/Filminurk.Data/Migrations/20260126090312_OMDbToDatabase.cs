using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class OMDbToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OmdbToDatabase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Released = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Actors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImdbRating = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OmdbToDatabase", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OmdbToDatabase");
        }
    }
}
