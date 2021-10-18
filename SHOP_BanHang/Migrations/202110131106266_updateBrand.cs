namespace SHOP_BanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateBrand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductCode", c => c.String());
            AlterColumn("dbo.Brands", "Images", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Brands", "Images", c => c.String(maxLength: 150));
            DropColumn("dbo.Products", "ProductCode");
        }
    }
}
