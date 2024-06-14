using System.Text.Json.Serialization;
using Teste_conex_bd.Models;

namespace Teste_conex_bd.Models
{
    public class Diretor
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int Cd_situacao { get; set; } = 1;  // 1 para ativo, 0 para excluído

        public List<Dvd> Dvds { get; set; }              //# Lista do Dvd
    } 

}
