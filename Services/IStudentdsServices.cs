using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> FindAll();
        Task<Student> FindOne(int Code);
        Task<int> Insert(Student user);
        Task Update(int Code, Student user);
        Task Delete(int Code);
        
    }

    
}