using System.Text.Json;
using System.Net.Http;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui;
using exploralocalfinal.Servicios;

namespace exploralocalfinal.Vistas;

public partial class vSubirImagen : ContentPage
{
    private string _imageUrl;
    private byte[] _imageBytes;
    private readonly IImagenRepository _imagenService;

    public vSubirImagen()
    {
        InitializeComponent();
        UploadedImageView.IsVisible = false;
        _imagenService = new ImagenService();

    }

    private async void PickPhotoAsync(object sender, EventArgs e)
    {
        var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        {
            Title = "Por favor, selecciona una foto"
        });

        if (file != null)
        {
            var stream = await file.OpenReadAsync();
            _imageBytes = new byte[stream.Length];
            await stream.ReadAsync(_imageBytes, 0, _imageBytes.Length);

            // Crear una nueva instancia de MemoryStream para la imagen
            var memoryStream = new MemoryStream(_imageBytes);

            // Asignar la imagen previa
            SelectedImagePreview.Source = ImageSource.FromStream(() => memoryStream);

            // Mostrar el control SelectedImagePreview
            SelectedImagePreview.IsVisible = true;

            ImageUrlLabel.Text = "Imagen seleccionada, listo para subir.";
        }
    }

    private async void TakePhotoAsync(object sender, EventArgs e)
    {
        var options = new MediaPickerOptions
        {
            Title = "Por favor, toma una foto"
        };
        var file = await MediaPicker.CapturePhotoAsync(options);

        if (file != null)
        {
            var stream = await file.OpenReadAsync();
            _imageBytes = new byte[stream.Length];
            await stream.ReadAsync(_imageBytes, 0, _imageBytes.Length);

            // Crear una nueva instancia de MemoryStream para la imagen
            var memoryStream = new MemoryStream(_imageBytes);

            // Asignar la imagen previa
            SelectedImagePreview.Source = ImageSource.FromStream(() => memoryStream);

            // Mostrar el control SelectedImagePreview
            SelectedImagePreview.IsVisible = true;

            ImageUrlLabel.Text = "Imagen tomada, listo para subir.";
        }
    }

    private async void UploadImageAsync(object sender, EventArgs e)
    {
        if (_imageBytes != null)
        {
            try
            {
                _imageUrl = await _imagenService.SubirImagenAsync(_imageBytes);
                ImageUrlLabel.Text = $"URL: {_imageUrl}";

                var postData = new Dictionary<string, string>
                    {
                        { "image_url", _imageUrl }
                    };

                var postContent = new FormUrlEncodedContent(postData);

                using (var httpClient = new HttpClient())
                {
                    var serverResponse = await httpClient.PostAsync("http://192.168.0.18/exploralocal/api/insertar_img.php", postContent);

                    if (serverResponse.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Exito", $"Imagen subida correctamente.", "OK");
                        //await DisplayAlert("Success", $"Image uploaded successfully. URL: {_imageUrl}", "OK");
                        // Show the uploaded image
                        var uploadedImageStream = await httpClient.GetStreamAsync(_imageUrl);
                        UploadedImageView.Source = ImageSource.FromStream(() => uploadedImageStream);
                        UploadedImageView.IsVisible = true;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error al cargar imagen al servidor", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Por favor, selecciona o toma una imagen antes de subir.", "OK");
        }
    }
}
