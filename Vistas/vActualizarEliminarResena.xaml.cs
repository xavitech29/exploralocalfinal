using exploralocalfinal.Modelos;
using exploralocalfinal.Servicios;

namespace exploralocalfinal.Vistas;

public partial class vActualizarEliminarResena : ContentPage
{
    private readonly IResenaRepository _resenaRepository;
    private int _resenaId;
    private string _textoResena;


    public vActualizarEliminarResena(int resenaId, string textoResena)
	{
		InitializeComponent();
        _resenaRepository = new ResenaService();
        _resenaId = resenaId;
        _textoResena = textoResena;

        txtIdResena.Text = _resenaId.ToString();
        txtResena.Text = _textoResena;
    }


    private async void Actualizar_Clicked(object sender, EventArgs e)
    {
        var textoResena = txtResena.Text;
        if (_resenaId != 0 && !string.IsNullOrWhiteSpace(textoResena))
        {
            var exito = await _resenaRepository.ActualizarResenaAsync(_resenaId, textoResena);

            if (exito)
            {
                await DisplayAlert("�xito", "Rese�a actualizada correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Ocurri� un error al actualizar la rese�a", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Primero guarda una rese�a o el texto no puede estar vac�o", "OK");
        }
    }

    private async void Eliminar_Clicked(object sender, EventArgs e)
    {
        if (_resenaId != 0)
        {
            var exito = await _resenaRepository.EliminarResenaAsync(_resenaId);

            if (exito)
            {
                await DisplayAlert("�xito", "Rese�a eliminada correctamente", "OK");
                await Navigation.PopAsync();

            }
            else
            {
                await DisplayAlert("Error", "Ocurri� un error al eliminar la rese�a", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Primero guarda una rese�a para eliminarla", "OK");
        }
    }


}