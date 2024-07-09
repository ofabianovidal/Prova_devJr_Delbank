using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Teste_conex_bd.Data;
using Teste_conex_bd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste_conex_bd.Dtos;
using System.IO;

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
        public async Task<ActionResult<IEnumerable<Dvd>>> GetDvds()
        {
            return await _context.Dvds.Where(d => d.Cd_situacao == 1).ToListAsync();
            
        }



        // GET: api/Dvds/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<Dvd>> GetDvd(int id)
        {
            var dvd = await _context.Dvds.FindAsync(id);

            if (dvd == null || dvd.Cd_situacao == 0)
            {
                return NotFound();
            }

            return dvd;
        }




        // POST: api/Dvds
        [HttpPost]
        public async Task<ActionResult<Dvd>> PostDvd([FromBody] DvdDto request)
        {
            var dvd = new Dvd
            {

                Titulo = request.Titulo,
                Genero = request.Genero,
                DtPublicacao = request.DtPublicacao,
                QuantCopias = request.QuantCopias,
                DiretorId = request.DiretorId,
                RentCopy = 0
            };

            // Validações básicas
            if (dvd == null)
            {
                return BadRequest("Os dados do DVD são obrigatórios.");
            }

            // Verificar se o diretor existe
            var diretor = await _context.Diretores.FindAsync(dvd.DiretorId);
            if (diretor == null)
            {
                return BadRequest("É obrigatório ter um Diretor válido.");
            }

            

            _context.Dvds.Add(dvd);
            await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetDvd), new { title = dvd.Titulo }, dvd);
            return CreatedAtAction(nameof(GetDvd), new { id = dvd.Id }, dvd);
           


        }



        // PUT: api/Dvds/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDvd(int id, [FromBody] DvdDto request)
        {
            if (!_context.Dvds.Any(d => d.Id == id))
            {
                return NotFound();
            }

            var dvd = await _context.Dvds.FindAsync(id);
            if (dvd == null)
            {
                return NotFound();
            }

            dvd.Titulo = request.Titulo;
            dvd.Genero = request.Genero;
            dvd.DtPublicacao = request.DtPublicacao;
            dvd.QuantCopias = request.QuantCopias;
            dvd.DiretorId = request.DiretorId;

            _context.Entry(dvd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DvdExists(id))
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

        private bool DvdExists(int id)
        {
            throw new NotImplementedException();
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Dvd(int id)
        {
            var dvd = await _context.Dvds.FindAsync(id);
            if (dvd == null)
            {
                return NotFound();
            }

            dvd.Cd_situacao = 0;// Marcar como excluído
            dvd.DeletedAt = DateTime.Now;
            _context.Entry(dvd).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }



        // GET: api/Dvds/Deletados
        [HttpGet("Deletados")]
        public async Task<ActionResult<IEnumerable<Dvd>>> GetDvdsDeletados()
        {
            return await _context.Dvds.Where(d => d.Cd_situacao == 0).ToListAsync();
        }



        
        // PUT: api/Dvds/{id}/AlugarCopias
        [HttpPut("{id}/AlugarCopias")]
        public async Task<IActionResult> AlugarCopias(int id, [FromQuery] int quantidade)
        {
            if (id <= 0)
            {
                return BadRequest("O id é obrigatório.");
            }

            var dvd = await _context.Dvds.FirstOrDefaultAsync(d => d.Id == id);
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
            
            _context.Entry(dvd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _cache.Remove(DvdCacheKey + id); // Certifique-se de que a chave do cache está correta

            return NoContent();
        }


        // PUT: api/Dvds/{id}/DevolverCopias
        [HttpPut("{id}/DevolverCopias")]
        public async Task<IActionResult> DevolverCopias(int id, [FromQuery] int quantidade)
        {
            if (id <= 0)
            {
                return BadRequest("O id é obrigatório.");
            }

            var dvd = await _context.Dvds.FirstOrDefaultAsync(d => d.Id == id);
            if (dvd == null)
            {
                return NotFound();
            }

            if (dvd.ReturnCopy > dvd.RentCopy)
            {
                return BadRequest("Não há cópias suficientes para devolver.");
            }

            dvd.QuantCopias += quantidade;
            dvd.RentCopy -= quantidade;

            _context.Entry(dvd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _cache.Remove(DvdCacheKey + id); // Certifique-se de que a chave do cache está correta

            return NoContent();
        }
    }
}

