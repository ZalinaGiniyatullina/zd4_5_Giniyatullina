using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyPage : ContentPage
    {
        // Словарь с курсами валют (значения в рублях за 1 единицу)
        private readonly Dictionary<string, decimal> currencyRates = new Dictionary<string, decimal>
        {
            { "Доллар США (USD)", 80.50m },
            { "Евро (EUR)", 86.20m },
            { "Фунт стерлингов (GBP)", 100.80m },
            { "Швейцарский франк (CHF)", 88.40m },
            { "Китайский юань (CNY)", 11.20m }
        };

        // Дополнительная информация о валютах
        private readonly Dictionary<string, string> currencyInfo = new Dictionary<string, string>
        {
            { "Доллар США (USD)", "USD - Доллар США, основная резервная валюта мира. Эмитируется Федеральной резервной системой США." },
            { "Евро (EUR)", "EUR - Официальная валюта Евросоюза. Используется в 19 странах еврозоны." },
            { "Фунт стерлингов (GBP)", "GBP - Денежная единица Великобритании. Одна из старейших валют мира." },
            { "Швейцарский франк (CHF)", "CHF - Одна из самых стабильных валют. Является 'тихой гаванью' для инвесторов." },
            { "Китайский юань (CNY)", "CNY - Официальная валюта Китая. Одна из основных мировых валют." }
        };

        public CurrencyPage()
        {
            InitializeComponent();
            // Устанавливаем начальный выбор
            currencyPicker.SelectedIndex = 0;

            // Обновляем информацию при загрузке
            UpdateCurrencyInfo();
        }

        // Обработчик изменения значения слайдера
        private void AmountSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            amountLabel.Text = $"{e.NewValue:F0} 000 ₽";
        }

        // Обработчик нажатия кнопки
        private void ShowButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Проверка: выбран ли элемент в Picker
                if (currencyPicker.SelectedIndex == -1)
                {
                    DisplayAlert("Ошибка", "Пожалуйста, выберите валюту!", "OK");
                    return;
                }

                // Получаем выбранную валюту
                string selectedCurrency = currencyPicker.Items[currencyPicker.SelectedIndex];

                // Получаем курс из словаря
                decimal rate = currencyRates[selectedCurrency];

                // Получаем максимальное значение из слайдера
                double maxAmount = amountSlider.Maximum;

                // Получаем текущее значение слайдера
                double currentAmount = amountSlider.Value;

                // Расчет суммы в валюте
                decimal rubAmount = (decimal)currentAmount * 1000; // переводим тысячи в рубли
                decimal currencyAmount = rubAmount / rate;

                // Получаем информацию о валюте
                string info = currencyInfo[selectedCurrency];

                // Формируем результат с расшифровкой
                string result = $"{selectedCurrency}\n\n" +
                               $"Курс ЦБ РФ: {rate:F2} ₽\n" +
                               $"{currentAmount:F0} 000 ₽ = {currencyAmount:F2} {GetCurrencyCode(selectedCurrency)}\n\n" +
                               $"{info}\n\n" +
                               $"Максимальное значение слайдера: {maxAmount:F0} 000 ₽";

                // Отображаем результат
                resultLabel.Text = result;
                resultLabel.StyleClass = new[] { "result-value" };

                // Обновляем информационную панель
                currencyInfoLabel.Text = $"Выбрано: {selectedCurrency}\nКурс: {rate:F2} ₽";
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }

        // Обновление информации при смене валюты
        private void UpdateCurrencyInfo()
        {
            if (currencyPicker.SelectedIndex != -1)
            {
                string selectedCurrency = currencyPicker.Items[currencyPicker.SelectedIndex];
                if (currencyRates.ContainsKey(selectedCurrency))
                {
                    decimal rate = currencyRates[selectedCurrency];
                    currencyInfoLabel.Text = $"Выбрано: {selectedCurrency}\nКурс: {rate:F2} ₽";
                }
            }
        }

        // Вспомогательный метод для получения кода валюты
        private string GetCurrencyCode(string fullName)
        {
            if (fullName.Contains("USD")) return "USD";
            if (fullName.Contains("EUR")) return "EUR";
            if (fullName.Contains("GBP")) return "GBP";
            if (fullName.Contains("CHF")) return "CHF";
            if (fullName.Contains("CNY")) return "CNY";
            return "";
        }
    }
}