using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZTT.XSS.Prevention.BasicDetection.Data.Migrations
{
    public partial class ProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Cost = table.Column<double>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            InsertData(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }

        private void InsertData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
insert [dbo].[Product]
(Id, Name, Description, Cost)
values('c7aeecbe-d4e9-4a7e-849c-aa9fdf27755d', 'New Tesla Car for $20k', 'Brand new CAR x-showroom price.', 20000)
");
        }
    }
}
