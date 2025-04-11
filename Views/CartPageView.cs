using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace MyAvaloniaApp.Views;
public partial class CartPageView : UserControl
{
    public CartPageView()
    {
        InitializeComponent();
    }

    private void OnQuantityDoubleTapped(object sender, TappedEventArgs e)
    {
        var textBox = sender as TextBox;
        if (textBox != null)
        {
            // Включаем редактирование при двойном клике
            textBox.IsReadOnly = false;

            // Устанавливаем фокус на TextBox для удобства ввода
            textBox.Focus();
            textBox.SelectAll();
        }
    }

    private void OnQuantityLostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            if (int.TryParse(textBox.Text, out int newQuantity))
            {
                if (newQuantity <= 0)
            {
                // Если значение <= 0, ставим минимальное допустимое значение (например 1)
                newQuantity = 1;
            }
                // Обновляем количество в модели
                var cartItem = textBox.DataContext as Cart;
                if (cartItem != null)
                {
                    cartItem.Quantity = newQuantity;
                }
            }
            textBox.IsReadOnly = true;
        }
    }

    private void TextBox_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        // Проверяем, был ли это правый клик
        if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                // Переключаем состояние IsEnabled
                textBox.IsEnabled = !textBox.IsEnabled;
            }
        }
    }

}