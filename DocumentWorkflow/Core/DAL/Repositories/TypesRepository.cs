using DocumentWorkflow.Core.DAL.Entities;

namespace DocumentWorkflow.Core.DAL.Repositories;

public class TypesRepository
{
    private DbContext _dbContext;

    public TypesRepository(DbContext db)
    {
        _dbContext = db;
    }

    public ICollection<DocumentType> GetTypes()
    {
        return _dbContext.DocumentTypes.ToList();
    }
}