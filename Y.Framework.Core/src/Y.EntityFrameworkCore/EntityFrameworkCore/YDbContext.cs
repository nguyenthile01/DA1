using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Y.Authorization.Roles;
using Y.Authorization.Users;
using Y.Core;
using Y.MultiTenancy;

namespace Y.EntityFrameworkCore
{
    public class YDbContext : AbpZeroDbContext<Tenant, Role, User, YDbContext>
    {
        /* Define a DbSet for each entity of the application */

        #region MyRegion

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

        #endregion
        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<JobCategory> JobCategories { get; set; }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobSeeker> JobSeekers { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Knowledge> Knowledges { get; set; }
        public virtual DbSet<DesiredLocationJob> DesiredLocationJobs { get; set; }
        public virtual DbSet<DesiredCareer> DesiredCareers { get; set; }
        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }


        #region Location
        public virtual DbSet<Location> Locations { get; set; }

        #endregion
     

        #region Picture

        public DbSet<Picture> Pictures { get; set; }


        #endregion

        #region Contact

        public virtual DbSet<Contact> Contacts { get; set; }

        #endregion

        #region NewLetterSubcription
        public virtual DbSet<NewsLetterSubcription> NewsLetterSubcriptions { get; set; }

        #endregion

        #region Post
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }

        #endregion

        #region Topic
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<TopicCategory> TopicCategories { get; set; }

        #endregion

        #region NewLetterSubcription
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

        #endregion

      
        //#region Discount
        //public virtual DbSet<Discount> Discounts { get; set; }
        //#endregion


        #region Currency

        public virtual DbSet<Currency> Currencies { get; set; }

        #endregion


        public YDbContext(DbContextOptions<YDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, YCustomSqlServerMigrationsSqlGenerator>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.Name)
                .IsRequired(false)
                .HasMaxLength(EntityLength.Name);
            modelBuilder.Entity<User>()
                .Property(b => b.Surname)
                .IsRequired(false)
                .HasMaxLength(EntityLength.LastName);
            modelBuilder.Entity<Currency>()
                .Property(p => p.Rate)
                .HasColumnType("decimal(18,10)")
                .IsRequired(true);

            modelBuilder.Entity<PromotionCode>()
                .HasIndex(p => new { p.DiscountCode })
                .IsUnique();

            base.OnModelCreating(modelBuilder);

        }
    }
}
