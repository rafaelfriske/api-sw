using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_sw.Models
{
    public class TbLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public required string Login { get; set; }
        public required string Senha { get; set; }
        public required string Nome { get; set; }

        public virtual ICollection<TbTarefas> Tarefas { get; set; }  // Propriedade de navegação
    }
}
