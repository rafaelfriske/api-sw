using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_sw.Models;
using api_sw.Models.CustomModels;

namespace api_sw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbTarefasController : ControllerBase
    {
        private readonly MeuContexto _context;

        public TbTarefasController(MeuContexto context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbTarefas>>> GetTbTarefas()
        {
            try
            {                
                var tarefas = await _context.TbTarefas.Where(a => a.TarefaRemovida == 0)
                    .AsNoTracking()
                    .ToListAsync();         

                return tarefas;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar sua requisição" + ex.Message);
            }
        }

        // GET: api/TbTarefas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbTarefas>> GetTbTarefas(int id)
        {
            var tbTarefas = await _context.TbTarefas.FindAsync(id);

            if (tbTarefas == null)
            {
                return NotFound();
            }

            return tbTarefas;
        }

        // PUT: api/TbTarefas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbTarefas(int id, TbTarefas tbTarefas)
        {
            if (id != tbTarefas.IdTarefa)
            {
                return BadRequest();
            }

            _context.Entry(tbTarefas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbTarefasExists(id))
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

        [HttpPut("alterarStatus/{idTarefa}")]
        public async Task<IActionResult> AlterarStatus(int idTarefa, [FromBody] StatusCustom statusCustom)
        {
            var tarefa = await _context.TbTarefas.FindAsync(idTarefa);
            if (tarefa == null)
            {
                return NotFound();
            }

            if (statusCustom.IdStatus == 2)
            {
                tarefa.DataConclusao = DateTime.Now;
                tarefa.DataAlteracao = DateTime.Now;
                tarefa.IdStatus = 2;
            }

            if (statusCustom.IdStatus == 1)
            {
                tarefa.DataAlteracao = DateTime.Now;
                tarefa.DataConclusao = null;
                tarefa.IdStatus = 1;
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, "Erro ao atualizar status: " + ex.Message);
            }
        }

        [HttpPut("removerTarefa/{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] StatusCustom statusCustom)
        {
            var tarefa = await _context.TbTarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            tarefa.TarefaRemovida = 1;
            tarefa.DataAlteracao = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, "Erro ao atualizar status: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TbTarefas>> PostTbTarefas(TarefaCustom tarefaCustom)
        {
            if (tarefaCustom.DataCriacao == null)
            {
                tarefaCustom.DataCriacao = DateTime.Now;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tbTarefas = new TbTarefas
            {
                Descricao = tarefaCustom.Descricao,
                Titulo = tarefaCustom.Titulo,
                DataCriacao = tarefaCustom.DataCriacao,
                IdStatus = tarefaCustom.IdStatus,
                IdUsuario = tarefaCustom.idUsuario,
                TarefaRemovida = 0

            };

            try
            {
                _context.TbTarefas.Add(tbTarefas);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, "Erro ao atualizar status: " + ex.Message);
            }


            return CreatedAtAction("GetTbTarefas", new { id = tbTarefas.IdTarefa }, tbTarefas);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbTarefas(int id)
        {
            var tbTarefas = await _context.TbTarefas.FindAsync(id);
            if (tbTarefas == null)
            {
                return NotFound();
            }

            _context.TbTarefas.Remove(tbTarefas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TbTarefasExists(int id)
        {
            return _context.TbTarefas.Any(e => e.IdTarefa == id);
        }
    }
}