using System.Text.Json;
namespace bossaz;
using static CV;
using static Vacancy;
class Program
{
    static void Main(string[] args)
    {
        DirectoryInfo directory = new("BossAz");
        if (!directory.Exists)
            Directory.CreateDirectory(directory.FullName);

        FileInfo User = new($@"{directory.FullName}\User.json");
        FileInfo CV = new($@"{directory.FullName}\CV.json");
        FileInfo Vacancy = new($@"{directory.FullName}\Vacancy.json");

        List<User> users;
        if (User.Exists)
            users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(User.FullName))!;
        else
            users = new();

        List<CV> cvs;
        if (CV.Exists)
            cvs = JsonSerializer.Deserialize<List<CV>>(File.ReadAllText(CV.FullName))!;
        else
            cvs = new();

        List<Vacancy> vacancies;
        if (Vacancy.Exists)
            vacancies = JsonSerializer.Deserialize<List<Vacancy>>(File.ReadAllText(Vacancy.FullName))!;
        else
            vacancies = new();

        string[] start = new[] { "Sign up", "Log in", "Exit" };
        string[] type = new[] { "As Worker", "As Employer", "Exit" };
        int index = 0;
        bool exit = false;
        bool isEmployer = false;
        bool isRegistering = true;
        bool end = false;
        ConsoleKeyInfo key;

        while (!exit)
        {
            Console.Clear();
            Menu.Show(start, index);
            key = Console.ReadKey();

            Menu.Cursor(key, start.Length, ref index);
            if (key.Key == ConsoleKey.Enter)
            {
                switch (index)
                {
                    case 0:
                        isRegistering = true;
                        exit = true;
                        break;
                    case 1:
                        isRegistering = false;
                        exit = true;
                        break;
                    case 2:
                        end = true;
                        exit = true;
                        break;
                }
            }
        }

        if (end)
            Environment.Exit(0);

        exit = false;

        while (!exit)
        {
            Console.Clear();
            Menu.Show(type, index);
            key = Console.ReadKey();

            Menu.Cursor(key, type.Length, ref index);

            if (key.Key == ConsoleKey.Enter)
            {
                switch (index)
                {
                    case 0:
                        isEmployer = false;
                        exit = true;
                        break;
                    case 1:
                        isEmployer = true;
                        exit = true;
                        break;
                    case 2:
                        end = true;
                        exit = true;
                        break;
                }
            }
        }

        if (end)
            Environment.Exit(0);

        exit = false;

        string username;
        string password;
        User? currentUser = null;

