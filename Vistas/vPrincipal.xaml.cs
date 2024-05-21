using exploralocalfinal.Modelos;

namespace exploralocalfinal.Vistas;

public partial class vPrincipal : ContentPage
{
    private Usuario _usuario;
    public vPrincipal(Usuario usuario)
	{
		InitializeComponent();
        _usuario = usuario;
        MostrarDatosUsuario();
    }
    private void MostrarDatosUsuario()
    {
        if (_usuario != null)
        {
            lblNombreApellido.Text = $"Bienvenido, {_usuario.nombre} {_usuario.apellido}";
        }
    }
    private async void Mapa_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new vMapa());
    }

    private async void ListaLugares_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new vSubirImagen());
    }

    private async void DetalleLugar_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new vResenas());
    }

    private async void ResenaUsuario_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new vResenaUsuario());
    }

    private async void Configuracion_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new vConfiguracion());
    }
}