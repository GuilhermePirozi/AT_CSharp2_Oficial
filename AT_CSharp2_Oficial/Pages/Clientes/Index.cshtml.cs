using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Clientes {
    [Authorize]
    public class IndexModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public IndexModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public IList<Cliente> Cliente { get; set; } = default!;

        public async Task OnGetAsync() {
            Cliente = await _context.Clientes.ToListAsync();
        }
    }
}
