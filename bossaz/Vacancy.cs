using System.Text.Json.Serialization;

namespace bossaz;
class Vacancy
{
    public string Name { get; }
    public double Salary { get; }
    public double MinExperience { get; }
    public double MaxAge { get; }
    public double WorkPeriod { get; }

    [JsonConstructor]
    public Vacancy() { }

    public Vacancy(string name, double salary, 
        double minExperience, double maxAge, double workPeriod)
    {
        Name = name;
        Salary = salary;
        MinExperience = minExperience;
        MaxAge = maxAge;
        WorkPeriod = workPeriod;
    }


    public override string ToString()
    {
        return $@"Job: {Name}
Salary: {Salary}
Minimum Experience: {MinExperience}
Maximum Age: {MaxAge}
Contract for {WorkPeriod} months";
    }
}
