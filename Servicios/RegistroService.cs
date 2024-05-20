using exploralocalfinal.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploralocalfinal.Servicios
{
    public class RegistroService : IRegistroRepository
    {
        private readonly HttpClient _httpClient;

        public RegistroService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://192.168.0.18/exploralocal/api/");
        }

        public async Task<bool> Registro(Usuario usuario)
        {
            try
            {
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("registro.php", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción, por ejemplo, registrarla o notificar al usuario
                throw new Exception($"Error al intentar registrar: {ex.Message}");
            }
        }
    }
}
