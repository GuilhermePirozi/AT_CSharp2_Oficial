using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Cidades {
    [Authorize]
    public class CreateModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public CreateModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            ViewData["PaisDestinoId"] = new SelectList(_context.Paises, "Id", "Nome", null);
            return Page();
        }


        [BindProperty]
        public CidadeDestino CidadeDestino { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Cidades.Add(CidadeDestino);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
