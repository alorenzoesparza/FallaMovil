namespace FallaMovil.API.Controllers
{
    using FallaMovil.API.Helpers;
    using FallaMovil.API.Models;
    using FallaMovil.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    public class ActsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Acts
        public async Task<IHttpActionResult> GetActs()
        {
            var userlogued = User.Identity.Name;
            var role = UsersHelper.GetRoles(userlogued);

            var acts = await db.Acts
                .Where(a => a.FechaActo >= DateTime.Now)
                .ToListAsync();

            var component = await db.Components
                .Where(c => c.Email == userlogued)
                .FirstOrDefaultAsync();

            var dependientes = await db.Components
                .Where(d => d.IdFallero == component.IdFallero || d.IdDependent == component.IdFallero)
                .OrderBy(d => d.IdDependent)
                .ToListAsync();

            var actsResponse = new List<ActResponse>();
            var dependentRespone = new List<DependentResponse>();

            foreach (var act in acts)
            {
                var actAsistancesRespone = new List<ActAssistanceResponse>();

                foreach (var asistencia in act.ActAssistances)
                {
                    var componentAsistente = await db.Components
                        .FindAsync(asistencia.IdFallero);

                    if (asistencia.IdFallero == component.IdFallero
                        || componentAsistente.IdDependent == component.IdFallero
                        )
                    {
                        actAsistancesRespone.Add(new ActAssistanceResponse
                        {
                            IdAct = asistencia.IdAct,
                            IdAsistencia = asistencia.IdAsistencia,
                            IdFallero = asistencia.IdFallero,
                            Nombre = componentAsistente.Nombre,
                            Apuntado = true,
                            Infantil = componentAsistente.Infantil,
                        });
                    }
                }

                foreach (var dependiente in dependientes)
                {
                    var dependienteApuntado = await db.ActAssistances
                        .Where(aa => aa.IdAct == act.IdAct && aa.IdFallero == dependiente.IdFallero)
                        .FirstOrDefaultAsync();

                    var componentAsistente = await db.Components
                        .FindAsync(dependiente.IdFallero);

                    if (dependienteApuntado == null)
                    {
                        actAsistancesRespone.Add(new ActAssistanceResponse
                        {
                            IdAct = act.IdAct,
                            IdFallero = dependiente.IdFallero,
                            Nombre = dependiente.Nombre,
                            Apuntado = false,
                            Infantil = dependiente.Infantil,
                            IdAsistencia = 0,
                        });
                    }
                }


                if (act.ActoOficial)
                {
                    if (role != "Simpatizante")
                    {
                        actsResponse.Add(new ActResponse
                        {
                            ActAssistances = actAsistancesRespone,
                            ActoOficial = act.ActoOficial,
                            Descripcion = act.Descripcion,
                            FechaActo = act.FechaActo,
                            HoraActo = act.HoraActo,
                            IdAct = act.IdAct,
                            Imagen = act.Imagen,
                            Imagen500 = act.Imagen500,
                            Precio = act.Precio,
                            PrecioInfantiles = act.PrecioInfantiles,
                            Titular = act.Titular,
                        });
                    }

                }
                else
                {
                    actsResponse.Add(new ActResponse
                    {
                        ActAssistances = actAsistancesRespone,
                        ActoOficial = act.ActoOficial,
                        Descripcion = act.Descripcion,
                        FechaActo = act.FechaActo,
                        HoraActo = act.HoraActo,
                        IdAct = act.IdAct,
                        Imagen = act.Imagen,
                        Imagen500 = act.Imagen500,
                        Precio = act.Precio,
                        PrecioInfantiles = act.PrecioInfantiles,
                        Titular = act.Titular,
                    });
                }
            }

            return Ok(actsResponse);
        }

        // GET: api/Acts/5
        [ResponseType(typeof(Act))]
        public async Task<IHttpActionResult> GetAct(int id)
        {
            Act act = await db.Acts.FindAsync(id);
            if (act == null)
            {
                return NotFound();
            }

            return Ok(act);
        }

        // PUT: api/Acts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAct(int id, Act act)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != act.IdAct)
            {
                return BadRequest();
            }

            db.Entry(act).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActExists(id))
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

        // POST: api/Acts
        [ResponseType(typeof(Act))]
        public async Task<IHttpActionResult> PostAct(Act act)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Acts.Add(act);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = act.IdAct }, act);
        }

        // DELETE: api/Acts/5
        [ResponseType(typeof(Act))]
        public async Task<IHttpActionResult> DeleteAct(int id)
        {
            Act act = await db.Acts.FindAsync(id);
            if (act == null)
            {
                return NotFound();
            }

            db.Acts.Remove(act);
            await db.SaveChangesAsync();

            return Ok(act);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActExists(int id)
        {
            return db.Acts.Count(e => e.IdAct == id) > 0;
        }
    }
}