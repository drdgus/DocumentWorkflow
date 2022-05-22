using DocumentWorkflow.Core.DAL.Entities;
using System.IO;
using DocumentWorkflow.Core.DAL;
using DocumentWorkflow.Core.DAL.Repositories;

namespace DocumentWorkflow.Core.Services
{
    public class StudentsImporter
    {
        private readonly StudentsRepository _studentsRepository;
        private readonly ExcelParser _excelParser;
        private readonly ILogger<StudentsImporter> _logger;

        public StudentsImporter(StudentsRepository studentsRepository, 
            ExcelParser excelParser,
            ILogger<StudentsImporter> logger)
        {
            _studentsRepository = studentsRepository;
            _excelParser = excelParser;
            _logger = logger;
        }

        public void Import(string path)
        {
            var students = GetUsersFromExcel(path);
            InsertStudentsToDb(students);
        }

        private IEnumerable<Student> GetUsersFromExcel(string path)
        {
            try
            {
                return _excelParser.ParseStudentsFromFile(path);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ошибка импорта из CSV файла.\n{e.Message}");
                return new List<Student>();
            }
        }

        private void InsertStudentsToDb(IEnumerable<Student> students)
        {
            if(!students.Any()) return;
            
            _studentsRepository.AddStudents(students);
        }
    }
}
