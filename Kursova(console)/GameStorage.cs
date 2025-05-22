namespace Kursova_console_
{

    public static class GameStorage
    {
        public static List<Game> LoadFromFile(string path)
        {
            var games = new List<Game>();
            if (!File.Exists(path)) return games;

            foreach (var line in File.ReadAllLines(path))
            {
                var parts = line.Split(';');
                if (parts[0] == "PC")
                {
                    games.Add(new PcGame
                    {
                        Genre = parts[1],
                        Name = parts[2],
                        Developer = parts[3],
                        Platform = parts[4],
                        Price = decimal.Parse(parts[5])
                    });
                }
                else if (parts[0] == "ONLINE")
                {
                    games.Add(new OnlineGame
                    {
                        Genre = parts[1],
                        Name = parts[2],
                        Developer = parts[3],
                        Platform = parts[4],
                        Players = int.Parse(parts[5]),
                        Price = decimal.Parse(parts[6])
                    });
                }
            }
            return games;
        }

        public static void SaveToFile(string path, List<Game> games)
        {
            var lines = new List<string>();
            foreach (var game in games)
            {
                if (game is PcGame pc)
                {
                    lines.Add($"PC;{pc.Genre};{pc.Name};{pc.Developer};{pc.Platform};{pc.Price}");
                }
                else if (game is OnlineGame online)
                {
                    lines.Add($"ONLINE;{online.Genre};{online.Name};{online.Developer};{online.Platform};{online.Players};{online.Price}");
                }
            }
            File.WriteAllLines(path, lines);
        }
    }

}
