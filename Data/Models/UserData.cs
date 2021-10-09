using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("UserData")]
    public class UserData
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public GameData UserInfo { get; set; }
    }
}
