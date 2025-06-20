using AT_CSharp2_Oficial.Data;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AT_CSharp2_Oficial.Pages.Clientes {
    [Authorize]
    public class DeleteModel : PageModel {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) return NotFound();

            Cliente = await _context.Clientes.FindAsync(id);

            if (Cliente == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) return NotFound();

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null) return NotFound();

            cliente.IsDeleted = true;
            _context.Update(cliente);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
