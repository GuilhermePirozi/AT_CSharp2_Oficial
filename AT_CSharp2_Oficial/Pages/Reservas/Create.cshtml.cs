using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AT_CSharp2_Oficial.Data;
using AT_CSharp2_Oficial.Models;
using Microsoft.EntityFrameworkCore;

namespace AT_CSharp2_Oficial.Pages.Reservas {
    public class CreateModel : PageModel {
        private readonly AT_CSharp2_Oficial.Data.AppDbContext _context;

        public CreateModel(AT_CSharp2_Oficial.Data.AppDbContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["PacoteTuristicoId"] = new SelectList(_context.Pacotes, "Id", "Id");
            return Page();
        }


        [BindProperty]
        public Reserva Reserva { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync() {

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["PacoteTuristicoId"] = new SelectList(_context.Pacotes, "Id", "Nome");

            bool reservaExistente = await _context.Reservas.AnyAsync(r =>
            r.ClienteId == Reserva.ClienteId &&
            r.PacoteTuristicoId == Reserva.PacoteTuristicoId &&
            r.DataReserva.Date == Reserva.DataReserva.Date);

            if (reservaExistente) {
                ModelState.AddModelError(string.Empty, "Você já possui uma reserva para este pacote nesta data.");
                return Page();
            }

            var pacote = await _context.Pacotes.Include(p => p.Reservas).FirstOrDefaultAsync(p => p.Id == Reserva.PacoteTuristicoId);

            bool capacityReached = false;

            pacote.CapacityReached += (msg) => {
                Console.WriteLine($"{msg}");
                ModelState.AddModelError(string.Empty, msg);
                capacityReached = true;
            };

            pacote.AdicionarReserva(Reserva);

            if (capacityReached) {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
                ViewData["PacoteTuristicoId"] = new SelectList(_context.Pacotes, "Id", "Nome");
                return Page();
            }

            {
                if (!ModelState.IsValid) {
                    return Page();
                }

                if (pacote.Data <= DateTime.Today) {
                    ModelState.AddModelError(string.Empty, "Pacote com data passada não pode ser reservado.");
                    return Page();
                }


                if (pacote.Reservas.Count >= pacote.LimiteParticipantes) {
                    ModelState.AddModelError(string.Empty, "Limite de participantes atingido.");
                    return Page();
                }

                Reserva.PacoteTuristico = pacote;
                _context.Reservas.Add(Reserva);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
        }
    }
}
