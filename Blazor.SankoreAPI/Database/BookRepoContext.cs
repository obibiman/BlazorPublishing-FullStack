using Blazor.SankoreAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazor.SankoreAPI.Database
{
    public partial class BookRepoContext : IdentityDbContext<ApiUser>
    {
        public BookRepoContext()
        {
        }

        public BookRepoContext(DbContextOptions<BookRepoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=MAURICE-OPTIPLE;Database=BookRepo;Integrated Security=true;MultipleActiveResultSets=true");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.Entity<Author>(entity =>
            {
                _ = entity.Property(e => e.Bio).HasMaxLength(250);

                _ = entity.Property(e => e.FirstName).HasMaxLength(50);

                _ = entity.Property(e => e.LastName).HasMaxLength(50);
            });

            _ = modelBuilder.Entity<Book>(entity =>
            {
                _ = entity.HasIndex(e => e.Isbn, "UQ_Books_ISBN")
                    .IsUnique();

                _ = entity.Property(e => e.Image).HasMaxLength(50);

                _ = entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                _ = entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                _ = entity.Property(e => e.Summary).HasMaxLength(50);

                _ = entity.Property(e => e.Title).HasMaxLength(50);

                _ = entity.Property(e => e.Year)
                    .HasMaxLength(10)
                    .IsFixedLength();

                _ = entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_ToTable");
            });
            _ = modelBuilder.Entity<IdentityRole>().HasData
                (
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "1aa58b46-8208-479f-a26e-5b78035c280f"
                },
                 new IdentityRole
                 {
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR",
                     Id = "4f7af9a4-5221-422c-a10f-0817e81b84a5"
                 }
                );
            PasswordHasher<ApiUser>? hasher = new PasswordHasher<ApiUser>();
            _ = modelBuilder.Entity<ApiUser>().HasData
                (
               new ApiUser
               {
                   Id = "3a4d344e-158e-46b2-baad-4890f2d26fd1",
                   Email = "admin@sankorebookstore.com",
                   NormalizedEmail = "ADMIN@SANKOREBOOKSTORE.COM",
                   UserName = "admin@sankorebookstore.com",
                   NormalizedUserName = "ADMIN@SANKOREBOOKSTORE.COM",
                   FirstName = "System",
                   LastName = "Admin",
                   PasswordHash = hasher.HashPassword(null, "P@ssword1")
               },
                new ApiUser
                {
                    Id = "c487f4ae-ca04-40ea-a4ec-c241179cdac4",
                    Email = "user@sankorebookstore.com",
                    NormalizedEmail = "USER@SANKOREBOOKSTORE.COM",
                    UserName = "user@sankorebookstore.com",
                    NormalizedUserName = "USER@SANKOREBOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                }
               );

            _ = modelBuilder.Entity<IdentityUserRole<string>>().HasData
                (
              new IdentityUserRole<string>
              {
                  RoleId = "1aa58b46-8208-479f-a26e-5b78035c280f",
                  UserId = "3a4d344e-158e-46b2-baad-4890f2d26fd1"
              },
              new IdentityUserRole<string>
              {
                  RoleId = "4f7af9a4-5221-422c-a10f-0817e81b84a5",
                  UserId = "c487f4ae-ca04-40ea-a4ec-c241179cdac4"
              }
              );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
