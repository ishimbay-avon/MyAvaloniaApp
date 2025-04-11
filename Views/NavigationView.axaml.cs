using Avalonia.Controls;
using Avalonia.Interactivity;
using MyAvaloniaApp.ViewModels;

namespace MyAvaloniaApp.Views;
public partial class NavigationView : UserControl
{
    public NavigationView()
    {
        InitializeComponent();
    }

    private void OnNavigationButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                switch (button.Tag?.ToString())
        {
            case "Shop":
                if (this.DataContext is MainWindowViewModel shopViewModel)
                {
                    shopViewModel.SelectedPageName = "Магазин";
                    shopViewModel.SelectedPageContent = new ShopPageView();
                }
                break;
            case "Cart":
                if (this.DataContext is MainWindowViewModel cartViewModel)
                {
                    cartViewModel.SelectedPageName = "Корзина";
                    cartViewModel.SelectedPageContent = new CartPageView();
                }
                break;
            case "Profile":
                if (this.DataContext is MainWindowViewModel profileViewModel)
                {
                    profileViewModel.SelectedPageName = "Профиль";
                    profileViewModel.SelectedPageContent = new ProfilePageView();
                }
                break;
        }
            }
        }
}


