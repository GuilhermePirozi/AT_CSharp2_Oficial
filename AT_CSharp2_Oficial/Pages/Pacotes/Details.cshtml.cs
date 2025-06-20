using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Data;
using AT_CSharp2_Oficial.Models;

namespace AT_CSharp2_Oficial.Pages.Pacotes {
    public class DetailsModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public DetailsModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public PacoteTuristico PacoteTuristico { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var pacoteturistico = await _context.Pacotes.FirstOrDefaultAsync(m => m.Id == id);
            if (pacoteturistico == null) {
                return NotFound();
            } else {
                PacoteTuristico = pacoteturistico;
            }
            return Page();
        }
    }
}
