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
                await DisplayAlert("Éxito", "Reseña actualizada correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Ocurrió un error al actualizar la reseña", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Primero guarda una reseña o el texto no puede estar vacío", "OK");
        }
    }

    private async void Eliminar_Clicked(object sender, EventArgs e)
    {
        if (_resenaId != 0)
        {
            var exito = await _resenaRepository.EliminarResenaAsync(_resenaId);

            if (exito)
            {
                await DisplayAlert("Éxito", "Reseña eliminada correctamente", "OK");
                await Navigation.PopAsync();

            }
            else
            {
                await DisplayAlert("Error", "Ocurrió un error al eliminar la reseña", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Primero guarda una reseña para eliminarla", "OK");
        }
    }


}