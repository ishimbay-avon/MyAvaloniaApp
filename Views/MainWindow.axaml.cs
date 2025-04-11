using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MyAvaloniaApp.ViewModels;

namespace MyAvaloniaApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //DataContext = new MainWindowViewModel();
    }

    private void TogglePane(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        splitView.IsPaneOpen = !splitView.IsPaneOpen;
    }

    private void DebugBuyClick(object sender, RoutedEventArgs e)
    {
        // if (DataContext is MainWindowViewModel vm)
        // {
        //     Console.WriteLine($"BuyCommand exists: {vm.BuyCommand != null}");
        //     this.Title = $"BuyCommand exists: {vm.BuyCommand != null}";
        // }
        if (sender is Button button && button.DataContext is Product selectedProduct)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                // Теперь у вас есть выбранный продукт
                //this.Title = $"Выбран: {selectedProduct.Name}";
                var message = $"Товар: {selectedProduct.Name}\n" +
                             $"Количество: {selectedProduct.Quantity}\n" +
                             $"Изображение: {selectedProduct.Image}";

                this.Title = message;
                //vm.SelectedProduct = selectedProduct;
                vm.OnBuy(selectedProduct);

                // Если нужно вызвать команду ViewModel
                // if (vm.BuyCommand?.CanExecute(selectedProduct) == true)
                // {
                //     vm.BuyCommand.Execute(selectedProduct);
                // }
            }
        }
    }

    // private void OnProfileFieldRightTapped(object sender, PointerReleasedEventArgs e)
    // {
    //     // if (DataContext is MainWindowViewModel vm)
    //     // {
    //     //     vm.ToggleEditMode();
    //     // }
    //     if (e.InitialPressMouseButton == MouseButton.Right)
    //     {
    //         if (DataContext is MainWindowViewModel vm)
    //         {
    //             vm.ToggleEditMode();
    //         }
    //         e.Handled = true; // Предотвращаем дальнейшую обработку
    //     }
    // }

//     private void OnProfileFieldRightTapped(object sender, PointerReleasedEventArgs e)
// {
//     if (e.InitialPressMouseButton == MouseButton.Right && 
//         DataContext is MainWindowViewModel vm)
//     {
//         vm.UserProfile.IsEditing = !vm.UserProfile.IsEditing;
//         e.Handled = true;
//     }
// }
}