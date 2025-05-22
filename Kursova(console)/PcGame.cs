namespace Kursova_console_
{
    public class PcGame : Game
    {
        public override decimal CalculatePrice()
        {
            return Price > 1000 ? Price * 0.95m : Price;
        }

        public override string ToString() =>
            $"[PC] {Name} ({Genre}) - {Developer}, {Platform}, {Price} грн.";
    }

}
