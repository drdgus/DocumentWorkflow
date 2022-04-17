using DocumentWorkflow.Core.DAL.Repositories;
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
                DocumentType = new
                {
                    Id = i.DocumentType.Id,
                    Name = i.DocumentType.Name
                }
            });
            return Ok(categories);
        }
    }
}
