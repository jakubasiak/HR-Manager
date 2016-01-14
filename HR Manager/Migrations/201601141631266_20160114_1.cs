namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160114_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecruitmentEvents", "Recruitment_Id", "dbo.Recruitments");
            DropIndex("dbo.RecruitmentEvents", new[] { "Recruitment_Id" });
            AlterColumn("dbo.RecruitmentEvents", "Recruitment_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.RecruitmentEvents", "Recruitment_Id");
            AddForeignKey("dbo.RecruitmentEvents", "Recruitment_Id", "dbo.Recruitments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecruitmentEvents", "Recruitment_Id", "dbo.Recruitments");
            DropIndex("dbo.RecruitmentEvents", new[] { "Recruitment_Id" });
            AlterColumn("dbo.RecruitmentEvents", "Recruitment_Id", c => c.Long());
            CreateIndex("dbo.RecruitmentEvents", "Recruitment_Id");
            AddForeignKey("dbo.RecruitmentEvents", "Recruitment_Id", "dbo.Recruitments", "Id");
        }
    }
}
