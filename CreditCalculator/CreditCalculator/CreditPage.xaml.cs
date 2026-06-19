using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditPage : ContentPage
    {
        public CreditPage()
        {
            InitializeComponent();
            paymentPicker.SelectedIndex = 0; // По умолчанию выбран аннуитетный платеж
        }

        // Обработчик изменения значения слайдера
        private void RateSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            rateLabel.Text = $"{e.NewValue:F0} %";
        }

        // Обработчик нажатия кнопки "Рассчитать"
        private void Calculate_Clicked(object sender, EventArgs e)
        {
            double sum;
            int months;

            // Проверка ввода суммы кредита
            if (!double.TryParse(sumEntry.Text, out sum) || sum <= 0)
            {
                DisplayAlert("Ошибка", "Введите корректную сумму кредита", "OK");
                return;
            }

            // Проверка ввода срока кредита
            if (!int.TryParse(monthEntry.Text, out months) || months <= 0)
            {
                DisplayAlert("Ошибка", "Введите корректный срок кредита", "OK");
                return;
            }

            // Расчет ежемесячной процентной ставки
            double rate = rateSlider.Value / 100 / 12;

            // Проверка на нулевую ставку
            if (rate == 0)
            {
                DisplayAlert("Ошибка", "Процентная ставка не может быть равна 0", "OK");
                return;
            }

            // Проверка на аннуитетный платеж (выбран по умолчанию)
            if (paymentPicker.SelectedIndex == 0)
            {
                // Аннуитетный платеж
                double monthly = sum * (rate * Math.Pow(1 + rate, months)) / (Math.Pow(1 + rate, months) - 1);
                double total = monthly * months;
                double overpayment = total - sum;

                monthlyLabel.Text = $"Ежемесячный платеж: {monthly:F2}";
                totalLabel.Text = $"Общая сумма: {total:F2}";
                overpaymentLabel.Text = $"Переплата: {overpayment:F2}";
            }
            else
            {
                // Дифференцированный платеж
                // Для дифференцированного платежа ежемесячный платеж меняется,
                // поэтому выводим график платежей или только основные показатели

                double total = 0;
                double firstPayment = 0;
                double lastPayment = 0;

                for (int i = 1; i <= months; i++)
                {
                    double principalPayment = sum / months;
                    double interestPayment = (sum - principalPayment * (i - 1)) * rate;
                    double monthlyPayment = principalPayment + interestPayment;

                    total += monthlyPayment;

                    if (i == 1) firstPayment = monthlyPayment;
                    if (i == months) lastPayment = monthlyPayment;
                }

                double overpayment = total - sum;

                monthlyLabel.Text = $"Платеж: от {firstPayment:F2} до {lastPayment:F2}";
                totalLabel.Text = $"Общая сумма: {total:F2}";
                overpaymentLabel.Text = $"Переплата: {overpayment:F2}";
            }
        }
    }
}