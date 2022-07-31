using System;
using System.Linq;
using System.Text.Json;
using HRMS.API.Models;
using HRMS.Dal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HrmsDbContext _dbContext;

        public UsersController(HrmsDbContext  dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(b => b.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(user), "application/json");
        }

        //.TemporalAll()

        /// <summary>
        /// Get All Changes for Users - uses .TemporalAll()
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/all")]
        public ActionResult GetUserByIdAllChanges(int id)
        {
            var user = _dbContext.Users
                .TemporalAll()
                .Where(b => b.UserId == id)
                .Select(b=> new 
                {
                    User = b,
                    From = EF.Property<DateTime>(b, "PeriodStart"),
                    Till= EF.Property<DateTime>(b, "PeriodEnd"),
                })
                .OrderBy(x=>x.From)
                .ToList();
            if (!user .Any())
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(user), "application/json");
        }

        //TemporalAsOf
        /// <summary>
        /// Get All Changes for Users - uses .TemporalAsOf()
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        [HttpGet("{id}/on/{datetime}")]
        public ActionResult GetUserByIdAllChanges(int id,string datetime)
        {
            if (!DateTime.TryParse(datetime, out var pointInTime))
                return BadRequest();
            var user = _dbContext.Users
                .TemporalAsOf(pointInTime)
                .Where(b => b.UserId == id)
                .Select(b => new
                {
                    User = b,
                    From = EF.Property<DateTime>(b, "PeriodStart"),
                    Till = EF.Property<DateTime>(b, "PeriodEnd"),
                })
                .OrderBy(x => x.From)
                .ToList();
            //SINGLE RECORD ONLY
            if (!user.Any())
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(user), "application/json");
        }

        //TemporalBetween
        /// <summary>
        /// Get All Changes for Users BETWEEN uses .TemporalBetween()
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datetimeFrom"></param>
        /// <param name="datetimeTo"></param>
        /// <returns></returns>
        [HttpGet("{id}/from/{datetimeFrom}/till/{datetimeTo}")]
        public ActionResult GetUserByIdAllChanges(int id, string datetimeFrom, string datetimeTo)
        {
            if (!DateTime.TryParse(datetimeFrom, out var pointInTimeFrom) || !DateTime.TryParse(datetimeTo, out var pointInTimeTill))
                return BadRequest();

            var user = _dbContext.Users
                .TemporalBetween(pointInTimeFrom, pointInTimeTill)
                .Where(b => b.UserId == id)
                .Select(b => new
                {
                    User = b,
                    From = EF.Property<DateTime>(b, "PeriodStart"),
                    Till = EF.Property<DateTime>(b, "PeriodEnd"),
                })
                .OrderBy(x => x.From)
                .ToList();
            if (!user.Any())
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(user), "application/json");
        }

        //TemporalContainedIn
        /// <summary>
        /// Get All Changes for Users BETWEEN uses .TemporalContainedIn()
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datetimeFrom"></param>
        /// <param name="datetimeTo"></param>
        /// <returns></returns>
        [HttpGet("{id}/all/from/{datetimeFrom}/till/{datetimeTo}")]
        public ActionResult GetAllValidUsers(int id, string datetimeFrom, string datetimeTo)
        {
            if (!DateTime.TryParse(datetimeFrom, out var pointInTimeFrom) || !DateTime.TryParse(datetimeTo, out var pointInTimeTill))
                return BadRequest();

            var user = _dbContext.Users
                .TemporalContainedIn(pointInTimeFrom, pointInTimeTill)
                .Where(b => b.UserId == id)
                .Select(b => new
                {
                    User = b,
                    From = EF.Property<DateTime>(b, "PeriodStart"),
                    Till = EF.Property<DateTime>(b, "PeriodEnd"),
                })
                .OrderBy(x => x.From)
                .ToList();
            if (!user.Any())
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(user), "application/json");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody]UserUpdateModel updateModel)
        {
            var user = _dbContext.Users.FirstOrDefault(b => b.UserId == id);
            var hasChanges = false;
            if (user == null)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(updateModel.FirstName))
            {
                user.FirstName = updateModel.FirstName;
                hasChanges = true;
            }
            if (!string.IsNullOrEmpty(updateModel.LastName))
            {
                user.LastName = updateModel.LastName;
                hasChanges = true;
            }
            if (!string.IsNullOrEmpty(updateModel.OfficeName))
            {
                user.OfficeName = updateModel.OfficeName;
                hasChanges = true;
            }

            if (hasChanges)
            {
                user.ModifiedOn = DateTimeOffset.Now;
                user.ModifiedBy = "app_user";
            }

            var updateResult = _dbContext.SaveChanges() > 0;
            return Content(updateResult ? "true" : "no-change");
        }
    }
}
