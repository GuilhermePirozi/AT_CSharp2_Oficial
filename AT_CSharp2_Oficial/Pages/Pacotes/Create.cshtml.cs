using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Pacotes {
    [Authorize]
    public class CreateModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public CreateModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Pacotes.Add(PacoteTuristico);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
