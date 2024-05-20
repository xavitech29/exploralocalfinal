using exploralocalfinal.Servicios;

namespace exploralocalfinal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new Vistas.vSubirImagen());
        }
    }
}
