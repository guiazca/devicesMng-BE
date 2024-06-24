namespace DevicesApi.DTOs
{
    public class DispositivoDto
    {
        public int Id { get; set; }
        public int ModeloId { get; set; }
        public string ModeloNome { get; set; }
        public int MarcaId { get; set; }
        public string MarcaNome { get; set; }
        public int LocalizacaoId { get; set; }
        public string LocalizacaoNome { get; set; }
        public string? Nome { get; set; }
        public string IP { get; set; }
        public int? Porta { get; set; }
        public string URL { get; set; }
        public string? MacAddress { get; set; }
        public string? Descricao { get; set; }
    }
}
