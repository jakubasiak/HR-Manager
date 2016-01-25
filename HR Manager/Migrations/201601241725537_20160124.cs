namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160124 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(maxLength: 250),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillTags", "Person_Id", "dbo.People");
            DropIndex("dbo.SkillTags", new[] { "Person_Id" });
            DropTable("dbo.SkillTags");
        }
    }
}
