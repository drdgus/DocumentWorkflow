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
        private readonly TemplateParser _templateParser;

        public CategoriesController(CategoriesRepository categoriesRepository, TemplateParser templateParser)
        {
            _categoriesRepository = categoriesRepository;
            _templateParser = templateParser;
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
                Fields = GetFields(i)
            });

            return Ok(categories);
        }

        public ActionResult Get()
        {
            var categories = _categoriesRepository.GetCategories().Select(category => new
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                DocumentTypeId = category.DocumentTypeId,
                DocumentType = new
                {
                    Id = category.DocumentType.Id,
                    Name = category.DocumentType.Name
                },
                LogBookId = category.LogBookId,
                LogBook = category.LogBook,
                Fields = GetFields(category)
            });

            return Ok(categories);
        }

        private object GetFields(DocumentCategory category)
        {

            var fields = _templateParser.GetFields(category.CustomTemplateFileName).Select(field => new
            {
                Name = field.Name,
                NameForUser = field.NameForUser,
                VisibleForUser = field.VisibleForUser,
                Type = field.Type.ToString(),
                IsDisabled = field.IsDisabled,
                Value = field.Value,
                Order = field.Order
            }).ToList();


            //TODO: надо в 1 месте формировать все поля.
            fields.Insert(0, new
            {
                Name = "$Номер_документа$",
                NameForUser = "Номер документа",
                VisibleForUser = true,
                Type = "number",
                IsDisabled = true,
                Value = (category.LogBook.LastDocumentNumber + 1).ToString(),
                Order = 1
            });
            fields.Insert(1, new
            {
                Name = "$Дата_создания$",
                NameForUser = "Дата создания",
                VisibleForUser = true,
                Type = "datetime",
                IsDisabled = true,
                Value = DateTime.Now.ToString(),
                Order = 2
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
