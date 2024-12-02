using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimpleCRUD.DTOs;
using SimpleCRUD.Models;

namespace SimpleCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsalSekolahController : ControllerBase
    {
        private readonly SimpleCrudContext _context;

        public AsalSekolahController(SimpleCrudContext context)
        {
            _context = context;
        }

        // GET: api/AsalSekolah
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAsalSekolahs()
        {
            return await _context.AsalSekolahs.Select(f => new
            {
                f.Id,
                f.Name,
            }).ToListAsync();
        }

        // GET: api/AsalSekolah/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAsalSekolah(int id)
        {
            var asalSekolah = await _context.AsalSekolahs.Select(f => new
            {
                f.Id,
                f.Name,
            }).FirstOrDefaultAsync(f => f.Id == id);

            if (asalSekolah == null)
            {
                return NotFound(new { message = "Sekolah tidak ditemukan!", statusCode = 404 });
            }

            return asalSekolah;
        }

        // PUT: api/AsalSekolah/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsalSekolah(int id, AsalSekolahDTO asalSekolah)
        {
            asalSekolah.Id = id;

            var js = JsonConvert.DeserializeObject<AsalSekolah>(JsonConvert.SerializeObject(asalSekolah));

            _context.Entry(js).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsalSekolahExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { js.Id, js.Name });
        }

        // POST: api/AsalSekolah
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AsalSekolahDTO>> PostAsalSekolah(AsalSekolahDTO data)
        {
            AsalSekolah asalSekolah = new AsalSekolah()
            {
                Name = data.Name,
            };

            _context.AsalSekolahs.Add(asalSekolah);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsalSekolah", new { id = asalSekolah.Id }, new { asalSekolah.Id, asalSekolah.Name });
        }

        // DELETE: api/AsalSekolah/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsalSekolah(int id)
        {
            var asalSekolah = await _context.AsalSekolahs.FindAsync(id);
            if (asalSekolah == null)
            {
                return NotFound();
            }

            try
            {
                _context.AsalSekolahs.Remove(asalSekolah);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Gagal menghapus sekolah!", statusCode = 400 });
            }

            return Ok(new { message = "Berhasil menghapus sekolah!", statusCode = 200 });
        }

        private bool AsalSekolahExists(int id)
        {
            return _context.AsalSekolahs.Any(e => e.Id == id);
        }
    }
}
