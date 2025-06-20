using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Cidades {
    [Authorize]
    public class DetailsModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public DetailsModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public CidadeDestino CidadeDestino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var cidadedestino = await _context.Cidades.FirstOrDefaultAsync(m => m.Id == id);
            if (cidadedestino == null) {
                return NotFound();
            } else {
                CidadeDestino = cidadedestino;
            }
            return Page();
        }
    }
}
