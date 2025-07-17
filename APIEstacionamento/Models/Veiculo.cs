using System.ComponentModel.DataAnnotations;

namespace APIEstacionamento.Models
{
    public class Veiculo
    {
        public int Id { get; set; } 
        public string Placa { get; set; }
        public string Modelo { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public class VeiculoCsvModel
        {
            public string Placa { get; set; }
            public string Modelo { get; set; }
            public int ClienteId { get; set; }
        }


    }
}
