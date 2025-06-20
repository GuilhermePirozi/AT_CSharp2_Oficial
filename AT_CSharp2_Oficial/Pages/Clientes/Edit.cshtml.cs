using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Clientes {
    [Authorize]
    public class EditModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public EditModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null) {
                return NotFound();
            }
            Cliente = cliente;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(Cliente).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ClienteExists(Cliente.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClienteExists(int id) {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