        if (isRegistering)
        {
            while (!exit)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("What should we call you");
                    username = Console.ReadLine()!;
                    if (users.Any(user => user.Username == username))
                    {
                        Console.WriteLine("This username is already used");
                        Console.ReadKey(true);
                        continue;
                    }

                    Console.WriteLine("What should your password be?");
                    password = Console.ReadLine()!;

                    currentUser = new(username, password);

                    if (isEmployer)
                        currentUser.Human = Employer.AddEmployer();
                    else
                        currentUser.Human = Worker.AddWorker();

                    users.Add(currentUser);
                    exit = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }
        else
        {
            while (!exit)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Username:");
                    username = Console.ReadLine()!;
                    Console.WriteLine("Password:");
                    password = Console.ReadLine()!;

                    foreach (var user in users)
                    {
                        if (user.Username == username && user.Password == password)
                        {
                            currentUser = user;
                            exit = true;
                            break;
                        }
                    }

                    if (!exit)
                    {
                        Console.WriteLine("Wrong username/password");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }

        string[] workerMenu = new[] { "Create CV", "See Vacancies", "Filter Vacancies", "Exit" };
        string[] employerMenu = new[] { "Add vacancy", "See CVs", "Filter Workers", "Exit" };

        exit = false;
        index = 0;

        if (!isEmployer)
        {
            while (!exit)
            {
                Console.Clear();
                Menu.Show(workerMenu, index);
                key = Console.ReadKey();

                Menu.Cursor(key, workerMenu.Length, ref index);

                if (key.Key == ConsoleKey.Enter)
                {

                    switch (index)
                    {
                        case 0:
                            currentUser!.Human = new Worker(currentUser!.Human!.Name, currentUser!.Human.Surname, currentUser.Human.City, currentUser!.Human.Phone, currentUser.Human.Age, CreateCV());
                            cvs.Add((currentUser!.Human as Worker)!.Cv);
                            break;
                        case 1:
                            foreach (var v in vacancies)
                                Console.WriteLine(v);

                            Console.ReadKey();
                            break;
                        case 2:
                            index = 0;
                            while (!exit)
                            {
                                Console.Clear();
                                Menu.Show(Filter.WorkerFilter, index);
                                key = Console.ReadKey();

                                Menu.Cursor(key, Filter.WorkerFilter.Length, ref index);
                                if (key.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    switch (index)
                                    {
                                        case 0:
                                            Console.WriteLine("Minimum salary:");
                                            if (!double.TryParse(Console.ReadLine()!, out double minSalary))
                                            {
                                                Console.WriteLine("Invalid value");
                                                Console.ReadKey();
                                                continue;
                                            }

                                            foreach (var vacany in Filter.VacancySalary(vacancies!, minSalary))
                                                Console.WriteLine(vacany);

                                            Console.ReadKey(true);
                                            break;

                                        case 1:
                                            Console.WriteLine("Enter The age");
                                            if (!sbyte.TryParse(Console.ReadLine()!, out sbyte age))
                                            {
                                                Console.WriteLine("Invalid value");
                                                Console.ReadKey();
                                                continue;
                                            }

                                            if (age < 18)
                                            {
                                                Console.WriteLine("You can't hire minors here");
                                                Console.ReadKey();
                                                continue;
                                            }

                                            foreach (var vacancy in Filter.VacancyAge(vacancies!, age))
                                                Console.WriteLine(vacancy);

                                            Console.ReadKey();
                                            break;

                                        case 2:
                                            Console.WriteLine("Enter The experience in months");
                                            if (!sbyte.TryParse(Console.ReadLine()!, out sbyte experience))
                                            {
                                                Console.WriteLine("Invalid Value");
                                                Console.ReadKey();
                                                continue;
                                            }

                                            foreach (var vacancy in Filter.VacancyExperience(vacancies!, experience))
                                                Console.WriteLine(vacancy);

                                            Console.ReadKey();
                                            break;

                                        case 3:
                                            exit = true;
                                            break;
                                    }
                                }
                            }
                            exit = false;
                            break;

                        case 3:
                            exit = true;
                            break;

                    }
                }
            }
        }
        else
        {
            while (!exit)
            {
                Console.Clear();
                Menu.Show(employerMenu, index);
                key = Console.ReadKey();

                Menu.Cursor(key, employerMenu.Length, ref index);
               
                if (key.Key == ConsoleKey.Enter)
                {
                    switch (index)
                    {
                        case 0:
                            currentUser!.Human = new Employer(currentUser!.Human!.Name, currentUser!.Human.Surname, currentUser.Human.City, currentUser!.Human.Phone, currentUser.Human.Age);
                            (currentUser!.Human as Employer)!.AddVacancy();
                            int count = (currentUser!.Human as Employer)!.Vacancies.Count;
                            vacancies!.Add((currentUser!.Human as Employer)!.Vacancies[count - 1]);
                            break;
                        case 1:
                            foreach (var cv in cvs)
                                cv?.Print();
                            Console.ReadKey();
                            break;
                        case 2:
                            index = 0;
                            while (!exit)
                            {
                                Console.Clear();
                                Menu.Show(Filter.EmployerFilter, index);
                                key = Console.ReadKey(true);

                                Menu.Cursor(key, Filter.EmployerFilter.Length, ref index);
                                if (key.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    switch (index)
                                    {

                                        case 0:
                                            Console.WriteLine("Enter The experience in month");
                                            if (!sbyte.TryParse(Console.ReadLine()!, out sbyte experience))
                                            {
                                                Console.WriteLine("Invalid value");
                                                Console.ReadKey(true);
                                                continue;
                                            }

                                            foreach (var cv in Filter.CVExperience(cvs!, experience))
                                                cv.Print();

                                            Console.ReadKey(true);
                                            break;
                                        case 1:
                                            exit = true;
                                            break;
                                    }
                                }
                            }
                            exit = false;
                            break;
                        case 3:
                            exit = true;
                            break;

                    }
                }
            }
        }

        File.WriteAllText(User.FullName, JsonSerializer.Serialize(users));
        File.WriteAllText(CV.FullName, JsonSerializer.Serialize(cvs));
        File.WriteAllText(Vacancy.FullName, JsonSerializer.Serialize(vacancies));

    }
}
