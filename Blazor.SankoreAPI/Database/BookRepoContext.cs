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
                    Id = "cfe1e0ab-8511-4424-abdb-8ff82cf0c9f0"
                },
                 new IdentityRole
                 {
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR",
                     Id = "a1590a1f-25bb-4846-ab4b-d3f7c0cbdef2"
                 }
                );
            PasswordHasher<ApiUser>? hasher = new PasswordHasher<ApiUser>();
            _ = modelBuilder.Entity<ApiUser>().HasData
                (
               new ApiUser
               {
                   Id = "df66f44f-f920-4592-9aed-ceab29956b39",
                   Email = "admin@bookstore.com",
                   NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                   UserName = "admin@bookstore.com",
                   NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                   FirstName = "System",
                   LastName = "Admin",
                   PasswordHash = hasher.HashPassword(null, "P@ssword1")
               },
                new ApiUser
                {
                    Id = "207384ff-8b8e-4dba-a68d-565cb63d40e5",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                }
               );

            _ = modelBuilder.Entity<IdentityUserRole<string>>().HasData
                (
              new IdentityUserRole<string>
              {
                  RoleId = "cfe1e0ab-8511-4424-abdb-8ff82cf0c9f0",
                  UserId = "207384ff-8b8e-4dba-a68d-565cb63d40e5"
              },
              new IdentityUserRole<string>
              {
                  RoleId = "a1590a1f-25bb-4846-ab4b-d3f7c0cbdef2",
                  UserId = "df66f44f-f920-4592-9aed-ceab29956b39"
              }
              );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
