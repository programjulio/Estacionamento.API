using APIEstacionamento.Data;
using APIEstacionamento.Models;
using CsvHelper;
using Estacionamento.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace APIEstacionamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly EstacionamentoContext _context;

        public ClientesController(EstacionamentoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(BuscarClientePorId), new { id = cliente.Id }, cliente);
        }

        [HttpPost("csv")]
        public async Task<IActionResult> InserirClientes(IFormFile file)
        {
            //_context.Clientes.Add(cliente);
            //await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(BuscarClientePorId), new { id = cliente.Id }, cliente);

            return BadRequest();

        }

        [HttpPost("veiculos")]
        public async Task<IActionResult> UploadVeiculosCSV(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido.");

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var registros = csv.GetRecords<VeiculoCsvModel>().ToList();

            foreach (var item in registros)
            {
                // Busca o cliente pelo ID fornecido no CSV
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == item.ClienteId);

                if (cliente != null)
                {
                    var veiculo = new Veiculo
                    {
                        Placa = item.Placa,
                        Modelo = item.Modelo,
                        ClienteId = cliente.Id
                    };

                    _context.Veiculos.Add(veiculo);
                }
            }

            // Salva todos de uma vez no banco
            await _context.SaveChangesAsync();

            return Ok("Importação concluída com sucesso.");
        }


        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Veiculos)
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarClientePorId(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Veiculos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }
    }
}
