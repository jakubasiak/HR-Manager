namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160117 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CandidateComments", newName: "PersonNotes");
            AddColumn("dbo.PersonNotes", "Person_Id", c => c.Int());
            CreateIndex("dbo.PersonNotes", "Person_Id");
            AddForeignKey("dbo.PersonNotes", "Person_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonNotes", "Person_Id", "dbo.People");
            DropIndex("dbo.PersonNotes", new[] { "Person_Id" });
            DropColumn("dbo.PersonNotes", "Person_Id");
            RenameTable(name: "dbo.PersonNotes", newName: "CandidateComments");
        }
    }
}
