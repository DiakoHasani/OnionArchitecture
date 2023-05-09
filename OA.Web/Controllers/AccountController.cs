using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OA.Service.DTO.User;
using OA.Service.Services;

namespace OA.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    public AccountController(IUserService userService,
    ITokenService tokenService,
    IConfiguration configuration)
    {
        _userService = userService;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserModel model)
    {
        var result = await _userService.AddUser(model);
        if (result.Result)
        {
            return Ok(result.Message);
        }
        else
        {
            return BadRequest(result.Message);
        }
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginBodyModel model)
    {
        var result = await _userService.Login(model);
        if (!result.Result)
        {
            return BadRequest(result.Message);
        }
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, result.UserId) };

        var accessToken = _tokenService.GenerateAccessToken(claims,_configuration["Jwt:Key"],_configuration["Jwt:Issuer"],_configuration["Jwt:Audience"]);

        return Ok(accessToken);
    }
}