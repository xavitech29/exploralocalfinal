using exploralocalfinal.Servicios;

namespace exploralocalfinal.Vistas;

public partial class vActualizarEliminarImagen : ContentPage
{
    private string _imageUrl;
    private byte[] _imageBytes;
    
    //private string _imgUrl;

    private readonly IImagenRepository _imagenRepository;
    private int _imagenId;
    private string _imagenUrl;

    public vActualizarEliminarImagen(int imagenId, string imagenUrl)
    {

        /* InitializeComponent();
         UploadedImageView.IsVisible = false;
         _imagenRepository = new ImagenService();
         txtIdImagen.Text = _imagenId.ToString();// Asignar la id_imagen al Entry txtIdImagen

         _imagenId = imagenId; // Asignar el valor de imagenId a la propiedad _imagenId
         _imgUrl = imgUrl;

         // Mostrar la imagen seleccionada en la vista previa
         LoadImageFromUrl(imgUrl);*/

        InitializeComponent();
        _imagenRepository = new ImagenService();
        _imagenId = imagenId;
        _imagenUrl = imagenUrl;

        txtIdImagen.Text = _imagenId.ToString();
        ImageUrlLabel.Text = _imagenUrl;
        LoadImageFromUrl(imagenUrl);

    }

    private async void LoadImageFromUrl(string imageUrl)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(imageUrl);
                SelectedImagePreview.Source = ImageSource.FromStream(() => stream);
                //SelectedImagePreview.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
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

    private async void ActualizarImagenAsync(object sender, EventArgs e)
    {
        if (_imageBytes != null)
        {
            try
            {
                string nuevaUrlImagen = await _imagenRepository.SubirImagenAsync(_imageBytes);

                if (!string.IsNullOrEmpty(nuevaUrlImagen))
                {
                    if (_imagenId != 0)
                    {
                        // Si existe un ID de imagen, actualizamos la imagen
                        var exito = await _imagenRepository.ActualizarImagenAsync(_imagenId, nuevaUrlImagen);

                        if (exito)
                        {
                            await DisplayAlert("Éxito", "Imagen actualizada correctamente", "OK");
                            ImageUrlLabel.Text = $"URL: {nuevaUrlImagen}";
                            _imagenUrl = nuevaUrlImagen;
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error", "Ocurrió un error al actualizar la imagen", "OK");
                        }
                    }
                    else
                    {
                        // Si no hay un ID de imagen, mostramos un mensaje de error
                        await DisplayAlert("Error", "No se puede actualizar la imagen sin un ID de imagen", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "La subida de la imagen falló. Inténtalo de nuevo.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Por favor, selecciona o toma una imagen antes de subirla.", "OK");
        }
    }


    private async void Eliminar_Clicked(object sender, EventArgs e)
    {
        if (_imagenId != 0)
        {
            var exito = await _imagenRepository.EliminarImagenAsync(_imagenId);

            if (exito)
            {
                await DisplayAlert("Éxito", "Imagen eliminada correctamente", "OK");
                await Navigation.PopAsync();

            }
            else
            {
                await DisplayAlert("Error", "Ocurrió un error al eliminar la imagen", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Primero guarda una imagen para eliminarla", "OK");
        }
    }
    

}



