namespace Kursova_console_
{
    public class OnlineGame : Game
    {
        public int Players { get; set; }

        public override decimal CalculatePrice()
        {
            return Price > 500 ? Price * 0.90m : Price;
        }

        public override string ToString() =>
            $"[ONLINE] {Name} ({Genre}) - {Developer}, {Platform}, {Players} гравців, {Price} грн.";
    }

}
