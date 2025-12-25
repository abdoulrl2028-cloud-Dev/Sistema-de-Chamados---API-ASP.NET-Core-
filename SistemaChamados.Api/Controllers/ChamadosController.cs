using Microsoft.AspNetCore.Mvc;
using SistemaChamados.Api.Application.DTOs;
using SistemaChamados.Api.Application.Interfaces;

namespace SistemaChamados.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly IChamadoService _chamadoService;
        private readonly ILogger<ChamadosController> _logger;

        public ChamadosController(IChamadoService chamadoService, ILogger<ChamadosController> logger)
        {
            _chamadoService = chamadoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os chamados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ChamadoDTO>>> GetAll()
        {
            try
            {
                var chamados = await _chamadoService.GetAllAsync();
                return Ok(chamados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter chamados");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Obtém um chamado por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChamadoDTO>> GetById(int id)
        {
            try
            {
                var chamado = await _chamadoService.GetByIdAsync(id);
                if (chamado == null)
                    return NotFound();

                return Ok(chamado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter chamado");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Cria um novo chamado
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChamadoDTO>> Create([FromBody] CreateChamadoDTO dto)
        {
            try
            {
                var chamado = await _chamadoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = chamado.Id }, chamado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar chamado");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Atualiza um chamado existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChamadoDTO>> Update(int id, [FromBody] UpdateChamadoDTO dto)
        {
            try
            {
                var chamado = await _chamadoService.UpdateAsync(id, dto);
                return Ok(chamado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar chamado");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deleta um chamado
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _chamadoService.DeleteAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar chamado");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
