namespace Mythic.WPF.Printing
{
    public class GenerateRequest
    {
        private GenerateRequest(int chaos, int numberOfMythicEvents, int numberOfMythicFates)
        {
            Chaos = chaos;
            NumberOfMythicEvents = numberOfMythicEvents;
            NumberOfMythicFates = numberOfMythicFates;
        }

        public int Chaos { get; }
        public int NumberOfMythicEvents { get; }
        public int NumberOfMythicFates { get; }

        public static GenerateRequest Factory()
        {
            return new GenerateRequest(5, 15, 15);
        }
    }
}