using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AT_CSharp2_Oficial.Pages {
    [Authorize]
    public class ViewNotesModel : PageModel {

        public class InputModel {

            [Required(ErrorMessage = "O campo de texto é obrigatório.")]
            public string Content { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string Caminho { get; set; }
        public List<string> Arquivos { get; set; } = new();
        public string ConteudoLido { get; set; }
        public string NomeSelecionado { get; set; }

        private string ObterPasta() {
            string pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            return pasta;
        }

        private void CarregarArquivos(string pasta) {
            Arquivos = Directory.GetFiles(pasta, "*.txt").Select(Path.GetFileName).ToList();
        }

        public void OnGet(string file = null) {
            string pasta = ObterPasta();
            CarregarArquivos(pasta);

            if (!string.IsNullOrEmpty(file) && Path.GetFileName(file) == file) {
                string caminhoArquivo = Path.Combine(pasta, file);

                if (System.IO.File.Exists(caminhoArquivo)) {
                    NomeSelecionado = file;
                    ConteudoLido = System.IO.File.ReadAllText(caminhoArquivo);
                }
            }
        }

        public void OnPost() {
            if (!ModelState.IsValid)
                return;

            string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string nomeArquivo = $"note-{timestamp}.txt";
            string pasta = ObterPasta();
            string caminhoCompleto = Path.Combine(pasta, nomeArquivo);

            using (StreamWriter escrever = new StreamWriter(caminhoCompleto)) {
                escrever.WriteLine(Input.Content);
            }

            Caminho = $"/files/{nomeArquivo}";
            CarregarArquivos(pasta);
        }
    }
}
