namespace SHOP_BanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Images", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Images", c => c.String(maxLength: 150));
        }
    }
}
