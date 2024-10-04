using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> FindAll();
        Task<User> FindOne(int Code);
        Task<int> Insert(User user);
        Task Update(int Code,User user);
        Task Delete(int Code);
        Task<int> Login(string Email, string pass);
    }

    
}