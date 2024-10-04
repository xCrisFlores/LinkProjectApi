using Microsoft.EntityFrameworkCore;
using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public class StudentsService
    {
        private readonly LinkProjectBddContext _context;

        public StudentsService(LinkProjectBddContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> FindAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> FindOne(int Code)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.StudentCode == Code);
        }

         public async Task<int> Insert(Student student)
        {
            _context.Students.Add(student);
            try{
                await _context.SaveChangesAsync();
                return 1;
            }catch(Exception){
                return 0;
            }
            
        }

       public async Task Update(int Code, Student student)
        {
            var toUpdate = await _context.Students.FindAsync(Code);
            if (toUpdate != null)
            {
            
                toUpdate.Phone = student.Phone;
                toUpdate.Status = student.Status;
                toUpdate.Lab = student.Lab;
                toUpdate.Biography = student.Biography;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Person not found");
            }
        }


        public async Task Delete(int Code)
        {
            var person = await _context.Students.FindAsync(Code);
            if (person != null)
            {
                _context.Students.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
        

    }
    
}