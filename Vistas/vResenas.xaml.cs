using exploralocalfinal.Modelos;
using exploralocalfinal.Servicios;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace exploralocalfinal.Vistas;

public partial class vResenas : ContentPage, INotifyPropertyChanged
{
    private readonly IResenaImagenRepository _resenaImagenRepository;

    private List<ResenaImagenModel> _resenasConImagenes;
    public List<ResenaImagenModel> ResenasConImagenes
    {
        get { return _resenasConImagenes; }
        set
        {
            _resenasConImagenes = value;
            OnPropertyChanged(nameof(ResenasConImagenes));
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData(); // Cargar los datos cada vez que se muestre la página
    }
    public vResenas()
    {
        InitializeComponent();
        BindingContext = this;
        _resenaImagenRepository = new ResenaImagenService();
        ResenasConImagenes = new List<ResenaImagenModel>();
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            var resenasConImagenes = await _resenaImagenRepository.ObtenerResenasConImagenesAsync();
            ResenasConImagenes = resenasConImagenes;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void BtnImagen_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var resena = button.BindingContext as ResenaImagenModel;

        if (resena != null)
        {
            
            int id_img = resena.id_imagen;
            string url_img = resena.url_imagen;
            await Navigation.PushAsync(new vActualizarEliminarImagen(id_img, url_img));
        }
    }
   
    private async void BtnResena_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var resena = button.BindingContext as ResenaImagenModel;

        if (resena != null)
        {
            int id = resena.id_resena;
            string texto = resena.texto;
            await Navigation.PushAsync(new vActualizarEliminarResena(id, texto));
        }
    }

}