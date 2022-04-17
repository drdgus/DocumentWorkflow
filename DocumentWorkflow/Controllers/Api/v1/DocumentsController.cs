using DocumentWorkflow.Core.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DocumentWorkflow.Controllers.Api.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly DocumentsRepository _documentsRepository;

    public DocumentsController(DocumentsRepository documentsRepository)
    {
        _documentsRepository = documentsRepository;
    }

    [HttpGet("categoryId={id}")]
    public ActionResult Get(int id)
    {
        var documents = _documentsRepository.GetDocuments(id).Select(i => new
        {
            Id = i.Id,
            Number = i.Number,
            CreatedDate = i.CreatedDate,
            Name = i.Name
        });
        return Ok(documents);
    }
}