namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160106 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recruitments", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.Recruitments", new[] { "Candidate_Id" });
            CreateTable(
                "dbo.RecruitmentCandidates",
                c => new
                    {
                        Recruitment_Id = c.Long(nullable: false),
                        Candidate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recruitment_Id, t.Candidate_Id })
                .ForeignKey("dbo.Recruitments", t => t.Recruitment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id, cascadeDelete: true)
                .Index(t => t.Recruitment_Id)
                .Index(t => t.Candidate_Id);
            
            DropColumn("dbo.Recruitments", "Candidate_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recruitments", "Candidate_Id", c => c.Int());
            DropForeignKey("dbo.RecruitmentCandidates", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.RecruitmentCandidates", "Recruitment_Id", "dbo.Recruitments");
            DropIndex("dbo.RecruitmentCandidates", new[] { "Candidate_Id" });
            DropIndex("dbo.RecruitmentCandidates", new[] { "Recruitment_Id" });
            DropTable("dbo.RecruitmentCandidates");
            CreateIndex("dbo.Recruitments", "Candidate_Id");
            AddForeignKey("dbo.Recruitments", "Candidate_Id", "dbo.Candidates", "Id");
        }
    }
}
