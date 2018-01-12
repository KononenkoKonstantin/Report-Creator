namespace ReportCreator.Domain.Entities
{
    using System.Data.Entity;

    public partial class RCContext : DbContext
    {
        public RCContext() : base("name=RCContext")
        {
        }

        public virtual DbSet<Expenditure> Expenditures { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expenditure>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<Expenditure>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.Expenditure)                
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.Sum)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Expenditures)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);
        }
    }
}
