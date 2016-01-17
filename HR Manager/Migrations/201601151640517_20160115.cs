namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160115 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecruitmentCandidates", "Recruitment_Id", "dbo.Recruitments");
            DropForeignKey("dbo.RecruitmentCandidates", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.RecruitmentCandidates", new[] { "Recruitment_Id" });
            DropIndex("dbo.RecruitmentCandidates", new[] { "Candidate_Id" });
            AddColumn("dbo.Candidates", "Recruitment_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Candidates", "Recruitment_Id");
            AddForeignKey("dbo.Candidates", "Recruitment_Id", "dbo.Recruitments", "Id", cascadeDelete: true);
            DropTable("dbo.RecruitmentCandidates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecruitmentCandidates",
                c => new
                    {
                        Recruitment_Id = c.Long(nullable: false),
                        Candidate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recruitment_Id, t.Candidate_Id });
            
            DropForeignKey("dbo.Candidates", "Recruitment_Id", "dbo.Recruitments");
            DropIndex("dbo.Candidates", new[] { "Recruitment_Id" });
            DropColumn("dbo.Candidates", "Recruitment_Id");
            CreateIndex("dbo.RecruitmentCandidates", "Candidate_Id");
            CreateIndex("dbo.RecruitmentCandidates", "Recruitment_Id");
            AddForeignKey("dbo.RecruitmentCandidates", "Candidate_Id", "dbo.Candidates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecruitmentCandidates", "Recruitment_Id", "dbo.Recruitments", "Id", cascadeDelete: true);
        }
    }
}
