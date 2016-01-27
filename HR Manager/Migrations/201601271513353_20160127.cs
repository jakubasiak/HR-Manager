namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160127 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SkillTags", "Person_Id", "dbo.People");
            DropIndex("dbo.SkillTags", new[] { "Person_Id" });
            CreateTable(
                "dbo.SkillTagPersons",
                c => new
                    {
                        SkillTag_Id = c.Int(nullable: false),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillTag_Id, t.Person_Id })
                .ForeignKey("dbo.SkillTags", t => t.SkillTag_Id, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.SkillTag_Id)
                .Index(t => t.Person_Id);
            
            DropColumn("dbo.SkillTags", "Person_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SkillTags", "Person_Id", c => c.Int());
            DropForeignKey("dbo.SkillTagPersons", "Person_Id", "dbo.People");
            DropForeignKey("dbo.SkillTagPersons", "SkillTag_Id", "dbo.SkillTags");
            DropIndex("dbo.SkillTagPersons", new[] { "Person_Id" });
            DropIndex("dbo.SkillTagPersons", new[] { "SkillTag_Id" });
            DropTable("dbo.SkillTagPersons");
            CreateIndex("dbo.SkillTags", "Person_Id");
            AddForeignKey("dbo.SkillTags", "Person_Id", "dbo.People", "Id");
        }
    }
}
