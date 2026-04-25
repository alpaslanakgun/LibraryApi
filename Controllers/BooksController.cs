using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly LibraryDBContext _context;

		public BooksController(LibraryDBContext context)
		{
			_context = context;
		}

		[HttpGet]
		//api/book
		//api/book?category=Roman&year=1972
		public ActionResult<IEnumerable<BookDto>> GetAll([FromQuery] string? category, [FromQuery]int?year)
		{
			var query=_context.Books.Include(b=>b.Author).AsQueryable();

			if(!string.IsNullOrWhiteSpace(category))
				query=query.Where(b=>b.Category == category);

			if(year.HasValue)
				query=query.Where(query=>query.Year == year.Value);

			var books = query.Select(b => new BookDto
			{
				Id = b.Id,
				Title = b.Title,
				Year = b.Year,
				Category = b.Category,
				AuthorName = b.Author!.FullName
			}).ToList();

			return Ok(books);
		}

		[HttpGet("{id}")]
		public ActionResult<BookDto>GetById(int id)
		{
			var book = _context.Books.Include(b => b.Author).FirstOrDefault(x=>x.Id == id);
			if (book == null)
				return NotFound(new { error = $"Book with id {id} not found" });
			var dto = new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				Year = book.Year,
				Category = book.Category,
				AuthorName = book.Author!.FullName
			};
			return Ok(dto);
		}

		[HttpPost]
		public ActionResult<BookCreateDto> Create([FromBody]BookCreateDto bookCreateDto)
		{
			if (string.IsNullOrWhiteSpace(bookCreateDto.Title))
				return BadRequest(new { error = "Title is required" });//400
			if(bookCreateDto.Year<1000 ||bookCreateDto.Year>DateTime.Now.Year)
				return BadRequest(new { error = "Year is not valid" });//400

			var authorExists = _context.Authors.Any(a => a.Id == bookCreateDto.AuthorId);
			if(!authorExists)
			return BadRequest(new { error = $"Author with id{bookCreateDto.AuthorId}not found " });//400

			var book = new Book
			{
				Title = bookCreateDto.Title,
				Year = bookCreateDto.Year,
				Category = bookCreateDto.Category,
				AuthorId = bookCreateDto.AuthorId
			};
			_context.Books.Add(book);
			_context.SaveChanges();

			_context.Entry(book).Reference(b=>b.Author).Load();


			var result = new BookDto
			{
				Title = book.Title,
				Year = book.Year,
				Category = book.Category,
				AuthorName = book.Author!.FullName
			};
			return CreatedAtAction(nameof(GetById), new {id=book.Id},result);//201 created
		}




	}
}
/*
 
 localhost:api/books/getbyid/{id}
 localhost:api/books/create/{id}
 */