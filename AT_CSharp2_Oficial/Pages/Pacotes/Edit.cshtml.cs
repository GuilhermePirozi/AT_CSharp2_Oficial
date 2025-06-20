using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Pacotes {
    [Authorize]
    public class EditModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public EditModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var pacoteturistico = await _context.Pacotes.FirstOrDefaultAsync(m => m.Id == id);
            if (pacoteturistico == null) {
                return NotFound();
            }
            PacoteTuristico = pacoteturistico;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(PacoteTuristico).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!PacoteTuristicoExists(PacoteTuristico.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PacoteTuristicoExists(int id) {
            return _context.Pacotes.Any(e => e.Id == id);
        }
    }
}
