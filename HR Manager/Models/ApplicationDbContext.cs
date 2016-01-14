using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HR_Manager.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<RecruitmentEvent> RecruitmentEvents { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateComment> Commments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<GIDOLog> GiDOlog { get; set; }
        public DbSet<HR_Manager.Utils.ErrorLog> Errors { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RecruitmentEvent>()
                .HasRequired(x => x.Recruitment)
                .WithMany(x => x.Events)
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Recruitment>().HasOptional<RecruitmentEvent>().WithOptionalDependent().WillCascadeOnDelete(true);
                
        }
    }
}