using DocumentWorkflow.Core.DAL.Entities;

namespace DocumentWorkflow.Core.DAL.Repositories;

public class DocumentsRepository
{
    private readonly DbContext _dbContext;

    public DocumentsRepository(DbContext db)
    {
        _dbContext = db;
    }

    public ICollection<Document> GetDocuments(int categoryId)
    {
        return _dbContext.Documents
            .Where(d => d.DocumentCategoryId == categoryId)
            .OrderByDescending(o => o.CreatedDate)
            .ToList();
    }

    public void AddDocument(int categoryId, string filename, string content, string name)
    {
        var category =  _dbContext.DocumentCategories.Single(c => c.Id == categoryId);
        category.LogBook.LastDocumentNumber++;

        _dbContext.Documents.Add(new Document
        {
            Number = category.LogBook.LastDocumentNumber,
            CreatedDate = DateTime.Now,
            Name = name,
            Content = content,
            FileName = filename,
            UserId = 1,
            DocumentCategoryId = categoryId,
        });

        _dbContext.SaveChanges();

        var doc = _dbContext.Documents.OrderByDescending(o => o.Id).First();

        _dbContext.DocumentsHistory.Add(new History
        {
            DocumentId = doc.Id,
            ChangeDate = doc.CreatedDate,
            EditedField = "Создание",
            OldValue = String.Empty,
            NewValue = String.Empty
        });

        _dbContext.SaveChanges();

    }
}