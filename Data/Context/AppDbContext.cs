using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserData> UserDatas {get; set;}
        public DbSet<GameData> GameDatas {get; set;}
    }
}
