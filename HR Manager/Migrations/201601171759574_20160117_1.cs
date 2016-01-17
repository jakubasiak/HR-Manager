namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160117_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "ApplyTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "ApplyTime");
        }
    }
}
