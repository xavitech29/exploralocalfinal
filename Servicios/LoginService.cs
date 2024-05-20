using exploralocalfinal.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploralocalfinal.Servicios
{
    public class LoginService : ILoginRepository
    {
        private readonly HttpClient _httpClient;

        public LoginService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://192.168.0.18/exploralocal/api/");
        }

        public async Task<Usuario> Login(string correo, string contrasena)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("correo", correo),
                    new KeyValuePair<string, string>("contrasena", contrasena)
                });

                var response = await _httpClient.PostAsync("usuarios.php", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);
                    return usuario;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Si recibimos un 401 Unauthorized, significa que las credenciales son incorrectas
                    return null;
                }
                else
                {
                    // Otro código de estado indica un problema diferente
                    throw new Exception($"Error al intentar iniciar sesión: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Error de red
                throw new Exception($"Error de red al intentar iniciar sesión: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Otro tipo de error
                throw new Exception($"Error al intentar iniciar sesión: {ex.Message}");
            }
        }
    }
}
