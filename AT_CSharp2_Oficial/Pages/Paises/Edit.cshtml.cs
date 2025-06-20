using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Paises {
    [Authorize]
    public class EditModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public EditModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
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
            }
            PaisDestino = paisdestino;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(PaisDestino).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!PaisDestinoExists(PaisDestino.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PaisDestinoExists(int id) {
            return _context.Paises.Any(e => e.Id == id);
        }
    }
}
