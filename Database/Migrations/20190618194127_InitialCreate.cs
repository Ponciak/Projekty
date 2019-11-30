using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Librarys",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeofMedia = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    AutorID = table.Column<int>(nullable: false),
                    Year = table.Column<string>(nullable: false),
                    DateRent = table.Column<DateTime>(nullable: true),
                    Renter = table.Column<string>(nullable: true),
                    Lender = table.Column<string>(nullable: true),
                    BlobID = table.Column<string>(nullable: false),
                    State = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Librarys_Autors_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Autors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Librarys_AutorID",
                table: "Librarys",
                column: "AutorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Librarys");

            migrationBuilder.DropTable(
                name: "Autors");
        }
    }
}
