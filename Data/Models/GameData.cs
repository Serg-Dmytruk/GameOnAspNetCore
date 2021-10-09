namespace Data.Models
{
    public class GameData
    {
        public int Id { get; set; }
        public int UserDataId { get; set; }
        public UserData UserData { get; set; }
        public int? Score { get; set; }
        public int TotalGamesCount { get; set; }
    }
}
