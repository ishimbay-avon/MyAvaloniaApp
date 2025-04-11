using Avalonia.Input;
using Avalonia.Controls;
using System;

namespace MyAvaloniaApp.Views;
public partial class ProfilePageView : UserControl
{
    public ProfilePageView()
    {
        InitializeComponent();
    }

    public void OnTextBoxPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(sender as Control).Properties.IsRightButtonPressed)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                // Переключаем режим активности (IsReadOnly)
                textBox.IsReadOnly = !textBox.IsReadOnly;
            }
        }
    }
}