using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Cart : INotifyPropertyChanged
{
    public int Index { get; set; }
    private Product? _product;
    private int _quantity;

    private bool _isAvailable;

    public Product? Product
    {
        get => _product;
        set
        {
            _product = value;
            OnPropertyChanged();
            UpdateAvailability();
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
            UpdateAvailability();
        }
    }

    public bool IsAvailable
    {
        get => _isAvailable;
        set
        {
            _isAvailable = value;
            OnPropertyChanged();
        }
    }

    private void UpdateAvailability()
    {
        if (Product != null)
        {
            IsAvailable = Quantity <= Product.Quantity;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

