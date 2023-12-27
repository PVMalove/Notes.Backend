using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notes.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    // internal Guid UserID
    // {
    //     get
    //     {
    //         string? value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //         if (value != null)
    //             return !User.Identity!.IsAuthenticated
    //                 ? Guid.Empty
    //                 : Guid.Parse(value);
    //         return default;
    //     }
    // }
    
    internal Guid UserID => !User.Identity.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
}