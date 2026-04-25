using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data
{
    public class LibraryDBContext:DbContext
    {
        public LibraryDBContext(DbContextOptions<LibraryDBContext>options):base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*seed data*/
            modelBuilder.Entity<Author>().HasData(
                 new Author { Id=1,FullName="Halis Gökçe",Country="TR"},
                 new Author { Id=2,FullName="Gürbüz Sucuoglu",Country="FR"},
                 new Author { Id=3,FullName="Kadir Dostayevski",Country="RS"},
                 new Author { Id=4,FullName="Deniz Orwell",Country="BG"},
                 new Author { Id=5,FullName="Efe OK",Country="TR"}

                );
            modelBuilder.Entity<Book>().HasData(
                 new Book {Id=1,Title="Halis ve arikalar Diyari",Year=1965,Category="Roman",AuthorId=1},
                 new Book {Id=2,Title="Suç ve Ceza ",Year=1975,Category="Roman",AuthorId=3},
                 new Book {Id=3,Title="Gürbüz ve mutlu olmanın sırrı",Year=1985,Category="Roman",AuthorId=2},
                 new Book {Id=4,Title="Deniz ve 1984 hikayeleri  ",Year=1965,Category="Hikaye",AuthorId=1}

                );
        }



    }
}
