using DocumentWorkflow.Core.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DocumentWorkflow.Controllers.Api.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsRepository _studentsRepository;

        public StudentsController(StudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var students = _studentsRepository.GetStudents().Select(x => new
            {
                FullName = x.FullName,
                Birthday = x.BirthDay,
                Class = x.Class,
                Gender = x.GenderToString()

            });

            return Ok(students);
        }
    }
}

