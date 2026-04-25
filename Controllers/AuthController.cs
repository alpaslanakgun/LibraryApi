using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		private readonly ITokenService _tokenService;

		public AuthController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}


		private static readonly List<User> _users = new()
		{
			new User{Id=1,Username="efeok",Password="1234",Role="admin" },
			new User{Id=1,Username="halisgokce",Password="1234",Role="user" },
			new User{Id=1,Username="doga",Password="1234",Role="user" },
			new User{Id=1,Username="stajyerdeniz",Password="1234",Role="admin" }
		};


	}
}
