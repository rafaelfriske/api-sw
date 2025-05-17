using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api_sw.Models;
using api_sw.Models.CustomModels;

namespace api_sw.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TbLoginsController : ControllerBase
    {
        private readonly MeuContexto _context;

        public TbLoginsController(MeuContexto context)
        {
            _context = context;
        }

        // GET: api/TbLogins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbLogin>>> GetTbLogins()
        {
            return await _context.TbLogin.ToListAsync();
        }

        // GET: api/TbLogins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbLogin>> GetTbLogin(int id)
        {
            var tbLogin = await _context.TbLogin.FindAsync(id);

            if (tbLogin == null)
            {
                return NotFound();
            }

            return tbLogin;
        }

        // POST: api/TbLogins
        [HttpPost]
        public async Task<ActionResult<TbLogin>> PostTbLogin(TbLogin tbLogin)
        {
            _context.TbLogin.Add(tbLogin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTbLogin), new { id = tbLogin.IdUsuario }, tbLogin);
        }

        // POST: TbLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Login")] // Mudança para POST (mais adequado para login)
        public async Task<IActionResult> Login([FromBody] LoginCustom loginCustom)
        {
            // Validação do modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Dados de entrada inválidos",
                    Errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList(),
                    ErrorCode = "VALIDATION_ERROR"
                });
            }

            try
            {
                // 1. Verificar se o usuário existe (sem a senha primeiro para evitar timing attacks)
                var usuario = await _context.TbLogin
                    .AsNoTracking() // Melhoria de performance
                    .FirstOrDefaultAsync(u => u.Login == loginCustom.Login);

                if (usuario == null)
                {
                    // Log de tentativa de login com usuário inexistente (para segurança)
                    //_logger.LogWarning($"Tentativa de login com usuário inexistente: {loginCustom.Login}");

                    // Mesma mensagem para usuário não encontrado e senha inválida (segurança)
                    return Unauthorized(new
                    {
                        Success = false,
                        Message = "Credenciais inválidas",
                        ErrorCode = "AUTH_ERROR"
                    });
                }

                // 2. Verificar a senha (usando hash na prática)
                // ATENÇÃO: Isso é um exemplo - você DEVE usar hash de senha na realidade
                if (usuario.Senha != loginCustom.Senha)
                {
                    // Log de tentativa com senha inválida
                   // _logger.LogWarning($"Tentativa de login com senha inválida para o usuário: {loginCustom.Login}");

                    return Unauthorized(new
                    {
                        Success = false,
                        Message = "Credenciais inválidas",
                        ErrorCode = "AUTH_ERROR"
                    });
                }

                // 3. Gerar token JWT (recomendado para autenticação stateless)
                //var token = GenerateJwtToken(usuario);

                // 4. Registrar login bem-sucedido
               // _logger.LogInformation($"Login bem-sucedido para o usuário: {usuario.Login}");

                // 5. Retornar resposta com token
                return Ok(new
                {
                    Success = true,
                    Message = "Autenticação realizada com sucesso",
                    UserId = usuario.IdUsuario,
                    Login = usuario.Login,

                });
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Erro durante o processo de login");
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Ocorreu um erro durante o login",
                    ErrorCode = "SERVER_ERROR"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbLogin(int id)
        {
            var tbLogin = await _context.TbLogin.FindAsync(id);
            if (tbLogin == null)
            {
                return NotFound();
            }

            _context.TbLogin.Remove(tbLogin);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
