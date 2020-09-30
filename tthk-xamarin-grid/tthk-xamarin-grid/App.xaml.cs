using Xamarin.Forms;

namespace tthk_xamarin_grid
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new CrossZeroGame());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
