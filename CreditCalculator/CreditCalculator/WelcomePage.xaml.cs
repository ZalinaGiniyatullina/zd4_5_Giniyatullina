using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            // Проверка на заполнение поля
            if (string.IsNullOrWhiteSpace(surnameEntry.Text))
            {
                errorLabel.Text = "Пожалуйста, введите вашу фамилию!";
                errorLabel.IsVisible = true;
                return;
            }

            errorLabel.IsVisible = false;
            string surname = surnameEntry.Text.Trim();

            // Создаем главную страницу
            var mainTabbedPage = new MainTabbedPage(surname);

            // Переход с анимацией
            await Navigation.PushAsync(mainTabbedPage);
        }
    }
}