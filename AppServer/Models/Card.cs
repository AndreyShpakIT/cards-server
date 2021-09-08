namespace AppServer.Models
{
    public class Card
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}