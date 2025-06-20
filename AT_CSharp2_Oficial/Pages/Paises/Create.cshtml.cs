using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AT_CSharp2_Oficial.Data;
using AT_CSharp2_Oficial.Models;

namespace AT_CSharp2_Oficial.Pages.Paises {
    public class CreateModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public CreateModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public PaisDestino PaisDestino { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Paises.Add(PaisDestino);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
