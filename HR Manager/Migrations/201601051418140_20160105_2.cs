namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160105_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecruitmentEvents", "Recruitment_Id", "dbo.Recruitments");
            DropIndex("dbo.RecruitmentEvents", new[] { "Recruitment_Id" });
            DropColumn("dbo.RecruitmentEvents", "Recruitment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecruitmentEvents", "Recruitment_Id", c => c.Long());
            CreateIndex("dbo.RecruitmentEvents", "Recruitment_Id");
            AddForeignKey("dbo.RecruitmentEvents", "Recruitment_Id", "dbo.Recruitments", "Id");
        }
    }
}
