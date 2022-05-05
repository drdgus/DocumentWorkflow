using System.Collections;
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
            var categories = _categoriesRepository.GetCategoriesByType(id).Select(category => new
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
                Fields = GetFields(category),
                RequiredModule = category.RequiredModule
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
                Fields = GetFields(category),
                RequiredModule = category.RequiredModule
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
                Order = field.Order,
                RequiredElements = field.RequiredElements.ToList()
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
                Order = 1,
                RequiredElements = new List<TemplateField.Element>().ToList()
            });
            fields.Insert(1, new
            {
                Name = "$Дата_создания$",
                NameForUser = "Дата создания",
                VisibleForUser = true,
                Type = "datetime",
                IsDisabled = true,
                Value = DateTime.Now.ToString(),
                Order = 2,
                RequiredElements = new List<TemplateField.Element>().ToList()
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

    public interface ICategoryFields
    {
        public string Name { get; set; }
        public string NameForUser { get; set; }
        public bool VisibleForUser { get; set; }
        public string Type { get; set; }
        public bool IsDisabled { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public List<TemplateField.Element> RequiredElements { get; set; }
    }
}
