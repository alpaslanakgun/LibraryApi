namespace LibraryApi.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string  FullName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int BookCount { get; set; }

    }
    public class AuthorCreateDto
    {
        public string FullName { get; set; } = string.Empty;
        public string  Country { get; set; } = string.Empty;
    }
}
