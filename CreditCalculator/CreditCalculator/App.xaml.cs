using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCalculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Стартовая страница - WelcomePage в NavigationPage
            MainPage = new NavigationPage(new WelcomePage());
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