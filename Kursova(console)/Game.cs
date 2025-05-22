namespace Kursova_console_
{
    public abstract class Game
    {
        public string Genre { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Platform { get; set; }
        public decimal Price { get; set; }

        public abstract decimal CalculatePrice();
    }

}
