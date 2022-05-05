using DocumentWorkflow.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DocumentWorkflow.Core.DAL.Repositories
{
    public class CategoriesRepository
    {
        private readonly DbContext _dbContext;

        public CategoriesRepository(DbContext db)
        {
            _dbContext = db;
        }

        public ICollection<DocumentCategory> GetCategories()
        {
            var categories =  _dbContext.DocumentCategories
                .Include(c => c.DocumentType)
                .Include(c => c.LogBook)
                .ToList();

            categories.ForEach(c => { c.CustomTemplateFileName ??= c.DocumentType.TemplateFileName; });
            return categories;
        }

        public ICollection<DocumentCategory> GetCategoriesByType(int typeId)
        {
            var categories = _dbContext.DocumentCategories
                .Include(c => c.DocumentType)
                .Include(c => c.LogBook)
                .Where(c => c.DocumentTypeId == typeId)
                .ToList();

            categories.ForEach(c => { c.CustomTemplateFileName ??= c.DocumentType.TemplateFileName; });
            return categories;
        }

        public void AddCategory(DocumentCategory category)
        {
            _dbContext.DocumentCategories.Add(new DocumentCategory
            {
                ParentId = category.ParentId,
                Name = category.Name,
                CustomTemplateFileName = category.CustomTemplateFileName,
                DocumentTypeId = category.DocumentTypeId,
                LogBookId = category.LogBookId,
            });
        }

        public DocumentCategory GetCategory(int documentCategoryId)
        {
            return _dbContext.DocumentCategories
                .Include(c => c.DocumentType)
                .Include(c => c.LogBook)
                .Single(c => c.Id == documentCategoryId);
        }
    }
}
