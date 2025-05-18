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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbLogin>>> GetTbLogins()
        {
            return await _context.TbLogin.ToListAsync();
        }

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

        [HttpPost]
        public async Task<ActionResult<TbLogin>> PostTbLogin(TbLogin tbLogin)
        {
            _context.TbLogin.Add(tbLogin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTbLogin), new { id = tbLogin.IdUsuario }, tbLogin);
        }


        [HttpPost("Login")] 
        public async Task<IActionResult> Login([FromBody] LoginCustom loginCustom)
        {           
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
                var usuario = await _context.TbLogin
                    .AsNoTracking() 
                    .FirstOrDefaultAsync(u => u.Login == loginCustom.Login);

                if (usuario == null)
                {
                    return Unauthorized(new
                    {
                        Success = false,
                        Message = "Credenciais inválidas",
                        ErrorCode = "AUTH_ERROR"
                    });
                }

                if (usuario.Senha != loginCustom.Senha)
                {

                    return Unauthorized(new
                    {
                        Success = false,
                        Message = "Credenciais inválidas",
                        ErrorCode = "AUTH_ERROR"
                    });
                }

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
