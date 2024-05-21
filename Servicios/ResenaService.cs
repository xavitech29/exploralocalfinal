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

        public async Task<bool> ActualizarResenaAsync(int idResena, string nuevoTexto)
        {
            var httpClient = new HttpClient();
            var uri = new Uri($"http://192.168.0.18/exploralocal/api/resena.php?id_resena={idResena}");

            var data = new Dictionary<string, string>
    {
        { "id_resena", idResena.ToString() },
        { "texto", nuevoTexto }
    };

            var content = new FormUrlEncodedContent(data);

            try
            {
                var response = await httpClient.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Actualización exitosa
                }
                else
                {
                    return false; // Error al actualizar
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la reseña: {ex.Message}");
            }
        }

        public async Task<bool> EliminarResenaAsync(int idResena)
        {
            var httpClient = new HttpClient();
            var uri = new Uri($"http://192.168.0.18/exploralocal/api/resena.php?id_resena={idResena}");

            try
            {
                var response = await httpClient.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Eliminación exitosa
                }
                else
                {
                    return false; // Error al eliminar
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la reseña: {ex.Message}");
            }
        }


    }
}
