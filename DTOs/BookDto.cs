using LibraryApi.Models;

namespace LibraryApi.DTOs
{
    //Client'a cevap dönerken kullanırız.
    public class BookDto
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string AuthorName { get; set; }

    }
    //Client'a yeni kitap olusturulurken gönderir.
    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Category { get; set; } = string.Empty;
        public int AuthorId { get; set; }

    }
    public class BookUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Category { get; set; } = string.Empty;
        public int AuthorId { get; set; }

    }
}

