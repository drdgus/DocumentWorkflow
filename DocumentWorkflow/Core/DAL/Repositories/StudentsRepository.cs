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

        public void AddStudents(IEnumerable<Student> students)
        {
            var oldStudents = _dbContext.Students.ToList();
            _dbContext.Students.RemoveRange(oldStudents);

            _dbContext.Students.AddRange(students);
            _dbContext.SaveChanges();
        }
    }
}
