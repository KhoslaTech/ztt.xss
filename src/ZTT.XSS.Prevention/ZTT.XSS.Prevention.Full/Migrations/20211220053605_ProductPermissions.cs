using Microsoft.EntityFrameworkCore.Migrations;

namespace ZTT.XSS.Prevention.Full.Migrations
{
    public partial class ProductPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        var permissions = new[]
	        {
		        "('IndexProduct', 'Product', 'List Products', 0)", // general permission
		        "('CreateProduct', 'Product', 'Add a new Product', 0)", // general permission
		        "('EditProduct', 'Product', 'Modify Product', 1)", // instance permission
		        "('DeleteProduct', 'Product', 'Delete Product', 1)", // instance permission
		        "('ClassifiedUser', 'User', 'Manager ClassifiedUser permissions', 0)" // general permission
	        };

	        var impliedPermissions = new[]
	        {
		        "('CreateProduct', 'IndexProduct')",
		        "('EditProduct', 'CreateProduct')",
		        "('DeleteProduct', 'EditProduct')",
		        "('ClassifiedUser', 'DeleteProduct')"
	        };

	        var permissionsSql = new[]
	        {
		        @"insert into [dbo].[Permission](PermissionCode, EntityTypeCode, Description, Kind)values" + string.Join(",\r\n", permissions),
		        @"insert into [dbo].[ImpliedPermission]
				(PermissionCode, ImpliedPermissionCode)values" + string.Join(",\r\n", impliedPermissions)
	        };

	        foreach (var script in permissionsSql)
	        {
		        migrationBuilder.Sql(script);
	        }
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
