using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_sw.Models;

namespace api_sw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbStatusController : ControllerBase
    {
        private readonly MeuContexto _context;

        public TbStatusController(MeuContexto context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbStatus>>> GetTbStatus()
        {
            return await _context.TbStatus.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TbStatus>> GetTbStatus(int id)
        {
            var tbStatus = await _context.TbStatus.FindAsync(id);

            if (tbStatus == null)
            {
                return NotFound();
            }

            return tbStatus;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbStatus(int id, TbStatus tbStatus)
        {
            if (id != tbStatus.IdStatus)
            {
                return BadRequest();
            }

            _context.Entry(tbStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TbStatus>> PostTbStatus(TbStatus tbStatus)
        {
            _context.TbStatus.Add(tbStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbStatus", new { id = tbStatus.IdStatus }, tbStatus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbStatus(int id)
        {
            var tbStatus = await _context.TbStatus.FindAsync(id);
            if (tbStatus == null)
            {
                return NotFound();
            }

            _context.TbStatus.Remove(tbStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TbStatusExists(int id)
        {
            return _context.TbStatus.Any(e => e.IdStatus == id);
        }
    }
}