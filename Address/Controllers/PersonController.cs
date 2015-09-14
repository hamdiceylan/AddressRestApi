using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Address.Models;
using Address.App_Code;

namespace Address.Controllers
{
    public class PersonController : ApiController
    {
        private AddressEntities db = new AddressEntities();

        // GET: api/Person
        public IEnumerable<Persons> GetPerson()
        {
            using (AddressEntities db = new AddressEntities())
            {
                List<Persons> listOfPersons = new List<Persons>();
                Persons personModel = new Persons();
                foreach (var person in db.Person)
                {
                    personModel.Id = person.Id;
                    personModel.Firstname = person.Firstname;
                    personModel.Lastname = person.Lastname;
                    personModel.Birthdate = person.Birthdate;
                    personModel.Gender = person.Gender;
                    personModel.Email= person.Email;
                    listOfPersons.Add(personModel);
                }
                IEnumerable<Persons> persons = listOfPersons;

                return persons;
            }
        }

        // GET: api/Person/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/Person/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Person
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Person.Add(person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
        }

        // DELETE: api/Person/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(int id)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.Person.Remove(person);
            db.SaveChanges();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.Person.Count(e => e.Id == id) > 0;
        }
    }
}