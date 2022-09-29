namespace bossaz;
class Filter
{
    public static string[] WorkerFilter = new[]
    {
        "Salary",
        "Age",
        "Minimum Experience",
        "Exit"
    };

    public static string[] EmployerFilter = new[]
    {
        "Experience",
        "Exit"
    };

    

    public static List<Vacancy> VacancyAge(List<Vacancy> vacancies, sbyte age)
    {
        List<Vacancy> filter = new();
        filter.AddRange(vacancies.FindAll(v => v.MaxAge >= age));
        return filter;
    }

    public static List<Vacancy> VacancySalary(List<Vacancy> vacancies, double min)
    {
        List<Vacancy> filter = new();
        filter.AddRange(vacancies.FindAll(v => v.Salary >= min));
        return filter;
    }

    public static List<Vacancy> VacancyExperience(List<Vacancy> vacancies, sbyte experience)
    {
        List<Vacancy> filter = new();
        filter.AddRange(vacancies.FindAll(v => v.MinExperience <= experience));
        return filter;
    }
   
    public static List<CV> CVExperience(List<CV> workers, sbyte experience) => workers.FindAll(cv => cv.Experience >= experience);

}
