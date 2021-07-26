namespace GameLibrary.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public string ClientId { get; set; }
        public Client Client { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

    }
}
