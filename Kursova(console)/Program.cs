using System.Text;
using Kursova_console_;

class Program
{
    static List<Game> games = new List<Game>();
    static string filePath = @"C:\Users\User\source\repos\Kursova(console)\Kursova(console)\games.txt";

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        games = GameStorage.LoadFromFile(filePath);

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1. Перегляд всіх ігор");
            Console.WriteLine("2. Додати гру");
            Console.WriteLine("3. Видалити гру");
            Console.WriteLine("4. Редагувати гру");
            Console.WriteLine("5. Сортувати за ціною");
            Console.WriteLine("6. Вибрати ігри за жанром");
            Console.WriteLine("7. Вивести лише ПК-ігри");
            Console.WriteLine("8. Найдешевша гра");
            Console.WriteLine("9. Розрахувати вартість гри зі знижкою");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть дію: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": ShowAll(); break;
                case "2": AddGame(); break;
                case "3": RemoveGame(); break;
                case "4": EditGame(); break;
                case "5": SortByPrice(); break;
                case "6": FilterByGenre(); break;
                case "7": ShowPcGames(); break;
                case "8": ShowCheapest(); break;
                case "9": ShowDiscountedPrice(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Невірна команда."); break;
            }
        }

        GameStorage.SaveToFile(filePath, games);
        Console.WriteLine("Програма завершена.");
    }

    static void ShowAll()
    {
        if (games.Count == 0)
            Console.WriteLine("Немає ігор.");
        else
        {
            int i = 1;
            foreach (var g in games)
                Console.WriteLine($"{i++}. {g}");
        }
    }

    static void AddGame()
    {
        Console.Write("Тип гри (PC/ONLINE): ");
        string type = Console.ReadLine().ToUpper();

        Console.Write("Жанр: ");
        string genre = Console.ReadLine();
        Console.Write("Назва: ");
        string name = Console.ReadLine();
        Console.Write("Розробник: ");
        string dev = Console.ReadLine();
        Console.Write("Платформа: ");
        string plat = Console.ReadLine();
        Console.Write("Ціна: ");
        decimal price = decimal.Parse(Console.ReadLine());

        if (type == "PC")
        {
            games.Add(new PcGame { Genre = genre, Name = name, Developer = dev, Platform = plat, Price = price });
        }
        else if (type == "ONLINE")
        {
            Console.Write("Кількість гравців: ");
            int players = int.Parse(Console.ReadLine());
            games.Add(new OnlineGame { Genre = genre, Name = name, Developer = dev, Platform = plat, Players = players, Price = price });
        }
        else
        {
            Console.WriteLine("Невідомий тип гри.");
        }
    }

    static void RemoveGame()
    {
        ShowAll();
        Console.Write("Введіть номер гри для видалення: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= games.Count)
        {
            games.RemoveAt(index - 1);
            Console.WriteLine("Гру видалено.");
        }
        else
        {
            Console.WriteLine("Некоректний номер.");
        }
    }

    static void EditGame()
    {
        ShowAll();
        Console.Write("Введіть номер гри для редагування: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= games.Count)
        {
            var game = games[index - 1];

            Console.WriteLine("\nРедагування гри:");
            Console.WriteLine("(Залишіть поле порожнім, щоб не змінювати)\n");

            // Редагування жанру
            Console.Write($"Поточний жанр: {game.Genre}\nНовий жанр: ");
            string newGenre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newGenre))
                game.Genre = newGenre;

            // Редагування назви
            Console.Write($"\nПоточна назва: {game.Name}\nНова назва: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                game.Name = newName;

            // Редагування розробника
            Console.Write($"\nПоточний розробник: {game.Developer}\nНовий розробник: ");
            string newDev = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDev))
                game.Developer = newDev;

            // Редагування платформи
            Console.Write($"\nПоточна платформа: {game.Platform}\nНова платформа: ");
            string newPlatform = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPlatform))
                game.Platform = newPlatform;

            // Редагування ціни
            Console.Write($"\nПоточна ціна: {game.Price}\nНова ціна: ");
            string priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice))
                game.Price = newPrice;

            // Для онлайн-ігор - редагування кількості гравців
            if (game is OnlineGame onlineGame)
            {
                Console.Write($"\nПоточна кількість гравців: {onlineGame.Players}\nНова кількість: ");
                string playersInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(playersInput) && int.TryParse(playersInput, out int newPlayers))
                    onlineGame.Players = newPlayers;
            }

            Console.WriteLine("\nІнформацію оновлено!");
        }
        else
        {
            Console.WriteLine("Некоректний номер.");
        }
    }
    static void SortByPrice()
    {
        var sorted = games.OrderBy(g => g.Price).ToList();
        foreach (var g in sorted)
            Console.WriteLine(g);
    }

    static void FilterByGenre()
    {
        Console.Write("Введіть жанр: ");
        string genre = Console.ReadLine().ToLower();
        var filtered = games.Where(g => g.Genre.ToLower() == genre).ToList();

        if (filtered.Count == 0)
            Console.WriteLine("Нічого не знайдено.");
        else
            filtered.ForEach(g => Console.WriteLine(g));
    }

    static void ShowPcGames()
    {
        var pcGames = games.OfType<PcGame>().ToList();

        if (pcGames.Count == 0)
            Console.WriteLine("ПК-ігор не знайдено.");
        else
            pcGames.ForEach(g => Console.WriteLine(g));
    }

    static void ShowCheapest()
    {
        if (games.Count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        var cheapest = games.OrderBy(g => g.Price).First();
        Console.WriteLine("Найдешевша гра:");
        Console.WriteLine(cheapest);
    }

    static void ShowDiscountedPrice()
    {
        ShowAll();
        Console.Write("Введіть номер гри: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= games.Count)
        {
            var game = games[index - 1];
            Console.WriteLine($"Ціна зі знижкою: {game.CalculatePrice()} грн.");
        }
        else
        {
            Console.WriteLine("Некоректний номер.");
        }
    }
}

