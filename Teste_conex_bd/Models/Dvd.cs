using Teste_conex_bd.Models;

namespace Teste_conex_bd.Models
{
    public class Dvd
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime DtPublicacao { get; set; }
        public int QuantCopias { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int RentCopy { get; set; } = 0;
        public string? ReturnCopy { get; set; }
        public int DiretorId { get; set; } 
        public Diretor Diretor { get; set; }
        //public bool IsRented { get; set; } // Para gerenciar aluguéis

        public int Cd_situacao { get; set; } = 1; // 1 para ativo, 0 para excluído
    }

}
