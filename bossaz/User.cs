using System.Text.RegularExpressions;

namespace bossaz;

class User
{
    private string? _username;
    public string? Username
    {
        get { return _username; }
        set
        {
            if (string.IsNullOrEmpty(value) || !Regex.Match(value, "^[a-zA-Z0-9_]{3,}$").Success)
                throw new ArgumentException($"{nameof(Username)} is invalid");
                         
            _username = value;
        }
    }

    private string? _password;
    public string? Password
    {
        get { return _password; }
        set
        {
            if (string.IsNullOrEmpty(value) || !Regex.Match(value, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$").Success)
               throw new ArgumentException($"{nameof(Password)} is invalid");
                       
            _password = value;
        }
    }

    public Person Human { get; set; } = null!;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
