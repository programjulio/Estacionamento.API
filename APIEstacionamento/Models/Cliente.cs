namespace APIEstacionamento.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public List<Veiculo> Veiculos { get; set; }
        public bool EhMensalista { get; set; }
    }
}
