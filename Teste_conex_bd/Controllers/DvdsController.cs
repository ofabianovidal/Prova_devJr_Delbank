using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Teste_conex_bd.Data;
using Teste_conex_bd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_conex_bd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DvdsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private const string DvdCacheKey = "Dvd_";

        public DvdsController(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/Dvds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dvd>>> GetDvds(string title)
        {
            IQueryable<Dvd> dvds = _context.Dvds.Include(d => d.Diretor);

            // Filtro opcional por título
            if (!string.IsNullOrEmpty(title))
            {
                dvds = dvds.Where(d => d.Titulo.Contains(title));
            }

            return await dvds.ToListAsync();
        }

        // GET: api/Dvds/{title}
        [HttpGet("{title}")]
        public async Task<ActionResult<Dvd>> GetDvd(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("O título é obrigatório.");
            }

            if (_cache.TryGetValue(DvdCacheKey + title, out Dvd dvd))
            {
                return dvd;
            }

            dvd = await _context.Dvds.Include(d => d.Diretor).FirstOrDefaultAsync(d => d.Titulo == title);

            if (dvd == null)
            {
                return NotFound();
            }

            _cache.Set(DvdCacheKey + title, dvd);

            return dvd;
        }

        // POST: api/Dvds
        [HttpPost]
        public async Task<ActionResult<Dvd>> PostDvd([FromBody] Dvd dvd, string title, string genero, int quantidadeCopias, int alugarCopias, string devolverCopias)
        {
            // Validações básicas
            if (dvd == null || string.IsNullOrEmpty(title))
            {
                return BadRequest("Os dados do DVD e o título são obrigatórios.");
            }

            // Verificar se o diretor existe
            var diretor = await _context.Diretores.FindAsync(dvd.DiretorId);
            if (diretor == null)
            {
                diretor = new Diretor
                {
                    FirstName = dvd.Diretor.FirstName,
                    Surname = dvd.Diretor.Surname,
                    CreatedAt = DateTime.Now
                };
                _context.Diretores.Add(diretor);
                await _context.SaveChangesAsync();
                dvd.DiretorId = diretor.Id;
            }

            dvd.CreatedAt = DateTime.Now;
            dvd.Titulo = title; // Atribui o título recebido ao DVD
            dvd.Genero = genero; // Atribui o gênero recebido ao DVD
            dvd.QuantCopias = quantidadeCopias; // Atribui a quantidade de cópias recebida ao DVD
            dvd.RentCopy = alugarCopias; // Atribui a quantidade de cópias alugadas recebida ao DVD
            dvd.ReturnCopy = devolverCopias; // Atribui a quantidade de cópias devolvidas recebida ao DVD
            _context.Dvds.Add(dvd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDvd), new { title = dvd.Titulo }, dvd);
        }

        // PUT: api/Dvds/{title}
        [HttpPut("{title}")]
        public async Task<IActionResult> PutDvd(string title, Dvd dvd)
        {
            if (string.IsNullOrEmpty(title) || title != dvd.Titulo)
            {
                return BadRequest("O título é obrigatório e deve corresponder ao título do DVD.");
            }

            // Validações básicas
            if (string.IsNullOrEmpty(dvd.Titulo))
            {
                return BadRequest("O título é obrigatório.");
            }

            _context.Entry(dvd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _cache.Remove(DvdCacheKey + title);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DvdExists(title))
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

        // DELETE: api/Dvds/{title}
        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteDvd(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("O título é obrigatório.");
            }

            var dvd = await _context.Dvds.FirstOrDefaultAsync(d => d.Titulo == title);
            if (dvd == null)
            {
                return NotFound();
            }

            // Marcando como excluído (em vez de deletar do banco de dados)
            dvd.DeletedAt = DateTime.Now;
            dvd.Cd_situacao = 0;
            _context.Entry(dvd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _cache.Remove(DvdCacheKey + title);

            return NoContent();
        }

        // PUT: api/Dvds/{title}/AlugarCopias
        [HttpPut("{title}/AlugarCopias")]
        public async Task<IActionResult> AlugarCopias(string title, int quantidade)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("O título é obrigatório.");
            }

            var dvd = await _context.Dvds.FirstOrDefaultAsync(d => d.Titulo == title);
            if (dvd == null)
            {
                return NotFound();
            }

            if (dvd.QuantCopias < quantidade)
            {
                return BadRequest("Não há cópias suficientes disponíveis para alugar.");
            }

            dvd.QuantCopias -= quantidade;
            dvd.RentCopy += quantidade;
            dvd.ReturnCopy = "Rented";
            _context.Entry(dvd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _cache.Remove(DvdCacheKey + title);

            return NoContent();
        }

        // PUT: api/Dvds/{title}/DevolverCopias
        [HttpPut("{title}/DevolverCopias")]
        public async Task<IActionResult> DevolverCopias(string title, int quantidade)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("O título é obrigatório.");
            }

            var dvd = await _context.Dvds.FirstOrDefaultAsync(d => d.Titulo == title);
            if (dvd == null)
            {
                return NotFound();
            }

            if (dvd.RentCopy < quantidade)
            {
                return BadRequest("A quantidade de cópias a devolver é maior do que as cópias alugadas.");
            }

            dvd.QuantCopias += quantidade;
            dvd.RentCopy -= quantidade;
            dvd.ReturnCopy = "Returned";
            _context.Entry(dvd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _cache.Remove(DvdCacheKey + title);

            return NoContent();
        }

        private bool DvdExists(string title)
        {
            return _context.Dvds.Any(d => d.Titulo == title);
        }
    }
}

