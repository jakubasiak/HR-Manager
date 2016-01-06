namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160105_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecruitmentEvents", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecruitmentEvents", "Author", c => c.String(nullable: false));
            AddColumn("dbo.RecruitmentEvents", "Event", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecruitmentEvents", "Event");
            DropColumn("dbo.RecruitmentEvents", "Author");
            DropColumn("dbo.RecruitmentEvents", "Time");
        }
    }
}
