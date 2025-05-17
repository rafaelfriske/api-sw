namespace api_sw.Models.CustomModels
{
    public class TarefaCustom
    {
        public required string Descricao { get; set; }
        public required string Titulo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public int IdStatus { get; set; }
        public int idUsuario { get; set; }
    }
}
