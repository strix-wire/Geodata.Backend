using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Geodata.Api.Controllers;

public class BaseController : Controller
{
    private IMediator _mediator;
    //Будет использоваться для формирования команд, выполнения запросов
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
