using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface.Queries
{
    public interface IGenericsQuery
    {
        Task<T> GetById<T>(string table, string column, string id) where T : class;
        Task<List<T>> GetAll<T>(string table) where T : class;
        Task<bool> Exist(string table, string column, string[] values = null);
    }
}
