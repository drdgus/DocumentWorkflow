using DocumentWorkflow.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DocumentWorkflow.Core.DAL.Repositories
{
    public class CategoriesRepository
    {
        private DbContext _dbContext;

        public CategoriesRepository(DbContext db)
        {
            _dbContext = db;
        }

        public ICollection<DocumentCategory> GetCategories()
        {
            return _dbContext.DocumentCategories
                .Include(c => c.DocumentType)
                .ToList();
        }

        public ICollection<DocumentCategory> GetCategories(int typeId)
        {
            return _dbContext.DocumentCategories
                .Include(c => c.DocumentType)
                .Where(c => c.DocumentTypeId == typeId)
                .ToList();
        }
    }

    public class DocumentsRepository
    {
        private DbContext _dbContext;

        public DocumentsRepository(DbContext db)
        {
            _dbContext = db;
        }

        public ICollection<Document> GetDocuments(int categoryId)
        {
            return _dbContext.Documents
                .Where(d => d.DocumentCategoryId == categoryId)
                .ToList();
        }
    }
}
