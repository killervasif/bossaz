namespace bossaz;

class Menu
{
    public static void Show(string[] choices, int index)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            if (index == i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(choices[i]);
                Console.ResetColor();
                continue;
            }
            Console.WriteLine(choices[i]);
        }
    }

    public static void Cursor(ConsoleKeyInfo key, int max, ref int index)
    {
        if (key.Key == ConsoleKey.UpArrow)
            index--;
        else if (key.Key == ConsoleKey.DownArrow)
            index++;

        if (index < 0) index = 0;
        else if (index >= max) index = max - 1;
    }
}
