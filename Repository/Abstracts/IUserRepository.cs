using ArchiveApp.Models;

namespace ArchiveApp.Repository.Abstracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> UserAsync(string email, string password);
        Task AddAsync(User user);
        Task UpdateAsync(int id, User user);
        Task DeleteAsync(int id);
    }
}
