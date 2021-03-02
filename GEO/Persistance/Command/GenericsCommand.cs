using Domain.Interface.Command;
using Persistance;
using System.Threading.Tasks;

namespace Persistence.Command
{
    public class GenericsCommand : IGenericsCommand
    {
        private readonly ApplicationDbContext _context;

        public GenericsCommand(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<T> Find<T>(object id) where T : class
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}
