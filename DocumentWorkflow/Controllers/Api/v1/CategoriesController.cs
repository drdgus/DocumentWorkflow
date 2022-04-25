using DocumentWorkflow.Core.DAL.Entities;
using DocumentWorkflow.Core.DAL.Repositories;
using DocumentWorkflow.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentWorkflow.Controllers.Api.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository _categoriesRepository;

        public CategoriesController(CategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var categories = _categoriesRepository.GetCategories(id).Select(i => new
            {
                Id = i.Id,
                Name = i.Name,
                ParentId = i.ParentId,
                DocumentTypeId = i.DocumentTypeId,
                DocumentType = new
                {
                    Id = i.DocumentType.Id,
                    Name = i.DocumentType.Name
                },
                LogBookId = i.LogBookId,
                LogBook = i.LogBook,
                Fields = getFields(i)
            });

            return Ok(categories);
        }

        public ActionResult Get()
        {
            var categories = _categoriesRepository.GetCategories().Select(i => new
            {
                Id = i.Id,
                Name = i.Name,
                ParentId = i.ParentId,
                DocumentTypeId = i.DocumentTypeId,
                DocumentType = new
                {
                    Id = i.DocumentType.Id,
                    Name = i.DocumentType.Name
                },
                LogBookId = i.LogBookId,
                LogBook = i.LogBook,
                Fields = getFields(i)
            });

            return Ok(categories);
        }
        private object getFields(DocumentCategory category)
        {

            var fields = new TemplateParser().GetFields(category.CustomTemplateFileName).Select(f => new
            {
                Name = f.Name,
                NameForUser = f.NameForUser,
                Type = f.Type.ToString(),
                IsDisabled = f.IsDisabled,
                Value = f.Value
            }).ToList();

            fields.Insert(0, new
            {
                Name = "$Номер_документа$",
                NameForUser = "Номер документа",
                Type = "number",
                IsDisabled = true,
                Value = (category.LogBook.LastDocumentNumber + 1).ToString()
            });
            fields.Insert(1, new
            {
                Name = "$Дата_создания$",
                NameForUser = "Дата создания",
                Type = "datetime",
                IsDisabled = true,
                Value = DateTime.Now.ToString()
            });

            return fields;
        }

        [HttpPut]
        public ActionResult Put(DocumentCategory category)
        {

            _categoriesRepository.AddCategory(category);

            return Ok();
        }
    }
}
