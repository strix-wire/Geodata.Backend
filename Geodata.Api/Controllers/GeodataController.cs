using AutoMapper;
using Geodata.Api.Models;
using Geodata.Application.Geodata.Commands.CreateGeodata;
using Geodata.Application.Geodata.Commands.DeleteGeodata;
using Geodata.Application.Geodata.Commands.UpdateGeodata;
using Geodata.Application.Geodata.Queries.GetGeodata;
using Geodata.Application.Geodata.Queries.GetGeodataList;
using Geodata.Persistence.IdentityEF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Geodata.Api.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class GeodataController : BaseController
{
    private readonly UserManager<MyIdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<GeodataController> _logger;
    private readonly IMapper _mapper;

    public GeodataController(ILogger<GeodataController> logger, IMapper mapper,
        RoleManager<IdentityRole> roleManager, UserManager<MyIdentityUser> userManager)
        => (_logger, _mapper, _userManager, _roleManager) = (logger, mapper, userManager, roleManager);

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Create([FromBody] CreateGeodataDto dto)
    {
        _logger.LogInformation("Create Geodata. Input model: " + dto);

        var command = _mapper.Map<CreateGeodataCommand>(dto);
        await Mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    [Route("DetailsGeodata")]
    public async Task<ActionResult> Get([FromBody] GetGeodataDto dto)
    {
        _logger.LogInformation("Get Geodata. Input model: " + dto);

        var query = _mapper.Map<GeodataDetailsQuery>(dto);

        var vm = await Mediator.Send(dto);

        return Ok(vm);
    }

    [HttpGet]
    [Route("GetGeodataList")]
    public async Task<ActionResult> GetGeodataList(
         [FromBody] GetGeodataList dto)
    {
        _logger.LogInformation("Get list Geodata. Input model: " + dto);

        var queryList = _mapper.Map<GeodataListQuery>(dto);
        var vm = await Mediator.Send(queryList);

        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult> Update([FromBody] UpdateGeodataDto dto)
    {
        _logger.LogInformation("Update Geodata. Input model: " + dto);

        var command = _mapper.Map<UpdateGeodataCommand>(dto);

        await Mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] DeleteGeodataDto dto)
    {
        _logger.LogInformation("Delete Geodata. Input model: " + dto);


        var command = _mapper.Map<DeleteGeodataCommand>(dto);
        await Mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    [Route("Test")]
    public string GetTest()
    {
        return "Service geodata is running";
    }
}
