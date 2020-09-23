using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tthk_xamarin_grid
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CrossZeroGame();
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
