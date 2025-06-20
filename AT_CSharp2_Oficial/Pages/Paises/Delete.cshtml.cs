using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Paises {
    [Authorize]
    public class DeleteModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public DeleteModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public PaisDestino PaisDestino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var paisdestino = await _context.Paises.FirstOrDefaultAsync(m => m.Id == id);

            if (paisdestino == null) {
                return NotFound();
            } else {
                PaisDestino = paisdestino;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var paisdestino = await _context.Paises.FindAsync(id);
            if (paisdestino != null) {
                PaisDestino = paisdestino;
                _context.Paises.Remove(PaisDestino);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
