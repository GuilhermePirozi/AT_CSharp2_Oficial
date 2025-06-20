using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Simulador {
    [Authorize]
    public class CalcularTotalModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public CalcularTotalModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public decimal ValorDiaria { get; set; }

        [BindProperty]
        public int Dias { get; set; }

        public decimal ValorTotal { get; set; }

        public string Mensagem { get; set; }

        public void PopularForm() {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["PacoteTuristicoId"] = new SelectList(_context.Pacotes, "Id", "Nome");
        }


        public IActionResult OnGet() {
            PopularForm();
            return Page();
        }

        [BindProperty]
        public Reserva Reserva { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                PopularForm();
                return Page();
            }

            Func<decimal, int, decimal> calcularTotal = (valorDiaria, dias) => valorDiaria * dias;
            decimal totalCalculado = calcularTotal(ValorDiaria, Dias);
            ValorTotal = totalCalculado;
            Mensagem = $"Preço total: {ValorTotal}";

            PopularForm();
            return Page();
        }
    }
}
