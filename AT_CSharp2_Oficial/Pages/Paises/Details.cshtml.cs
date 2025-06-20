using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Paises {
    [Authorize]
    public class DetailsModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public DetailsModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public PaisDestino PaisDestino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var paisdestino = await _context.Paises.Include(p => p.Cidades).FirstOrDefaultAsync(p => p.Id == id);

            if (paisdestino == null) {
                return NotFound();
            } else {
                PaisDestino = paisdestino;
            }
            return Page();
        }
    }
}
