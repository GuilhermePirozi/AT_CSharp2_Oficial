using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Data;
using AT_CSharp2_Oficial.Models;

namespace AT_CSharp2_Oficial.Pages.Cidades {
    public class IndexModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public IndexModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public IList<CidadeDestino> CidadeDestino { get; set; } = default!;

        public async Task OnGetAsync() {
            CidadeDestino = await _context.Cidades
                .Include(c => c.PaisDestino).ToListAsync();
        }
    }
}
