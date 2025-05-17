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

        // GET: api/TbTarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbTarefas>>> GetTbTarefas()
        {
            try
            {
                // Carrega os dados sem tracking para melhor performance
                var tarefas = await _context.TbTarefas
                    .AsNoTracking()
                    .ToListAsync();

                // Verifica e corrige datas inválidas
                foreach (var tarefa in tarefas)
                {
                    // Exemplo para um campo chamado DataConclusao
                    if (tarefa.DataConclusao == DateTime.MinValue)
                    {
                        tarefa.DataConclusao = null; // Ou algum valor padrão
                    }

                    // Repita para outros campos DateTime
                }

                return tarefas;
            }
            catch (Exception ex)
            {
                // Log do erro (implemente seu sistema de logging)
               // _logger.LogError(ex, "Erro ao buscar tarefas");

                return StatusCode(500, "Ocorreu um erro ao processar sua requisição");
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

            // Atualiza apenas o status
            tarefa.IdStatus = statusCustom.IdStatus;

            // Se precisar atualizar a data de conclusão quando concluído
            if (statusCustom.IdStatus == 2) // Assumindo que 2 = Concluído
            {
                tarefa.DataConclusao = DateTime.Now;
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

        // POST: api/TbTarefas
        [HttpPost]
        public async Task<ActionResult<TbTarefas>> PostTbTarefas(TarefaCustom tarefaCustom)
        {
            // Definir valores padrão se necessário
            if (tarefaCustom.DataCriacao == null)
            {
                tarefaCustom.DataCriacao = DateTime.Now;
            }

            // Validar dados recebidos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapear TarefaCustom para TbTarefas
            var tbTarefas = new TbTarefas
            {
                Descricao = tarefaCustom.Descricao,
                Titulo = tarefaCustom.Titulo,
                DataCriacao = tarefaCustom.DataCriacao,
                IdStatus = tarefaCustom.IdStatus,
                IdUsuario = tarefaCustom.idUsuario,
                
            };

            _context.TbTarefas.Add(tbTarefas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbTarefas", new { id = tbTarefas.IdTarefa }, tbTarefas);
        }

        // DELETE: api/TbTarefas/5
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