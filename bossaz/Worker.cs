namespace bossaz;

class Worker : Person
{
    public CV? Cv { get; set; }
    public Worker(string name, string surname, string city, string phone, sbyte age, CV? cv = null) 
        : base(name,surname,city,phone,age)
    {
        Cv = cv;
    }

    public static Worker AddWorker()
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
                return new(name , surname, city, phone, age);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                continue;
            }
        }
    }



    public void Print()
    {
        Console.WriteLine(base.ToString());
        Cv!.Print();
    }
}
