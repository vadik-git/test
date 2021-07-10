using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.Data;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PersonController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPut]
        public IActionResult Update(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            if (!_db.Persons.Any(x => x.Id == person.Id))
            {
                return NotFound();
            }

            _db.Update(person);
            _db.SaveChanges();

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            _db.Persons.Add(person);
            _db.SaveChanges();
            return Ok(person);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var person = _db.Persons.FirstOrDefault(i => i.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            _db.Persons.Remove(person);
            _db.SaveChanges();
            return Ok(person);

        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return  _db.Persons.ToList();
        }
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Person user = _db.Persons.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
    }
}
