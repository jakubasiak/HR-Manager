namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160126_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SkillTagPersons", "SkillTag_Id", "dbo.SkillTags");
            DropForeignKey("dbo.SkillTagPersons", "Person_Id", "dbo.People");
            DropIndex("dbo.SkillTagPersons", new[] { "SkillTag_Id" });
            DropIndex("dbo.SkillTagPersons", new[] { "Person_Id" });
            AddColumn("dbo.SkillTags", "Person_Id", c => c.Int());
            CreateIndex("dbo.SkillTags", "Person_Id");
            AddForeignKey("dbo.SkillTags", "Person_Id", "dbo.People", "Id");
            DropTable("dbo.SkillTagPersons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SkillTagPersons",
                c => new
                    {
                        SkillTag_Id = c.Int(nullable: false),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillTag_Id, t.Person_Id });
            
            DropForeignKey("dbo.SkillTags", "Person_Id", "dbo.People");
            DropIndex("dbo.SkillTags", new[] { "Person_Id" });
            DropColumn("dbo.SkillTags", "Person_Id");
            CreateIndex("dbo.SkillTagPersons", "Person_Id");
            CreateIndex("dbo.SkillTagPersons", "SkillTag_Id");
            AddForeignKey("dbo.SkillTagPersons", "Person_Id", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SkillTagPersons", "SkillTag_Id", "dbo.SkillTags", "Id", cascadeDelete: true);
        }
    }
}
