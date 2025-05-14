using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS.Common;
using Pdc.Application.Services.UserService;
using Pdc.Domain.Models.Security;

namespace Pdc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IConfiguration configuration, IMapper mapper)
        {
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet("signin-oidc")]
        [ApiExplorerSettings(IgnoreApi = true)] // Hide from Swagger
        public IActionResult SignInCallback()
        {
            // The OIDC middleware will intercept this request
            // This code won't actually run but the route needs to exist
            return Ok();
        }

        [HttpGet("login")]
        public IActionResult Login(string? uri)
        {
            // TODO Changer localhost:3000 pour une url dans le appSettings.
            return Challenge(new AuthenticationProperties { RedirectUri = string.IsNullOrEmpty(uri) ? "https://localhost:3000/" : uri },
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // TODO V2 quand on se connecte, on crée deux cookies. Je crois que le deuxième vient de l'appel au signIn dans la section Azure
            // Bref, sans le signInManager.SignInAsync, le user n'était pas connecté, mais je crois que c'est lui qui crée l'autre token.
            // Pour l'instant, ça crée de la confusion au dev, mais  aussi au pirate alors je crois qu'on va appeler ça un "feature"
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            // TODO redirect sur la page d'acceuil de mon app.
            return Redirect("/");
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            User userInfo = await _userService.GetCurrentUserInfoAsync();
            return Ok(_mapper.Map<UserDTO>(userInfo));
        }
    }
}
