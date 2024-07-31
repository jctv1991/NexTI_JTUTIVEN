using Microsoft.AspNetCore.Mvc;
using CPCore.Entidades;
using System.Text.Json;
using System.Net.Http;
using System.Threading;
using System.Text;

namespace miWebAdmin.Controllers
{
    [Route("miControlador")]
    [ApiController]
    public class miControladorController : Controller
    {
        private readonly HttpClient _httpClient;
        string miURL = "https://localhost:7160/miAPI/";

        // Constructor que recibe HttpClient a través de la inyección de dependencias
        public miControladorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("GetEventos")]
        public async Task<IActionResult> GetEventos()
        {
            try
            {
                // Realizar la solicitud GET a la API
                HttpResponseMessage response = await _httpClient.GetAsync(miURL + "GetAllEventos");

                // Verificar si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido JSON de la respuesta
                    List<Evento> eventos = await response.Content.ReadFromJsonAsync<List<Evento>>();
                    return Json(new { eventos });
                }
                else
                {
                    // Manejar el caso cuando la respuesta no es exitosa
                    return StatusCode((int)response.StatusCode, "Error al obtener eventos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor"); 
            }
        }

        [HttpPost]
        [Route("CrearEvento")]
        public async Task<IActionResult> CrearEvento([FromBody] Evento evento)
        {
            try
            {
                string requestUri = miURL + "CrearEvento";
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = new StringContent(
                        JsonSerializer.Serialize(evento),
                        Encoding.UTF8,
                        "application/json"
                    );

                    // Realizar la solicitud POST a la API
                    HttpResponseMessage response = await httpClient.PostAsync(requestUri, jsonContent);

                    // Verificar si la respuesta es exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { resultado = true, mensaje = "" });
                    }
                    else
                    {
                        // Manejar el caso cuando la respuesta no es exitosa
                        return Json(new { resultado = false, mensaje ="ERROR AL CREAR EVENTO" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { resultado = false, mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("actualizarEvento")]
        public async Task<IActionResult> actualizarEvento([FromBody] Evento evento)
        {
            try
            {
                string requestUri = miURL + "UpdateEvento";
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = new StringContent(
                        JsonSerializer.Serialize(evento),
                        Encoding.UTF8,
                        "application/json"
                    );

                    // Realizar la solicitud POST a la API
                    HttpResponseMessage response = await httpClient.PutAsync(requestUri, jsonContent);

                    // Verificar si la respuesta es exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { resultado = true, mensaje = "" });
                    }
                    else
                    {
                        // Manejar el caso cuando la respuesta no es exitosa
                        return Json(new { resultado = false, mensaje = "ERROR AL ACTUALIZAR EVENTO" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { resultado = false, mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarEvento")]
        public async Task<IActionResult> EliminarEvento(int id)
        {
            try
            {
                // Construir la URL para la solicitud DELETE
                string requestUri = miURL +  "DeleteEvento?id={id}";

                // Realizar la solicitud DELETE a la API
                HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri);

                // Verificar si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {
                    // La operación fue exitosa
                    return Json(new { resultado = true, mensaje = "" });
                }
                else
                {
                    // Manejar el caso cuando la respuesta no es exitosa
                    return Json(new { resultado = false, mensaje = "Error de eliminacion" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { resultado = false, mensaje = ex.Message });
            }
        }

    }
}
