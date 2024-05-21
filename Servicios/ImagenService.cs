using exploralocalfinal.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace exploralocalfinal.Servicios
{
    public class ImagenService : IImagenRepository
    {
        private const string ImgBBApiKey = "a5c7aa99467462d70c608c55b59a7e0a";
        private readonly HttpClient _httpClient;

 
        public ImagenService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://192.168.0.18/exploralocal/api/");
        }
        //
        public async Task<string> SubirImagenAsync(byte[] imagenBytes)
        {
            using (var httpClient = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new ByteArrayContent(imagenBytes), "image", "image.jpg");
                    formData.Add(new StringContent(ImgBBApiKey), "key");

                    var response = await httpClient.PostAsync("https://api.imgbb.com/1/upload", formData);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = System.Text.Json.JsonSerializer.Deserialize<ImgBBUploadResult>(responseContent);

                    if (result != null && result.data != null && result.data.url != null)
                    {
                        return result.data.url;
                    }
                    else
                    {
                        throw new Exception("Failed to upload image to ImgBB");
                    }
                }
            }
        }

        public async Task<bool> ActualizarImagenAsync(int id, string nuevaUrlImagen)
        {
            try
            {
                var data = new Dictionary<string, string>
        {
            { "id_imagen", id.ToString() },
            { "url_imagen", nuevaUrlImagen }
        };
                var content = new FormUrlEncodedContent(data);

                var response = await _httpClient.PutAsync("imagen.php", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la imagen: {ex.Message}");
            }
        }


        public async Task<bool> EliminarImagenAsync(int idImagen)
        {
            var httpClient = new HttpClient();
            var uri = new Uri($"http://192.168.0.18/exploralocal/api/imagen.php?id_imagen={idImagen}");

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
                throw new Exception($"Error al eliminar la imagen: {ex.Message}");
            }
        }




    }
}
