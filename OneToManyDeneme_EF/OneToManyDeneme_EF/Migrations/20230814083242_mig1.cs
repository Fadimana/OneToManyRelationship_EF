using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneToManyDeneme_EF.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departmanlar1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmanlar1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calisanlar1_Departmanlar1_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar1",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar1_DepartmanId",
                table: "Calisanlar1",
                column: "DepartmanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calisanlar1");

            migrationBuilder.DropTable(
                name: "Departmanlar1");
        }
    }
}
