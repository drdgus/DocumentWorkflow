using DocumentWorkflow.Core.DAL.Entities;
using DocumentWorkflow.Core.DAL.Repositories;
using DocumentWorkflow.Core.Models;
using DocumentWorkflow.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentWorkflow.Controllers.Api.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly DocumentsRepository _documentsRepository;
    private readonly DocumentCreator _documentCreator;

    public DocumentsController(DocumentsRepository documentsRepository, DocumentCreator documentCreator)
    {
        _documentsRepository = documentsRepository;
        _documentCreator = documentCreator;
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

    [HttpGet("documentId={id}")]
    public ActionResult GetDocumentFile(int id)
    {
        var document = _documentsRepository.GetDocumentForPrint(id);

        var file = System.IO.File.ReadAllText(document.FileName);

        var res = new
        {
            html = file
        };
        return Ok(res);
    }

    [HttpPut]
    public ActionResult Put([FromBody] NewDocument document)
    {
        var docId = _documentCreator.Create(document);
        return Ok(docId);
    }
}