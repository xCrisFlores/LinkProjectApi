using Microsoft.EntityFrameworkCore;
using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public class AdvisersService
    {
        private readonly LinkProjectBddContext _context;

        public AdvisersService(LinkProjectBddContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Adviser>> FindAll()
        {
            return await _context.Advisers.ToListAsync();
        }

        public async Task<Adviser> FindOne(int Code)
        {
            return await _context.Advisers.FirstOrDefaultAsync(x => x.AdviserCode == Code);
        }

        public async Task<int> Insert(Adviser adviser)
        {
            _context.Advisers.Add(adviser);
            try{
                await _context.SaveChangesAsync();
                return 1;
            }catch(Exception){
                return 0;
            }
            
        }

       public async Task Update(int Code, Adviser adviser)
        {
            var toUpdate = await _context.Advisers.FindAsync(Code);
            if (toUpdate != null)
            {
            
                toUpdate.Division = adviser.Division;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Person not found");
            }
        }


        public async Task Delete(int Code)
        {
            var person = await _context.Advisers.FindAsync(Code);
            if (person != null)
            {
                _context.Advisers.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
        

    }
    
}