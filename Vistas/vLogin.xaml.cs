using exploralocalfinal.Modelos;
using exploralocalfinal.Servicios;
using Newtonsoft.Json;
using System.Text;

namespace exploralocalfinal.Vistas;

public partial class vLogin : ContentPage
{
    private readonly ILoginRepository _loginRepository;

    private readonly HttpClient _httpClient;
    public vLogin()
    {

        InitializeComponent();
        _loginRepository = new LoginService();
        _httpClient = new HttpClient(); // Inicializar _httpClient aqui
        _httpClient.BaseAddress = new Uri("http://192.168.0.18/exploralocal/api/");
    }


    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        var correo = txtCorreo.Text;
        var contrasena = txtContrasena.Text;

        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
        {
            await DisplayAlert("Error", "Ingrese correo y contraseña, por favor", "OK");
            return;
        }

        // Crear un objeto JSON con los datos de inicio de sesion
        var datos = new
        {
            correo = correo,
            contrasena = contrasena
        };

        // Serializar el objeto JSON
        var jsonData = JsonConvert.SerializeObject(datos);

        // Crear el contenido de la solicitud HTTP
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        try
        {
            // Realizar la solicitud HTTP POST al servidor
            var response = await _httpClient.PostAsync("usuarios.php", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);
                await Navigation.PushAsync(new vPrincipal(usuario));
            }
            else
            {
                // Manejar otros codigos de estado de respuesta segun sea necesario
                await DisplayAlert("Error", "Error al intentar iniciar sesion", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar la excepcion, por ejemplo, registrarla o notificar al usuario
            await DisplayAlert("Error", $"Error al intentar iniciar sesion: {ex.Message}", "OK");
        }
    }

    private void OnFacebookLoginClicked(object sender, EventArgs e)
    {
        // Implementa la lógica para iniciar sesión con Facebook
    }

    private void OnGmailLoginClicked(object sender, EventArgs e)
    {
        // Implementa la lógica para iniciar sesión con Gmail
    }

    private void OnInstagramLoginClicked(object sender, EventArgs e)
    {
        // Implementa la lógica para iniciar sesión con Instagram
    }

    private async void lblRegistrarse_Tapped(object sender, TappedEventArgs e)
    {
        // Navega a la vista de registro
        await Navigation.PushAsync(new vRegistro());
    }
}