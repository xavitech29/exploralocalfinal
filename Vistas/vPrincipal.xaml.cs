namespace exploralocalfinal.Vistas;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
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
        await Navigation.PushAsync(new vDetalleLugar());
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