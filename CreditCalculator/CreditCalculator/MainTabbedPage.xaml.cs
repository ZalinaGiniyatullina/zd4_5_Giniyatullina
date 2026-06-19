using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        private string userSurname;

        public MainTabbedPage() : this(null)
        {
        }

        public MainTabbedPage(string surname)
        {
            InitializeComponent();
            userSurname = surname;

            // Создаем страницы
            var creditPage = new CreditPage();
            var currencyPage = new CurrencyPage();
            var welcomePage = new WelcomePage();

            // Если на WelcomePage есть метод для установки фамилии
            // welcomePage.SetSurname(surname);

            Children.Add(creditPage);
            Children.Add(currencyPage);
            Children.Add(welcomePage);

            // Устанавливаем заголовки
            creditPage.Title = "Кредит";
            currencyPage.Title = "Курсы";

            // Отображаем фамилию в заголовке вкладки
            if (!string.IsNullOrEmpty(userSurname))
            {
                welcomePage.Title = $"👤 {userSurname}";
            }
            else
            {
                welcomePage.Title = "Профиль";
            }
        }
    }
}