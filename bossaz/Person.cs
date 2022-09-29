using System.Text.RegularExpressions;

namespace bossaz;
class Person
{
    public Guid Id { get; }

    private string? _name;
    public string? Name
    {
        get { return _name; }
        init
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                throw new ArgumentException("Name is too short");

            _name = value;
        }
    }
    private string? _surname;
    public string? Surname
    {
        get { return _surname; }
        init
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                throw new ArgumentException("Surname is too short");

            _surname = value;
        }
    }

    private string? _phone;
    public string? Phone
    {
        get { return _phone; }
        init
        {
            string pattern = "^\\(?([0-9])\\)?[-.\\s]?([0-9])[-.\\s]?([0-9]){8,12}$";

            if (string.IsNullOrWhiteSpace(value) || !Regex.Match(value!, pattern).Success)
                throw new ArgumentException("Entered Phone number is wrong");
            
            _phone = value;
        }
    }
    public string? City { get; }

    private sbyte _age;

    public sbyte Age
    {
        get { return _age; }
        init
        {
            if (value < 18)
               throw new ArgumentException("Companies can't hire someone below 18");
                        
            _age = value;
        }
    }

    public Person(string? name, string? surname,
        string? city, string? phone, sbyte age)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        City = city;
        Phone = phone;
        Age = age;
    }

    public override string ToString()
    {
        return $@"Id: {Id}
{Name} {Surname} {Age} years old
from {City}
Contact via {Phone}";
    }
}
