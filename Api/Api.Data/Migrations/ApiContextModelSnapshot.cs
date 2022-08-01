﻿// <auto-generated />

#nullable disable

namespace Api.Data.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ActorEpisode", b =>
                {
                    b.Property<int>("ActorEpisodesEpisodeId")
                        .HasColumnType("int");

                    b.Property<int>("EpisodeActorsActorId")
                        .HasColumnType("int");

                    b.HasKey("ActorEpisodesEpisodeId", "EpisodeActorsActorId");

                    b.HasIndex("EpisodeActorsActorId");

                    b.ToTable("ActorEpisode");
                });

            modelBuilder.Entity("Api.Data.Model.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActorId"), 1L, 1);

                    b.Property<string>("ActorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Api.Data.Model.Authentication.Credentials", b =>
                {
                    b.Property<int>("CredentialsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CredentialsID"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CredentialsID");

                    b.ToTable("UserCredentials");
                });

            modelBuilder.Entity("Api.Data.Model.Authentication.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("CredentialsID")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("CredentialsID")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Api.Data.Model.Episode", b =>
                {
                    b.Property<int>("EpisodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EpisodeId"), 1L, 1);

                    b.Property<string>("EpisodeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EpisodeNumber")
                        .HasColumnType("int");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("int");

                    b.HasKey("EpisodeId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("Api.Data.Model.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Api.Data.Model.Season", b =>
                {
                    b.Property<int>("SeasonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeasonId"), 1L, 1);

                    b.Property<int?>("SeasonNumber")
                        .HasColumnType("int");

                    b.Property<int?>("SerialId")
                        .HasColumnType("int");

                    b.HasKey("SeasonId");

                    b.HasIndex("SerialId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("Api.Data.Model.Serial", b =>
                {
                    b.Property<int>("SerialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SerialId"), 1L, 1);

                    b.Property<string>("SerialName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SerialId");

                    b.ToTable("Serials");
                });

            modelBuilder.Entity("GenreSerial", b =>
                {
                    b.Property<int>("GenreSerialsSerialId")
                        .HasColumnType("int");

                    b.Property<int>("SerialGenresGenreId")
                        .HasColumnType("int");

                    b.HasKey("GenreSerialsSerialId", "SerialGenresGenreId");

                    b.HasIndex("SerialGenresGenreId");

                    b.ToTable("GenreSerial");
                });

            modelBuilder.Entity("ActorEpisode", b =>
                {
                    b.HasOne("Api.Data.Model.Episode", null)
                        .WithMany()
                        .HasForeignKey("ActorEpisodesEpisodeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Api.Data.Model.Actor", null)
                        .WithMany()
                        .HasForeignKey("EpisodeActorsActorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Data.Model.Authentication.User", b =>
                {
                    b.HasOne("Api.Data.Model.Authentication.Credentials", "Credentials")
                        .WithOne("User")
                        .HasForeignKey("Api.Data.Model.Authentication.User", "CredentialsID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Credentials");
                });

            modelBuilder.Entity("Api.Data.Model.Episode", b =>
                {
                    b.HasOne("Api.Data.Model.Season", "EpisodeSeason")
                        .WithMany("SeasonEpisodes")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("EpisodeSeason");
                });

            modelBuilder.Entity("Api.Data.Model.Season", b =>
                {
                    b.HasOne("Api.Data.Model.Serial", "SeasonSerial")
                        .WithMany("SerialSeasons")
                        .HasForeignKey("SerialId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SeasonSerial");
                });

            modelBuilder.Entity("GenreSerial", b =>
                {
                    b.HasOne("Api.Data.Model.Serial", null)
                        .WithMany()
                        .HasForeignKey("GenreSerialsSerialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Api.Data.Model.Genre", null)
                        .WithMany()
                        .HasForeignKey("SerialGenresGenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Data.Model.Authentication.Credentials", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Data.Model.Season", b =>
                {
                    b.Navigation("SeasonEpisodes");
                });

            modelBuilder.Entity("Api.Data.Model.Serial", b =>
                {
                    b.Navigation("SerialSeasons");
                });
#pragma warning restore 612, 618
        }
    }
}
