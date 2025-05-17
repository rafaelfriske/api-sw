using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_sw.Models
{
    // TbTarefas.cs
    public class TbTarefas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarefa { get; set; }

        public required string Descricao { get; set; }
        public required string Titulo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }

        [ForeignKey("Status")]
        public int IdStatus { get; set; }
        public virtual TbStatus Status { get; set; }  // Propriedade de navegação

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual TbLogin Usuario { get; set; }  // Propriedade de navegação
    }
}
