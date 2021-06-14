using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BurgerBackend.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    PostCode = table.Column<string>(type: "text", nullable: false),
                    GLat = table.Column<string>(type: "text", nullable: true),
                    GLong = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Hours",
                columns: table => new
                {
                    HoursID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MondayOpen = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    MondayClose = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    TuesdayOpen = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    TuesdayClose = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    WednesdayOpen = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    WednesdayClose = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    ThursdayOpen = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    ThursdayClose = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    FridayOpen = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    FridayClose = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    SaturdayOpen = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    SaturdayClose = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    SundayOpen = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    SundayClose = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    RestaurantName = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hours", x => x.HoursID);
                    table.ForeignKey(
                        name: "FK_Hours_Restaurants_RestaurantName",
                        column: x => x.RestaurantName,
                        principalTable: "Restaurants",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Taste = table.Column<int>(type: "integer", nullable: false),
                    Texture = table.Column<int>(type: "integer", nullable: false),
                    VisualRepresentation = table.Column<int>(type: "integer", nullable: false),
                    RestaurantName = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Restaurants_RestaurantName",
                        column: x => x.RestaurantName,
                        principalTable: "Restaurants",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hours_RestaurantName",
                table: "Hours",
                column: "RestaurantName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantName",
                table: "Reviews",
                column: "RestaurantName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hours");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
