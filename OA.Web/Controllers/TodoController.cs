using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.Service.DTO.Todo;
using OA.Service.Services;

namespace OA.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly ITokenService _tokenService;

    private readonly IConfiguration _configuration;
    public TodoController(ITodoService todoService,
    ITokenService tokenService,
    IConfiguration configuration)
    {
        _todoService = todoService;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("GetTodos")]
    public async Task<IActionResult> GetTodos(int page = 0)
    {
        var claims = _tokenService.GetPrincipalFromExpiredToken(await HttpContext.GetTokenAsync("access_token"), _configuration["Jwt:Key"]).Claims;
        var userId = claims.FirstOrDefault(a => a.Type == ClaimTypes.Name).Value;
        return Ok(_todoService.GetTodos(userId, page));
    }

    [HttpDelete]
    [Route("RemoveTodo")]
    public async Task<IActionResult> RemoveTodo(int id)
    {
        var result = await _todoService.RemoveTodo(id);
        if (result.Result)
        {
            return Ok(result.Message);
        }
        else
        {
            if (result.Code == 404)
                return NotFound(result.Message);
            return BadRequest(result.Message);
        }
    }

    [HttpPost]
    [Route("PostTodo")]
    public async Task<IActionResult> PostTodo([FromBody] PostTodoModel model)
    {
        var claims = _tokenService.GetPrincipalFromExpiredToken(await HttpContext.GetTokenAsync("access_token"), _configuration["Jwt:Key"]).Claims;
        var userId = claims.FirstOrDefault(a => a.Type == ClaimTypes.Name).Value;
        var result = await _todoService.AddTodo(model,userId);
        if (result.Result)
        {
            return Ok(result.Message);
        }
        else
        {
            return BadRequest(result.Message);
        }
    }
}