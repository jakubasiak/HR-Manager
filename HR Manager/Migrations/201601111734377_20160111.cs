namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecruitmentEvents", "AuthorId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecruitmentEvents", "AuthorId");
        }
    }
}
