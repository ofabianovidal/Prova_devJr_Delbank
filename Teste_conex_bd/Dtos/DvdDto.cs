namespace Teste_conex_bd.Dtos
{
    public class DvdDto
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime DtPublicacao { get; set; }
        public int QuantCopias { get; set; }

        public int RentCopy { get; set; }
        // iai git
        public string? ReturnCopy { get; set; }
    }
}
