﻿namespace Data.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public UserData UserInfo {get; set;}
    }
}
