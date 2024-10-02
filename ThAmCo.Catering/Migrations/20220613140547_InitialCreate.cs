using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Catering.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "thamco.catering");

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "thamco.catering",
                columns: table => new
                {
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodBookingId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "FoodBooking",
                schema: "thamco.catering",
                columns: table => new
                {
                    FoodBookingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBooking", x => x.FoodBookingId);
                    table.ForeignKey(
                        name: "FK_FoodBooking_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "thamco.catering",
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodList",
                schema: "thamco.catering",
                columns: table => new
                {
                    FoodListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodList", x => x.FoodListId);
                    table.ForeignKey(
                        name: "FK_FoodList_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "thamco.catering",
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "thamco.catering",
                table: "Menu",
                columns: new[] { "MenuId", "FoodBookingId", "MenuName" },
                values: new object[,]
                {
                    { "1", null, "French Menu" },
                    { "2", null, "Canton Menu" },
                    { "3", null, "Italian Menu" },
                    { "4", null, "Malay Menu" }
                });

            migrationBuilder.InsertData(
                schema: "thamco.catering",
                table: "FoodBooking",
                columns: new[] { "FoodBookingId", "Date", "MenuId" },
                values: new object[,]
                {
                    { "1", "06-06-2022", "1" },
                    { "2", "07-06-2022", "2" },
                    { "3", "08-06-2022", "3" },
                    { "4", "09-06-2022", "4" }
                });

            migrationBuilder.InsertData(
                schema: "thamco.catering",
                table: "FoodList",
                columns: new[] { "FoodListId", "FoodName", "MenuId", "Price" },
                values: new object[,]
                {
                    { 1, "Soupe à l’oignon", "1", 10.0 },
                    { 2, "Coq au vin", "1", 20.0 },
                    { 3, "Cassoulet", "1", 30.0 },
                    { 4, "Bœuf bourguignon", "1", 40.0 },
                    { 5, "Chocolate soufflé", "1", 50.0 },
                    { 6, "Steam Mihun with Ginger Wine Chicken", "2", 60.0 },
                    { 7, "Guang Xi Style Braised Pork with Man Tou ", "2", 70.0 },
                    { 8, "Guang Xi Style Stuffed Taufu Ball ", "2", 80.0 },
                    { 9, "Claypot Kampung Chicken with Sesame Oil & Ginger ", "2", 90.0 },
                    { 10, "Sweet and sour pork", "2", 100.0 },
                    { 11, "Lasagna Bolognese", "3", 10.0 },
                    { 12, "Veal Milanese", "3", 20.0 },
                    { 13, "Gnocchi Sorrento", "3", 30.0 },
                    { 14, "Spaghetti Carbonara", "3", 40.0 },
                    { 15, "Antipasto Italiano", "3", 50.0 },
                    { 16, "Ayam Percik", "4", 60.0 },
                    { 17, "Nasi Kerabu", "4", 70.0 },
                    { 18, "Nasi Lemak", "4", 80.0 },
                    { 19, "Beef Rendang", "4", 90.0 },
                    { 20, "Laksa", "4", 100.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodBooking_MenuId",
                schema: "thamco.catering",
                table: "FoodBooking",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodList_MenuId",
                schema: "thamco.catering",
                table: "FoodList",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodBooking",
                schema: "thamco.catering");

            migrationBuilder.DropTable(
                name: "FoodList",
                schema: "thamco.catering");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "thamco.catering");
        }
    }
}
