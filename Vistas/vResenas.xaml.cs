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
}