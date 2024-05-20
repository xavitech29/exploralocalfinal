using exploralocalfinal.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace exploralocalfinal.Servicios
{
    public class ResenaService : IResenaRepository
    {
        private readonly HttpClient _httpClient;

        public ResenaService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://192.168.0.18/exploralocal/api/");
        }

        /* public async Task<Resena> GuardarResenaAsync(Resena resena)
         {
             try
             {
                 var json = JsonConvert.SerializeObject(resena);
                 var content = new StringContent(json, Encoding.UTF8, "application/json");

                 var response = await _httpClient.PostAsync("resena.php", content);

                 if (response.IsSuccessStatusCode)
                 {
                     var responseContent = await response.Content.ReadAsStringAsync();
                     return JsonConvert.DeserializeObject<Resena>(responseContent);
                 }
                 else
                 {
                     throw new Exception("Error al guardar la reseña");
                 }
             }
             catch (Exception ex)
             {
                 // Manejar la excepción, por ejemplo, registrarla o notificar al usuario
                 throw new Exception($"Error al intentar guardar la reseña: {ex.Message}");
             }
         }*/

        public async Task<Resena> GuardarResenaAsync(Resena resena)
        {
            try
            {
                var json = JsonConvert.SerializeObject(resena);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("resena.php", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Resena>(responseContent);
                }
                else
                {
                    throw new Exception("Error al guardar la reseña");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al intentar guardar la reseña: {ex.Message}");
            }
        }

    }
}
