namespace FallaMovil.API.Controllers
{
    using FallaMovil.Domain;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    public class ActAssistancesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActAssistances
        public IQueryable<ActAssistance> GetActAssistances()
        {
            return db.ActAssistances;
        }

        // GET: api/ActAssistances/5
        [ResponseType(typeof(ActAssistance))]
        public async Task<IHttpActionResult> GetActAssistance(int id)
        {
            var  actAssistance = await db.ActAssistances.FindAsync(id);

            if (actAssistance == null)
            {
                return NotFound();
            }

            return Ok(actAssistance);
        }

        // PUT: api/ActAssistances/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActAssistance(int id, ActAssistance actAssistance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actAssistance.IdAsistencia)
            {
                return BadRequest();
            }

            db.Entry(actAssistance).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActAssistanceExists(id))
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

        // POST: api/ActAssistances
        [ResponseType(typeof(ActAssistance))]
        public async Task<IHttpActionResult> PostActAssistance(ActAssistance actAssistance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActAssistances.Add(actAssistance);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = actAssistance.IdAsistencia }, actAssistance);
        }

        // DELETE: api/ActAssistances/5
        [ResponseType(typeof(ActAssistance))]
        public async Task<IHttpActionResult> DeleteActAssistance(int id)
        {
            var actAssistance = await db.ActAssistances.FindAsync(id);

            if (actAssistance == null)
            {
                return NotFound();
            }

            db.ActAssistances.Remove(actAssistance);
            await db.SaveChangesAsync();

            return Ok(actAssistance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActAssistanceExists(int id)
        {
            return db.ActAssistances.Count(e => e.IdAsistencia == id) > 0;
        }
    }
}