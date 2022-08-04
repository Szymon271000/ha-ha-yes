using Api.Data.Seeder;

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
        public DbSet<Serie> Series { get; set; }
        public DbSet<Credentials> UserCredentials { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Actor>()
                .HasMany(left => left.ActorEpisodes)
                .WithMany(right => right.EpisodeActors)
                .UsingEntity<ActorEpisode>(
                    right => right.HasOne(e => e.Episode).WithMany(),
                    left => left.HasOne(e => e.Actor).WithMany().HasForeignKey(e => e.ActorId),
                    join => join.ToTable("ActorEpisode"));

            modelBuilder.Entity<Genre>()
                .HasMany(left => left.GenreSerie)
                .WithMany(right => right.SerieGenres)
                .UsingEntity<GenreSerie>(
                    right => right.HasOne(e => e.Serie).WithMany(),
                    left => left.HasOne(e => e.Genre).WithMany().HasForeignKey(e => e.GenreId),
                    join => join.ToTable("GenreSerie")); 

            modelBuilder.SeedDB();
        }
    }
}
