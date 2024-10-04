using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public interface IAdviserService
    {
        Task<IEnumerable<Adviser>> FindAll();
        Task<Adviser> FindOne(int Code);
        Task<int> Insert(Adviser user);
        Task Update(int Code,Adviser user);
        Task Delete(int Code);
        
    }

    
}