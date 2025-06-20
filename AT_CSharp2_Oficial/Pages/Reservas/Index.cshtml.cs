using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Reservas {
    [Authorize]
    public class IndexModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public IndexModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public IList<Reserva> Reserva { get; set; } = default!;

        public async Task OnGetAsync() {
            Reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.PacoteTuristico).ToListAsync();
        }
    }
}
