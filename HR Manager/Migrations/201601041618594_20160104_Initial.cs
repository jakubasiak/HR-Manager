namespace HR_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160104_Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinancialExpectations = c.String(),
                        ReadyToMove = c.Boolean(nullable: false),
                        MeetsRequirements = c.Boolean(nullable: false),
                        Invited = c.Boolean(nullable: false),
                        Candidate_Id = c.Int(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Candidate_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        Street = c.String(),
                        Zip = c.String(),
                        City = c.String(),
                        CVPath = c.String(),
                        PersonalDataProcessing = c.Boolean(nullable: false),
                        CanContact = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recruitments",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        JobOffer_OfferNumber = c.Long(),
                        Candidate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobOffers", t => t.JobOffer_OfferNumber)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id)
                .Index(t => t.JobOffer_OfferNumber)
                .Index(t => t.Candidate_Id);
            
            CreateTable(
                "dbo.JobOffers",
                c => new
                    {
                        OfferNumber = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        Location = c.String(),
                        AboutCompany = c.String(),
                        JobDescription = c.String(nullable: false),
                        Expectations = c.String(),
                        Offer = c.String(),
                        Current = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OfferNumber);
            
            CreateTable(
                "dbo.CandidateComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Author = c.String(),
                        AuthorId = c.String(),
                        Comment = c.String(),
                        Candidate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id)
                .Index(t => t.Candidate_Id);
            
            CreateTable(
                "dbo.GIDOLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Author = c.String(),
                        AuthorId = c.String(),
                        Changes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecruitmentEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Recruitment_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recruitments", t => t.Recruitment_Id)
                .Index(t => t.Recruitment_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RecruitmentEvents", "Recruitment_Id", "dbo.Recruitments");
            DropForeignKey("dbo.CandidateComments", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Recruitments", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Recruitments", "JobOffer_OfferNumber", "dbo.JobOffers");
            DropForeignKey("dbo.Candidates", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Candidates", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RecruitmentEvents", new[] { "Recruitment_Id" });
            DropIndex("dbo.CandidateComments", new[] { "Candidate_Id" });
            DropIndex("dbo.Recruitments", new[] { "Candidate_Id" });
            DropIndex("dbo.Recruitments", new[] { "JobOffer_OfferNumber" });
            DropIndex("dbo.Candidates", new[] { "Person_Id" });
            DropIndex("dbo.Candidates", new[] { "Candidate_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RecruitmentEvents");
            DropTable("dbo.GIDOLogs");
            DropTable("dbo.CandidateComments");
            DropTable("dbo.JobOffers");
            DropTable("dbo.Recruitments");
            DropTable("dbo.People");
            DropTable("dbo.Candidates");
        }
    }
}
