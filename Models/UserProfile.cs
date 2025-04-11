using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Avalonia.Media.Imaging;

public class UserProfile : INotifyPropertyChanged
{
    private string? _photoUrl;
    private string? _firstName;
    private string? _lastName;
    private string? _middleName;
    private string? _phone;
    private string? _email;
    private int _age;
    private decimal _balance;
    private bool _isEditing;
    private Bitmap? _photo;

public Bitmap? Photo
    {
        get => _photo;
        set { _photo = value; OnPropertyChanged(); }
    }

    public string? PhotoUrl
    {
        get => _photoUrl;
        set { _photoUrl = value; OnPropertyChanged(); }
    }

    // Имя
    public string? FirstName
    {
        get => _firstName;
        set { _firstName = value; OnPropertyChanged(); }
    }

    // Фамилия
    public string? LastName
    {
        get => _lastName;
        set { _lastName = value; OnPropertyChanged(); }
    }

    // Отчество
    public string? MiddleName
    {
        get => _middleName;
        set { _middleName = value; OnPropertyChanged(); }
    }

    // Телефон (с валидацией в setter)
    public string? Phone
    {
        get => _phone;
        set 
        { 
            if (_phone != value)
            {
                _phone = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPhoneValid));
                OnPropertyChanged(nameof(PhoneValidationError));
            }
        }
    }

    // Email (с валидацией в setter)
    public string? Email
    {
        get => _email;
        set 
        { 
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmailValid));
                OnPropertyChanged(nameof(EmailValidationError));
            }
        }
    }

    // Возраст
    public int Age
    {
        get => _age;
        set 
        { 
            if (_age != value)
            {
                _age = value;
                OnPropertyChanged();
            }
        }
    }

    // Баланс (десятичное число)
    public decimal Balance
    {
        get => _balance;
        set 
        { 
            if (_balance != value)
            {
                _balance = value;
                OnPropertyChanged();
            }
        }
    }

    // Флаг режима редактирования
    public bool IsEditing
    {
        get => _isEditing;
        set 
        { 
            if (_isEditing != value)
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }
    }

 public string? PhoneValidationError => 
        IsPhoneValid || string.IsNullOrEmpty(Phone) ? null : "Неверный формат телефона";
    
    public string? EmailValidationError => 
        IsEmailValid || string.IsNullOrEmpty(Email) ? null : "Неверный формат email";

    // Валидация телефона (дополнительное свойство)
    public bool IsPhoneValid => 
        !string.IsNullOrEmpty(Phone) && 
        Regex.IsMatch(Phone, @"^\+?[\d\s\(\)-]+$");

    // Валидация email (дополнительное свойство)
    public bool IsEmailValid => 
        !string.IsNullOrEmpty(Email) && 
        Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}