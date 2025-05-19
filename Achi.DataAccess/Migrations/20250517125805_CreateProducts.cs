using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Achi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Author", "CategoryID", "Description", "ISBN", "Image", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Ray Bradbury", 1, "Kitapların yasaklandığı ve itfaiyecilerin onları yakmakla görevli olduğu bir toplumda, Guy Montag bu düzeni sorgulamaya başlar. Gerçeği arayışa çıktıkça, özgürlüğün ve bilginin gerçek değerini keşfeder.", "9783060311354", "placeholder.png", 9.9900000000000002, 8.9900000000000002, 7.9900000000000002, 8.4900000000000002, "Fahrenheit 451" },
                    { 2, "Agatha Christie", 2, "Roger Ackroyd, aldığı bir mektupla büyük bir sırrı öğrenmesinin ardından evinde ölü bulunur. Emekli dedektif Hercule Poirot, kasabanın derinlerine gizlenmiş sırları açığa çıkararak katili bulmaya çalışır.", "9780006167921", "placeholder.png", 15.99, 14.99, 13.99, 14.49, "Roger Ackroyd Cinayeti" },
                    { 3, "Anthony Burgess", 3, "Şiddet ve suçla dolu bir gelecekte, genç Alex ve çetesi acımasızca eğlenirken, yakalandığında devletin onu “iyileştirmek” için uyguladığı deneysel bir yönteme maruz kalır. Ancak, özgür iradenin yok edildiği bu süreç, insan doğası ve ahlaki seçimler üzerine derin sorular ortaya çıkarır.", "9780393089134", "placeholder.png", 9.9900000000000002, 8.9900000000000002, 7.9900000000000002, 8.4900000000000002, "Otomatik Portakal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
