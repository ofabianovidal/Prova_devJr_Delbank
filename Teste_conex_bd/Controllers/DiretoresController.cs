using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste_conex_bd.Data;
using Teste_conex_bd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste_conex_bd.Dtos;

namespace Teste_conex_bd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiretoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiretoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Diretores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diretor>>> GetDiretores()
        {
           // return await _context.Diretores.ToListAsync();
            return await _context.Diretores.Where(d => d.Cd_situacao == 1).ToListAsync();

        }

        // GET: api/Diretores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diretor>> GetDiretor(int id)
        {
            var diretor = await _context.Diretores.FindAsync(id);

            if (diretor == null || diretor.Cd_situacao == 0)
            {
                return NotFound();
            }

            return diretor;
        }

        // POST: api/Diretores
        [HttpPost]
        public async Task<ActionResult<Diretor>> PostDiretor([FromBody] DiretorDto request)
        {
            var diretor = new Diretor
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
            };
            _context.Diretores.Add(diretor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDiretor), new { id = diretor.Id }, diretor);
        }

        // PUT: api/Diretores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiretor(int id, [FromBody] DiretorDto request)
        {
            if (!_context.Diretores.Any(d => d.Id == id))
            {
                return NotFound();
            }

            var diretor = await _context.Diretores.FindAsync(id);
            if (diretor == null)
            {
                return NotFound();
            }

            diretor.FirstName = request.FirstName;
            diretor.Surname = request.Surname;

            _context.Entry(diretor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiretorExists(id))
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

        private bool DiretorExists(int id)
        {
            return _context.Diretores.Any(e => e.Id == id);
        }

        // DELETE: api/Diretores/5


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDiretor(int id)
        //{
        //    var diretor = await _context.Diretores.FindAsync(id);
        //    if (diretor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Diretores.Remove(diretor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiretor(int id)
        {
            var diretor = await _context.Diretores.FindAsync(id);
            if (diretor == null)
            {
                return NotFound();
            }

            diretor.Cd_situacao = 0; // Marcar como excluído
            _context.Entry(diretor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }





        // GET: api/Diretores/Deletados
        [HttpGet("Deletados")]
        public async Task<ActionResult<IEnumerable<Diretor>>> GetDiretoresDeletados()
        {
            return await _context.Diretores.Where(d => d.Cd_situacao == 0).ToListAsync();
        }

    }
}
