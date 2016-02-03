namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160203 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Candidates", "Person_Id", "dbo.People");
            DropIndex("dbo.Candidates", new[] { "Person_Id" });
            AlterColumn("dbo.Candidates", "Person_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Candidates", "Person_Id");
            AddForeignKey("dbo.Candidates", "Person_Id", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candidates", "Person_Id", "dbo.People");
            DropIndex("dbo.Candidates", new[] { "Person_Id" });
            AlterColumn("dbo.Candidates", "Person_Id", c => c.Int());
            CreateIndex("dbo.Candidates", "Person_Id");
            AddForeignKey("dbo.Candidates", "Person_Id", "dbo.People", "Id");
        }
    }
}
