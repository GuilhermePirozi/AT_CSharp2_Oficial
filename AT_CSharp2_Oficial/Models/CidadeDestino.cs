using System.ComponentModel.DataAnnotations;

namespace AT_CSharp2_Oficial.Models {
    public class CidadeDestino {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "O nome precisa conter no mínimo de 3 letras.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? PaisDestinoId { get; set; }
        public PaisDestino? PaisDestino { get; set; }
    }
}
