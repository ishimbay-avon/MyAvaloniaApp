using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MyAvaloniaApp.ViewModels;

namespace MyAvaloniaApp.Views;
public partial class ShopPageView : UserControl
{
    public ShopPageView()
    {
        InitializeComponent();
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
                // var message = $"Товар: {selectedProduct.Name}\n" +
                //              $"Количество: {selectedProduct.Quantity}\n" +
                //              $"Изображение: {selectedProduct.Image}";
                
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
}