using CoindeskExampleApiServer.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace CoindeskExampleApiServer.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="options"></param>
    public partial class CoindeskDbContext(DbContextOptions<CoindeskDbContext> options) : DbContext(options)
    {
        /// <summary>
        ///     幣別
        /// </summary>
        public virtual DbSet<Copilot> Copilots { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=CoindeskExample;Integrated Security=True");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Copilot>(entity =>
            {
                entity.HasKey(m => m.Code);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
