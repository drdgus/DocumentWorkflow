using DocumentWorkflow.Core.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DocumentWorkflow.Controllers.Api.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class TypesController : ControllerBase
{
    private readonly TypesRepository _typesRepository;

    public TypesController(TypesRepository typesRepository)
    {
        _typesRepository = typesRepository;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var types = _typesRepository.GetTypes().Select(i => new
        {
            Id = i.Id,
            Name = i.Name
        });
        return Ok(types);
    }
}