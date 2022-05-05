using DocumentWorkflow.Core.DAL.Entities;

namespace DocumentWorkflow.Core.DAL.Repositories
{
    public class StudentsRepository
    {
        private readonly DbContext _dbContext;

        public StudentsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _dbContext.Students.ToList();
        }
    }
}
