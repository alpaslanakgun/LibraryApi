using LibraryApi.DTOs;
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
		[HttpPost("login")]
		public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
				return BadRequest(new { error = "Username and Password required." });

			var user = _users.FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);
			if (user == null)
				return Unauthorized(new { error = "Invalid credentials" });//401

			var (token, expiresAt) = _tokenService.GenerateToken(user);
			return Ok(new LoginResponse
			{
				Token = token,
				ExpiresAt = expiresAt,
				Username = request.Username,
				Role = user.Role
			});

		}

		/*
		 Refresh Token : Access token kısa süreli olunca bir sorun cıkıyor kullanıcı her saat bası tekrar  login mi olacak ? iş tam bu anda devreye refresh token girmektedir. 

		Access TOken > Süresi 15-60 dk arasında  Api isteklerinde kullanılır 
		Refresh TOken =>7-30 gün arasında  ve sadece yeni access token almak istedigimizde kullanılır...



		Access token kısa oldugu icin calınma riski az olur refresh token uzun ama sadece tek bir endpointte calısır onu daha güvenli saklarsınız.
		 
		
		1=> Login 
		      Client => Post => login [username,password]
		      Server=> {accessToken}(60dk),refreshtoken 7 gün

		2 Normal kullanım 60 dakika boyunca sistemdeyiz


		3 60 dakika doldu access token süresi doldu 
		   Server istek attıgın book tarafına 401 dönecek 





		 
		 */



	}
}
