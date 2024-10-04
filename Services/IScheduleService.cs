using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> FindAll(int ScheduleId);
        Task<int> Insert(Schedule day);
        Task Update(int Code, string day1, Schedule day);
        
    }

    
}