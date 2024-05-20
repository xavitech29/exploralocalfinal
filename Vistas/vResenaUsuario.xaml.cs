using exploralocalfinal.Modelos;
using exploralocalfinal.Servicios;

namespace exploralocalfinal.Vistas;

public partial class vResenaUsuario : ContentPage
{
    private readonly IResenaRepository _resenaRepository;
    public vResenaUsuario()
	{
		InitializeComponent();
        _resenaRepository = new ResenaService();
    }

    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        var textoResena = txtResena.Text;
        if (!string.IsNullOrWhiteSpace(textoResena))
        {
            var resena = new Resena { texto = textoResena };
            var resenaGuardada = await _resenaRepository.GuardarResenaAsync(resena);

            if (resenaGuardada != null)
            {
                await DisplayAlert("�xito", "Rese�a guardada correctamente", "OK");
                txtResena.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al guardar la rese�a", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "El texto de la rese�a no puede estar vac�o", "OK");
        }
    }
}