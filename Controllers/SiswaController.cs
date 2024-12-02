using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Versioning;
using SimpleCRUD.DTOs;
using SimpleCRUD.Models;

namespace SimpleCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiswaController : ControllerBase
    {
        private readonly SimpleCrudContext _context;

        public SiswaController(SimpleCrudContext context)
        {
            _context = context;
        }

        // GET: api/Siswa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetSiswas()
        {
            return await _context.Siswas.Include(f => f.AsalSekolah).Select(f => new
            {
                f.Id,
                f.Name,
                f.Sex
            }).ToListAsync();
        }

        // GET: api/Siswa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetSiswa(int id)
        {
            var siswa = await _context.Siswas.Include(f => f.AsalSekolah).Where(f => f.Id == id).Select(f => new
            {
                f.Id,
                f.Name,
                f.Sex,
                f.AsalSekolahId,
                asalSekolah = new
                {
                    f.AsalSekolah.Id,
                    f.AsalSekolah.Name
                }
            }).FirstOrDefaultAsync();

            if (siswa == null)
            {
                return NotFound(new { message = "Siswa tidak ditemukan!", statusCode = 404 });
            }

            return siswa;
        }

        // PUT: api/Siswa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiswa(int id, SiswaDTO siswa)
        {
            siswa.Id = id;

            if (!await _context.AsalSekolahs.AnyAsync(f => f.Id == siswa.AsalSekolahId)) return NotFound();

            var js = JsonConvert.DeserializeObject<Siswa>(JsonConvert.SerializeObject(siswa));

            _context.Entry(js).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiswaExists(id))
                {
                    return NotFound(new { message = "Siswa tidak ditemukan!", statusCode = 404 });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new
            {
                js.Id,
                js.Name,
                js.Sex,
                js.AsalSekolahId,
            });
        }

        // POST: api/Siswa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SiswaDTO>> PostSiswa(SiswaDTO siswa)
        {
            Siswa newSiswa = new Siswa()
            {
                Name = siswa.Name,
                Sex = siswa.Sex,
                AsalSekolahId = siswa.AsalSekolahId,
            };

            if (!await _context.AsalSekolahs.AnyAsync(f => f.Id == newSiswa.AsalSekolahId)) return NotFound();

            _context.Siswas.Add(newSiswa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSiswa", new { id = newSiswa.Id }, _context.Siswas.Include(f => f.AsalSekolah).Select(f => new
            {
                f.Id,
                f.Name,
                f.Sex,
                f.AsalSekolahId,
                asalSekolah = new
                {
                    f.AsalSekolah.Id,
                    f.AsalSekolah.Name
                }
            }).FirstOrDefault(f => f.Id == newSiswa.Id));
        }

        // DELETE: api/Siswa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiswa(int id)
        {
            var siswa = await _context.Siswas.FindAsync(id);
            if (siswa == null)
            {
                return NotFound(new { message = "Siswa tidak ditemukan!", statusCode = 404 });
            }

            _context.Siswas.Remove(siswa);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Berhasil meghapus siswa!", statusCode = 200 });
        }

        private bool SiswaExists(int id)
        {
            return _context.Siswas.Any(e => e.Id == id);
        }
    }
}
