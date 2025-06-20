using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AT_CSharp2_Oficial.Models;
using Microsoft.AspNetCore.Authorization;

namespace AT_CSharp2_Oficial.Pages.Simulador {
    [Authorize]
    public class Desconto : PageModel {

        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public Desconto(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public delegate decimal CalculateDelegate(decimal preco);

        public List<string> Memoria { get; set; } = new();

        private Action<string> Loggers { get; set; }

        [BindProperty]
        public decimal PrecoComDesconto { get; set; } = 0;

        public List<SelectListItem> TodasCidades { get; set; } = new();

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; } = default!;

        [BindProperty]
        public List<int> CidadesSelecionadas { get; set; } = new();

        private void InicializarLoggers() {
            if (Loggers == null) {
                Loggers = LogToConsole;
                Loggers += LogToFile;
                Loggers += LogToMemory;
            }
        }


        public void LogToConsole(string msg) {
            Console.WriteLine(msg);
        }

        public void LogToFile(string msg) {
            var pastaProjeto = Path.Combine(Directory.GetCurrentDirectory(), "txt");
            var caminho = Path.Combine(pastaProjeto, "log.txt");
            System.IO.File.AppendAllText(caminho, msg + Environment.NewLine);
        }

        public void LogToMemory(string msg) {
            Memoria.Add(msg);
        }

        public List<string> Logs { get; set; } = new();

        public void OnGet() {
            TodasCidades = _context.Cidades.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();
            InicializarLoggers();
        }


        public async Task<IActionResult> OnPostAsync() {
            TodasCidades = _context.Cidades.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();


            if (!ModelState.IsValid) {
                return Page();
            }

            InicializarLoggers();

            CalculateDelegate aplicacaoDoDesconto = preco => preco * 0.9m;
            PrecoComDesconto = aplicacaoDoDesconto(PacoteTuristico.Preco);
            Loggers?.Invoke($"Desconto: preço original R${PacoteTuristico.Preco} - com desconto R${PrecoComDesconto}");

            return Page();
        }

    }
}
