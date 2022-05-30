using DocumentWorkflow.Core.DAL.Entities;
using System.Collections.Generic;
using System.Text;

namespace DocumentWorkflow.Core.Services
{
    public class ExcelParser
    {
        public ExcelParser()
        {
            
        }

        public IEnumerable<Student> ParseStudentsFromFile(string path)
        {
            var students = new List<Student>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(file, Encoding.GetEncoding("windows-1251"));

            _ = sr.ReadLine(); //skip headers.

            for (var row = sr.ReadLine(); row != null; row = sr.ReadLine())
            {
                var student = ParseStudent(row);
                if(student != null)
                    students.Add(student);
            }

            return students;
        }

        private static Student? ParseStudent(string row)
        {
            var studentInfo = row.Split(';');
            if (studentInfo.Any(string.IsNullOrWhiteSpace)) return null;

            var newStudent = new Student
            {
                FullName = studentInfo[0],
                BirthDay = DateTime.Parse(studentInfo[1]),
                Gender = SetGender(studentInfo[2]),
                Class = studentInfo[3]
            };

            return newStudent;
        }

        private static Student.Genders SetGender(string gender)
        {
            return gender switch
            {
                "М" => Student.Genders.Male,
                "Ж" => Student.Genders.Female,
                _ => throw new ArgumentException("Некорректный пол ученика.", nameof(gender))
            };
        }
    }
}
