using System.Text.Json.Serialization;

namespace bossaz;
enum Level { A1=1,A2,B1,B2,C1,C2}
class Language
{
    public string Name { get; set; }
    public Level LanguageLevel { get; set; }

    public Language(string name, Level languageLevel)
    {
        Name = name;
        LanguageLevel = languageLevel;
    }

    public override string ToString()
    {
        return $@"{Name}-{LanguageLevel}";
    }
}

class CV
{

    [JsonConstructor]
    public CV() { }

    public CV(string speciality, string school, double enteranceScore, double experience, string[] skills, string[] companies, bool hasSpecialDiploma, string githubUsername, string linkedInUsername, List<Language> languagages = null)
    {
        Speciality = speciality;
        School = school;
        EnteranceScore = enteranceScore;
        Experience = experience;
        Skills = skills;
        Companies = companies;
        if (languagages is null)
            Languages = new();
        else
            Languages = languagages;
        this.hasSpecialDiploma = hasSpecialDiploma;
        GithubUsername = githubUsername;
        LinkedInUsername = linkedInUsername;
    }

    public string? Speciality { get; }
    public string? School { get; }
    public double EnteranceScore { get; }
    public double Experience { get; }
    public string[]? Skills { get; } = null;
    public string[]? Companies { get; } = null;
    public List<Language>? Languages { get; } = null;
    public bool hasSpecialDiploma { get; }
    public string? GithubUsername { get; } 
    public string? LinkedInUsername { get; } 

    public static CV CreateCV()
    {
        while (true)
        {
            try
            {
                string input;

                Console.WriteLine("What's your Speciality?");
                string Speciality = Console.ReadLine();
                Console.WriteLine("Where's your School?");
                string School = Console.ReadLine();

                Console.WriteLine("What was your University exam score?");
                double Score = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("How many months of Experience do you have?");
                double Experience = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("What about your Skills? (Use | for multiple skills)");
                string[] Skills = Console.ReadLine().Split('|');
                Console.WriteLine("What companies have you ever worked at? (Use | for multiple companies)");
                string[] Companies = Console.ReadLine().Split('|');

                Console.WriteLine("Do you have a special diploma? Type Yes if you have one");
                bool HasDiploma = Console.ReadLine().ToLower() == "yes" ? true : false;

                Console.WriteLine("What's your github username if you have one? Type none if you don't");
                input = Console.ReadLine();
                string? github;
                if (input.ToLower() == "none")
                    github = null;
                else
                    github = input;
                Console.WriteLine("What's your LinkedIn username if you have one? Type none if you don't");
                input = Console.ReadLine();
                string? linkedIn;
                if (input.ToLower() == "none")
                    linkedIn = null;
                else
                    linkedIn = input;

                bool exit = false;
                string language;
                List<Language> languages = new();
                Level level;
                do
                {
                    Console.WriteLine("Which Language do you know?");
                    language = Console.ReadLine();
                    Console.WriteLine($"From 1 to 6 how good is your {language}?");
                    level = (Level)Convert.ToInt32(Console.ReadLine());
                    languages.Add(new Language(language, level));
                    Console.WriteLine("Is that all?Type yes if it is");
                    exit = Console.ReadLine().ToLower() == "yes" ? true : false;
                } while (!exit);

                return new(Speciality, School, Score, Experience, Skills, Companies,
                     HasDiploma, github, linkedIn,languages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Console.Clear();
            }
        }      
    }
    public void Print()
    {
       Console.WriteLine($@"Speciality: {Speciality}
School: {School}
Exam Score: {EnteranceScore}");
        if(hasSpecialDiploma)
            Console.WriteLine("Has a Special Degree");
        Console.WriteLine("Skills: ");
        if (Skills is not null)
        {
            foreach (string skill in Skills)
                Console.WriteLine(skill);
        }
        Console.WriteLine("Companies: ");
        if (Companies is not null)
        {
            foreach (string company in Companies)
                Console.WriteLine(company);
        }
        Console.WriteLine($@"Experience: {Experience} months");    
        Console.WriteLine("Languages:");
        if (Languages is not null)
        {
            foreach (Language language in Languages)
                Console.WriteLine(language);
        }
        Console.WriteLine($"Github: { GithubUsername ?? "None"}");
        Console.WriteLine($"LinkedIn: { LinkedInUsername ?? "None"}");
    }
}
