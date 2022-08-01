namespace Api.Data.Context
{
    public class ApiContext : DbContext
    {
#pragma warning disable CS8618
        public ApiContext(DbContextOptions<ApiContext> opt) : base(opt)
#pragma warning restore CS8618
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Serial> Serials { get; set; }
        public DbSet<Credentials> UserCredentials { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
