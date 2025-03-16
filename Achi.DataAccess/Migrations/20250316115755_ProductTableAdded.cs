using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class ProductTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Ray Bradbury", "Kitapların yasaklandığı ve itfaiyecilerin onları yakmakla görevli olduğu bir toplumda, Guy Montag bu düzeni sorgulamaya başlar. Gerçeği arayışa çıktıkça, özgürlüğün ve bilginin gerçek değerini keşfeder.", "9783060311354", 9.9900000000000002, 8.9900000000000002, 7.9900000000000002, 8.4900000000000002, "Fahrenheit 451" },
                    { 2, "Agatha Christie", "Roger Ackroyd, aldığı bir mektupla büyük bir sırrı öğrenmesinin ardından evinde ölü bulunur. Emekli dedektif Hercule Poirot, kasabanın derinlerine gizlenmiş sırları açığa çıkararak katili bulmaya çalışır.", "9780006167921", 15.99, 14.99, 13.99, 14.49, "Roger Ackroyd Cinayeti" },
                    { 3, "Anthony Burgess", "Şiddet ve suçla dolu bir gelecekte, genç Alex ve çetesi acımasızca eğlenirken, yakalandığında devletin onu “iyileştirmek” için uyguladığı deneysel bir yönteme maruz kalır. Ancak, özgür iradenin yok edildiği bu süreç, insan doğası ve ahlaki seçimler üzerine derin sorular ortaya çıkarır.", "9780393089134", 9.9900000000000002, 8.9900000000000002, 7.9900000000000002, 8.4900000000000002, "Otomatik Portakal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
