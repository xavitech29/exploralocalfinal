using exploralocalfinal.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace exploralocalfinal.Servicios
{
    /*public class ResenaImagenService : IResenaImagenRepository
    {
        private const string ApiBaseUrl = "http://192.168.0.18/exploralocal/api";

        public async Task<List<ResenaImagenModel>> ObtenerResenasConImagenesAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var resenaImagenUrl = $"{ApiBaseUrl}/resenaimagen.php";
                    var response = await httpClient.GetAsync(resenaImagenUrl);
                    response.EnsureSuccessStatusCode();

                    var jsonString = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(jsonString);
                    var resenasConImagenes = JsonConvert.DeserializeObject<List<ResenaImagenModel>>(jsonString);

                    return resenasConImagenes;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud HTTP
                // Por ejemplo, puedes lanzar la excepción o manejarla de otra manera según tu caso de uso
                throw new Exception("Error al obtener las reseñas con imagenes", ex);
            }
        }
    }*/

    public class ResenaImagenService : IResenaImagenRepository
    {
        private const string ApiBaseUrl = "http://192.168.0.18/exploralocal/api";

        public async Task<List<ResenaImagenModel>> ObtenerResenasConImagenesAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var resenaImagenUrl = $"{ApiBaseUrl}/resenaimagen.php";
                    var response = await httpClient.GetAsync(resenaImagenUrl);
                    response.EnsureSuccessStatusCode();

                    var jsonString = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(jsonString); // Línea de depuración
                    var resenasConImagenes = JsonConvert.DeserializeObject<List<ResenaImagenModel>>(jsonString);

                    return resenasConImagenes;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al realizar la solicitud HTTP: {ex.Message}");
                return new List<ResenaImagenModel>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al deserializar el JSON: {ex.Message}");
                return new List<ResenaImagenModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return new List<ResenaImagenModel>();
            }
        }
    }
}


