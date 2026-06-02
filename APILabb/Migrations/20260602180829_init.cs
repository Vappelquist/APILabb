using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APILabb.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    InterestID = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Connections_Interests_InterestID",
                        column: x => x.InterestID,
                        principalTable: "Interests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "ID", "Description", "InterestName" },
                values: new object[,]
                {
                    { 1, null, "Programming" },
                    { 2, null, "Cooking" },
                    { 3, null, "Traveling" },
                    { 4, null, "Sports" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Alice", "1234567890" },
                    { 2, "Bob", "0987654321" },
                    { 3, "Charlie", "5555555555" },
                    { 4, "David", "1111111111" }
                });

            migrationBuilder.InsertData(
                table: "Connections",
                columns: new[] { "ID", "InterestID", "URL", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "https://www.youtube.com", 1 },
                    { 2, 1, "https://www.google.com", 1 },
                    { 3, 2, "https://www.google.com", 1 },
                    { 4, 3, "https://www.microsoft.com", 3 },
                    { 5, 4, "https://www.github.com", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_InterestID",
                table: "Connections",
                column: "InterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_UserID",
                table: "Connections",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
