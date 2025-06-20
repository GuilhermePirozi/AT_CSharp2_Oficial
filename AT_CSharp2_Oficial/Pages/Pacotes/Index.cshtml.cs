using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Pacotes {
    [Authorize]
    public class IndexModel : PageModel {

        private readonly ILogger<IndexModel> _logger;

        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public IndexModel(AT_CSharp2_Oficial.Data.AppDbContext context, ILogger<IndexModel> logger) {
            _context = context;
            _logger = logger;
        }

        public IList<PacoteTuristico> PacoteTuristico { get; set; } = default!;

        public async Task OnGetAsync() {
            PacoteTuristico = await _context.Pacotes.ToListAsync();
        }
    }
}
