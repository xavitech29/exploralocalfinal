using exploralocalfinal.Modelos;
using exploralocalfinal.Servicios;

namespace exploralocalfinal.Vistas;

public partial class vRegistro : ContentPage
{
    private readonly IRegistroRepository _registroRepository;

    public vRegistro()
    {
        InitializeComponent();
        _registroRepository = new RegistroService();
    }

    private async void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        var nombre = txtNombreRegistro.Text;
        var apellido = txtApellidoRegistro.Text;
        var correo = txtCorreoRegistro.Text;
        var contrasena = txtContrasenaRegistro.Text;

        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
        {
            await DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
            return;
        }

        var usuario = new Usuario
        {
            nombre = nombre,
            apellido = apellido,
            correo = correo,
            contrasena = contrasena
        };

        var resultado = await _registroRepository.Registro(usuario);

        if (resultado)
        {
            await DisplayAlert("{Exito", "Registro exitoso", "OK");
            // Aqui puedes navegar a otra p?gina o realizar alguna otra accion despues del registro exitoso
        }
        else
        {
            await DisplayAlert("Error", "Error al registrar", "OK");
        }
    }
}