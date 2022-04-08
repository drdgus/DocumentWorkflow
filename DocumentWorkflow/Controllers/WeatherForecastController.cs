using DocumentWorkflow.Core.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbContext = DocumentWorkflow.Core.DAL.DbContext;

namespace DocumentWorkflow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public WeatherForecastController(DbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var users = _dbContext.Users
                .Include(u => u.Roles)
                .ThenInclude(u => u.Role)
                .ToList()
                .Select(u => new
            {
                name = u.Name,
                roles = string.Join(",", u.Roles.Select(r => $"RoleName={r.Role.Name}, IsAdmin:{r.Role.IsAdmin}"))
            });

            return Ok(users);
        }
    }
}