using APIEstacionamento.Data;
using APIEstacionamento.Models;
using APIEstacionamento.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;

namespace Estacionamento.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly EstacionamentoContext _context;

        public UploadController(EstacionamentoContext context)
        {
            _context = context;
        }

        [HttpPost("clientes")]
        public async Task<IActionResult> UploadClientes(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido.");

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var clientes = csv.GetRecords<Cliente>().ToList();

            foreach (var cliente in clientes)
            {
                var clienteExistente = await _context.Clientes
                    .FirstOrDefaultAsync(c => c.Telefone == cliente.Telefone);

                if (clienteExistente == null)
                {
                    _context.Clientes.Add(cliente);
                }
           
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = $"{clientes.Count} clientes processados." });
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
               
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == item.ClienteId);

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

            await _context.SaveChangesAsync();
            return Ok("Veículos importados com sucesso.");
        }

    }

    public class VeiculoCsvModel
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int ClienteId { get; set; }
    }
}
