using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public interface ISkillService
    {
        Task<IEnumerable<Skill>> FindAll();
        Task<Skill> FindOne(int Code);
        Task<int> Insert(Skill skill);
        Task Update(int Code, Skill skill);
        Task Delete(int Code);
        
    }

    
}