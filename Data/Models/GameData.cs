using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("GameData")]
    public class GameData
    {
        public int Id { get; set; }
        public int UserDataId { get; set; }
        public UserData UserData { get; set; }
        public int? Score { get; set; }
        public int TotalGamesCount { get; set; }
    }
}
