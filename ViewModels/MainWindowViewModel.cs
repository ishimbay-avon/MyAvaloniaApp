using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Windows.Input;
using System.Linq;
using MyAvaloniaApp.Views;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.Input;

namespace MyAvaloniaApp.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private bool _isReadOnly = true;
    public bool IsReadOnly
    {
        get => _isReadOnly;
        set
        {
            if (_isReadOnly != value)
            {
                _isReadOnly = value;
                OnPropertyChanged();
            }
        }
    }
    
    public ICommand ToggleEditCommand { get; }
    

    private bool _isPaneOpen;
    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set
        {
            _isPaneOpen = value;
            OnPropertyChanged(nameof(IsPaneOpen));
        }
    }

    private object? _selectedPageContent;
    public object? SelectedPageContent
    {
        get => _selectedPageContent;
        set
        {
            _selectedPageContent = value;
            OnPropertyChanged(nameof(SelectedPageContent));
        }
    }

    private string _selectedPageName = "Магазин";
    public string SelectedPageName
    {
        get => _selectedPageName;
        set
        {
            _selectedPageName = value;
            OnPropertyChanged(nameof(SelectedPageName));
        }
    }

    private UserProfile _userProfile = null!;

    public UserProfile UserProfile
    {
        get => _userProfile;
        set { _userProfile = value; OnPropertyChanged(); }
    }
    public ICommand BuyCommand { get; }

    public ICommand TogglePaneCommand => new RelayCommand(() => IsPaneOpen = !IsPaneOpen);

    private Product? _selectedProduct;
    private readonly HttpClient _httpClient = new HttpClient();

    public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
    public ObservableCollection<Cart> CartItems { get; } = new();

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            OnPropertyChanged();
        }
    }

    private int _totalItemsInCart;
    public int TotalItemsInCart
    {
        get => _totalItemsInCart;
        set
        {
            _totalItemsInCart = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel()
    {
        BuyCommand = new RelayCommand<Product>(OnBuy);
        LoadProductsAsync().ConfigureAwait(false);

        UserProfile = new UserProfile
        {
            PhotoUrl = "/Assets/1.jpeg",
            FirstName = "Иван",
            LastName = "Иванов",
            MiddleName = "Иванович",
            Phone = "+7 (123) 456-78-90",
            Email = "ivanov@example.com",
            Age = 30,
            Balance = 15000.50m,
            IsEditing = true,
            Photo = LoadBitmapFromResource("/Assets/1.jpeg")
        };

        SelectedPageName = "Магазин";
        SelectedPageContent = new ShopPageView();

        ToggleEditCommand = new RelayCommand(() => 
        {
            IsReadOnly = !IsReadOnly;
        });
    }

    private Bitmap? LoadBitmapFromResource(string path)
    {
        try
        {
            var uri = new Uri($"avares://MyAvaloniaApp{path}");
            return new Bitmap(AssetLoader.Open(uri));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading image: {ex.Message}");
            return null;
        }
    }

    public void OnBuy(Product product)
    {
        if (product == null) return;

        var existingItem = CartItems.FirstOrDefault(item => item.Product?.Name == product.Name);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            CartItems.Add(new Cart
            {
                Index = CartItems.Count + 1,
                Product = product,
                Quantity = 1
            });
        }

        TotalItemsInCart = CartItems.Count;
        OnPropertyChanged(nameof(CartSummary));

        Console.WriteLine($"Товар {product.Name} добавлен в корзину. Всего в корзине: {TotalItemsInCart}");
    }

    public string CartSummary
    {
        get
        {
            if (CartItems.Count == 0)
                return "Корзина пуста";

            return string.Join(": ", "Всего товаров", CartItems.Count);
        }
    }

    public async Task LoadProductsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:3000/products");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<Product[]>(json) ?? [];

                Products.Clear();
                foreach (var product in products)
                {
                    product.ImageWeb = await LoadFromWeb(product.Image);

                    Products.Add(product);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
        }
    }

    public async Task<Bitmap?> LoadFromWeb(string? imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
            return null;

        try
        {
            var response = await _httpClient.GetAsync(imageUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            await using var stream = await response.Content.ReadAsStreamAsync();
            return new Bitmap(stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            return null;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;

    public RelayCommand(Action<T> execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        if (parameter is T typedParam)
        {
            _execute(typedParam);
        }
    }

#pragma warning disable CS0067
    public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067
}