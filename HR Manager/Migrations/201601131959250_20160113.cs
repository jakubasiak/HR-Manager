namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160113 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Candidates", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.Candidates", new[] { "Candidate_Id" });
            DropColumn("dbo.Candidates", "Candidate_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidates", "Candidate_Id", c => c.Int());
            CreateIndex("dbo.Candidates", "Candidate_Id");
            AddForeignKey("dbo.Candidates", "Candidate_Id", "dbo.Candidates", "Id");
        }
    }
}
