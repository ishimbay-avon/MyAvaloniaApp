using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;

public class Product: INotifyPropertyChanged
{
    // Поля, соответствующие JSON
    private string? _name;
    private string? _image;
    private int _quantity;
    private Bitmap? _imageWeb;

    // Свойства с атрибутами для десериализации
    [JsonProperty("name")]
    public string? Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    [JsonProperty("image")]
    public string? Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged();
        }
    }

    [JsonProperty("quantity")]
    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
        }
    }

    // Дополнительное свойство для загруженного изображения
    public Bitmap? ImageWeb
    {
        get => _imageWeb;
        set
        {
            _imageWeb = value;
            OnPropertyChanged();
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}