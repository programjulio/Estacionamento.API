using APIEstacionamento.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIEstacionamento.Data
{
    // Contexto do Entity Framework Core para o estacionamento
    // Define as entidades e suas relações
    // Configurações adicionais podem ser feitas aqui, como mapeamento de tabelas, etc.
}
public class EstacionamentoContext : DbContext
{
    public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
}

