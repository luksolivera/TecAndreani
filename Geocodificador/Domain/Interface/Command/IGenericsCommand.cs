using System.Threading.Tasks;

namespace Domain.Interface.Command
{
    public interface IGenericsCommand
    {
        Task SaveChangeAsync();
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<T> Find<T>(object id) where T : class;
    }
}
