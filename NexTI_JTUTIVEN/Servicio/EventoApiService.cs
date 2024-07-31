using CPCore.Entidades;

namespace miWebAdmin.Servicio
{
    public class EventoApiService
    {
        private readonly HttpClient _httpClient;

        public EventoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Evento>> ObtenerTodosEventosAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Evento>>("eventos");
        }

        public async Task<Evento> ObtenerEventoPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Evento>($"eventos/{id}");
        }

        public async Task<bool> CrearEventoAsync(Evento nuevoEvento)
        {
            var response = await _httpClient.PostAsJsonAsync("eventos", nuevoEvento);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarEventoAsync(int id, Evento eventoActualizado)
        {
            var response = await _httpClient.PutAsJsonAsync($"eventos/{id}", eventoActualizado);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarEventoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"eventos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
