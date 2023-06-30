using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Reddit.Controller;

using Model;
using DTO;
using Repository;
using Services;

using Microsoft.AspNetCore.Cors;
using Security.Jwt;


[ApiController]
[Route("forum")]
[EnableCors("MainPolicy")]

public class ForumController : ControllerBase
{

    // [HttpPost("create")]
    // public async Task<IActionResult> Create(
    //     [FromBody] ForumDTO forum,
    //     [FromServices] IRepository<Forum> repo
    // )
    // {
        
    // }
}