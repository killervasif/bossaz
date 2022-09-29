namespace bossaz;
class Employer : Person
{
    public List<Vacancy>? Vacancies=new List<Vacancy>();
    public Employer( string name,
        string surname,string city,string phone,sbyte age, List<Vacancy>? vacancies=null)
        :base(name,surname,city,phone,age)
    {
        if(vacancies is null)
            Vacancies = new ();
        else
            Vacancies = vacancies;
    }
    public override string ToString()
    {
        return base.ToString();
    }
    public void ShowAllVacancies()
    {
        Console.WriteLine($@"Vacancies from {Name} {Surname}");
        foreach (var Vacancy in Vacancies!)
        {
            Console.WriteLine(Vacancy);
            Console.WriteLine();
        }
    }

    public void AddVacancy()
    {
        while (true)
        {
            Vacancy vac;
            try
            {
                Console.WriteLine("What's the name of the job?");
                string name = Console.ReadLine()!;

                Console.WriteLine("How much money do you offer?");
                double salary = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("How much experience does a worker need?");
                double minExp = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("What's the age gap for the job?");
                double maxAge = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("How long is the contract?");
                double period = Convert.ToDouble(Console.ReadLine());
                vac = new(name, salary, minExp, maxAge, period);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            Vacancies?.Add(vac);
            break;
        }
    }

    public static Worker AddEmployer()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("What's your name?");
                string name = Console.ReadLine()!;
                Console.WriteLine("What's your surname?");
                string surname = Console.ReadLine()!;
                Console.WriteLine("Where are you from(as city)?");
                string city = Console.ReadLine()!;
                Console.WriteLine("What's your Phone number");
                string phone = Console.ReadLine()!;

                Console.WriteLine("How old are you?");
                if (!sbyte.TryParse(Console.ReadLine(), out sbyte age))
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey();
                    continue;
                }
                return new(name, surname, city, phone, age);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                continue;
            }
        }
    }
}
