namespace EmploeeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSalaryGross : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emploees", "SalaryGross", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emploees", "SalaryGross");
        }
    }
}
