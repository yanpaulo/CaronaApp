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
using CaronaApp.Server.Models.Entities;

namespace CaronaApp.Controllers
{
    public class CaronasController : ApiController
    {
        private CaronaContext db = new CaronaContext();

        // GET: api/Caronas
        public IQueryable<Carona> GetCaronas()
        {
            return db.Caronas;
        }

        // GET: api/Caronas/5
        [ResponseType(typeof(Carona))]
        public IHttpActionResult GetCarona(int id)
        {
            Carona carona = db.Caronas.Find(id);
            if (carona == null)
            {
                return NotFound();
            }

            return Ok(carona);
        }

        // PUT: api/Caronas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarona(int id, Carona carona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carona.Id)
            {
                return BadRequest();
            }

            db.Entry(carona).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaronaExists(id))
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

        // POST: api/Caronas
        [ResponseType(typeof(Carona))]
        public IHttpActionResult PostCarona(Carona carona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Caronas.Add(carona);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carona.Id }, carona);
        }

        // DELETE: api/Caronas/5
        [ResponseType(typeof(Carona))]
        public IHttpActionResult DeleteCarona(int id)
        {
            Carona carona = db.Caronas.Find(id);
            if (carona == null)
            {
                return NotFound();
            }

            db.Caronas.Remove(carona);
            db.SaveChanges();

            return Ok(carona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaronaExists(int id)
        {
            return db.Caronas.Count(e => e.Id == id) > 0;
        }
    }
}