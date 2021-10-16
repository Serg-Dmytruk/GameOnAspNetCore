using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GameServer.Common.Services.TableService
{
   public  class TableService : ITableService
    {
        private Dictionary<string, int> _tables { get; set; } = new();

        public async Task<IEnumerable<string>> GetAwailableTables()
        {
            return await Task.Run(() => _tables.Where(x => x.Value < 2).Select(x => x.Key).ToList());
        }
        public void AddTable(string tableId, int playerCount)
        {
            _tables.Add(tableId, playerCount);
        }
    }
}
