using System.ComponentModel.DataAnnotations;

namespace AT_CSharp2_Oficial.Models {
    public class Cliente {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome precisa conter no mínimo de 3 letras.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Email é obrigatório.")]
        public string Email { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<Reserva> Reservas { get; set; } = new();
    }
}
