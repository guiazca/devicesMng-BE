using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevicesApi.Data;
using DevicesApi.DTOs;
using DevicesApi.Models;

namespace DevicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DispositivosController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DispositivoDto>>> GetDispositivos()
        {
            var dispositivos = await _context.Dispositivos
                .Include(d => d.Modelo)
                .ThenInclude(m => m.Marca)
                .Include(d => d.Localizacao)
                .Select(d => new DispositivoDto
                {
                    Id = d.Id,
                    ModeloId = d.ModeloId,
                    ModeloNome = d.Modelo.Nome,
                    MarcaId = d.Modelo.MarcaId,
                    MarcaNome = d.Modelo.Marca.Nome,
                    LocalizacaoId = d.LocalizacaoId,
                    LocalizacaoNome = d.Localizacao.Nome,
                    Nome = d.Nome,
                    IP = d.IP,
                    Porta = d.Porta,
                    URL = d.URL,
                    MacAddress = d.MacAddress,
                    Descricao = d.Descricao
                })
                .ToListAsync();

            return Ok(dispositivos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DispositivoDto>> GetDispositivo(int id)
        {
            var dispositivo = await _context.Dispositivos
                .Include(d => d.Modelo)
                .ThenInclude(m => m.Marca)
                .Include(d => d.Localizacao)
                .Select(d => new DispositivoDto
                {
                    Id = d.Id,
                    ModeloId = d.ModeloId,
                    ModeloNome = d.Modelo.Nome,
                    MarcaId = d.Modelo.MarcaId,
                    MarcaNome = d.Modelo.Marca.Nome,
                    LocalizacaoId = d.LocalizacaoId,
                    LocalizacaoNome = d.Localizacao.Nome,
                    Nome = d.Nome,
                    IP = d.IP,
                    Porta = d.Porta,
                    URL = d.URL,
                    MacAddress = d.MacAddress,
                    Descricao = d.Descricao
                })
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dispositivo == null)
            {
                return NotFound();
            }

            return Ok(dispositivo);
        }
        [HttpPost]
        public async Task<ActionResult<DispositivoDto>> PostDispositivo(DispositivoDto dispositivoDto)
        {
            var dispositivo = new Dispositivo
            {
                ModeloId = dispositivoDto.ModeloId,
                LocalizacaoId = dispositivoDto.LocalizacaoId,
                Nome = dispositivoDto.Nome,
                IP = dispositivoDto.IP,
                Porta = dispositivoDto.Porta,
                URL = dispositivoDto.URL,
                MacAddress = dispositivoDto.MacAddress,
                Descricao = dispositivoDto.Descricao
            };

            _context.Dispositivos.Add(dispositivo);
            await _context.SaveChangesAsync();

            dispositivoDto.Id = dispositivo.Id;

            return CreatedAtAction(nameof(GetDispositivo), new { id = dispositivoDto.Id }, dispositivoDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispositivo(int id, DispositivoDto dispositivoDto)
        {
            if (id != dispositivoDto.Id)
            {
                return BadRequest();
            }

            var dispositivo = await _context.Dispositivos.FindAsync(id);

            if (dispositivo == null)
            {
                return NotFound();
            }

            dispositivo.ModeloId = dispositivoDto.ModeloId;
            dispositivo.LocalizacaoId = dispositivoDto.LocalizacaoId;
            dispositivo.Nome = dispositivoDto.Nome;
            dispositivo.IP = dispositivoDto.IP;
            dispositivo.Porta = dispositivoDto.Porta;
            dispositivo.URL = dispositivoDto.URL;
            dispositivo.MacAddress = dispositivoDto.MacAddress;
            dispositivo.Descricao = dispositivoDto.Descricao;

            _context.Entry(dispositivo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispositivo(int id)
        {
            var dispositivo = await _context.Dispositivos.FindAsync(id);

            if (dispositivo == null)
            {
                return NotFound();
            }

            _context.Dispositivos.Remove(dispositivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
