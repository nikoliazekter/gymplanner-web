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
using GymPlannerWeb;

namespace GymPlannerWeb.Controllers
{
    public class ExercisesController : ApiController
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        // GET: api/Exercises
        public IQueryable<Exercises> GetExercises()
        {
            return db.Exercises;
        }

        // GET: api/Exercises/5
        [ResponseType(typeof(Exercises))]
        public IHttpActionResult GetExercises(string id)
        {
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return NotFound();
            }

            return Ok(exercises);
        }

        // PUT: api/Exercises/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExercises(string id, Exercises exercises)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exercises.Name)
            {
                return BadRequest();
            }

            db.Entry(exercises).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExercisesExists(id))
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

        // POST: api/Exercises
        [ResponseType(typeof(Exercises))]
        public IHttpActionResult PostExercises(Exercises exercises)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exercises.Add(exercises);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ExercisesExists(exercises.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = exercises.Name }, exercises);
        }

        // DELETE: api/Exercises/5
        [ResponseType(typeof(Exercises))]
        public IHttpActionResult DeleteExercises(string id)
        {
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return NotFound();
            }

            db.Exercises.Remove(exercises);
            db.SaveChanges();

            return Ok(exercises);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExercisesExists(string id)
        {
            return db.Exercises.Count(e => e.Name == id) > 0;
        }
    }
}