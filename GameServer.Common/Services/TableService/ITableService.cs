using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameServer.Common.Services.TableService
{
    public interface ITableService 
    {
        Task<IEnumerable<string>> GetAwailableTables();
        void AddTable(string tableId, int playerCount);
        void DeleteTable(string tableId);
    }
}
