using System;

namespace Ships
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var test = new ShipsContext())
            {
                while (true)
                {
                    Console.WriteLine("Выберите число от 1 до 7");
                    var key = Console.ReadKey();
                    Console.WriteLine();
                    if (key.Key == ConsoleKey.NumPad1)
                    {
                        test.CreateClassOfShip("Biba", "bbc", "Kazakhstan", 10, 13, 50000);
                    }
                    else if (key.Key == ConsoleKey.NumPad2)
                    {
                        test.CreateShip("Aboba", "Biba", 1940, "bbc", "Kazakhstan", 10, 13, 50000);
                    }
                    else if (key.Key == ConsoleKey.NumPad3)
                    {
                        test.CreateBattle("North Kazakhstan", Convert.ToDateTime("1941-05-25 00:00:00.000"), "Aboba", "OK");
                    }
                    else if (key.Key == ConsoleKey.NumPad4)
                    {
                        test.ShowShips();
                    }
                    else if (key.Key == ConsoleKey.NumPad5)
                    {
                        test.ShowResultOfBattleByName("Bismarck");
                    }
                    else if (key.Key == ConsoleKey.NumPad6)
                    {
                        test.ShowBattleByName("North Atlantic");
                    }
                    else if (key.Key == ConsoleKey.NumPad7)
                    {
                        test.ShowAllBattlesByDate(Convert.ToDateTime("1941-05-25 00:00:00.000"));
                    }
                    else
                    {
                        Console.WriteLine("Error, incorrect number");
                        Console.ReadKey();
                    }
                    Console.Clear();
                }
            }

        }
    }
}
