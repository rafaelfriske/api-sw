using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_sw.Models
{
    public class TbStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdStatus { get; set; }

        public required string Descricao { get; set; }

        public virtual ICollection<TbTarefas> Tarefas { get; set; }  // Propriedade de navegação
    }
}
