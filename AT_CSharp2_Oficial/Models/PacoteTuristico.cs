namespace AT_CSharp2_Oficial.Models {
    public class PacoteTuristico {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? Data { get; set; }
        public decimal Preco { get; set; }
        public int LimiteParticipantes { get; set; }
        public List<Destino> Destinos { get; set; } = new();
        public List<Reserva> Reservas { get; set; } = new();

        public delegate void CapacityReachedDelegate(string mensagem);

        public event CapacityReachedDelegate CapacityReached;
        public void AdicionarReserva(Reserva reserva) {

            if (Reservas.Count >= LimiteParticipantes) {

                CapacityReached?.Invoke("Limite de participantes excedido.");
                return;
            }
            Reservas.Add(reserva);
        }
    }
}
