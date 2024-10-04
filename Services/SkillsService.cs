using Microsoft.EntityFrameworkCore;
using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public class SkillsService
    {
        private readonly LinkProjectBddContext _context;

        public SkillsService(LinkProjectBddContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Skill>> FindAll()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Skill> FindOne(int Code)
        {
            return await _context.Skills.FirstOrDefaultAsync(x => x.StudentCode == Code);
        }

         public async Task<int> Insert(Skill skill)
        {
            _context.Skills.Add(skill);
            try{
                await _context.SaveChangesAsync();
                return 1;
            }catch(Exception){
                return 0;
            }
            
        }

       public async Task Update(int Code, Skill skill)
        {
            var toUpdate = await _context.Skills.FindAsync(Code);
            if (toUpdate != null)
            {
            
                toUpdate.Skill1 = skill.Skill1;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Person not found");
            }
        }


        public async Task Delete(int Code)
        {
            var skill = await _context.Skills.FindAsync(Code);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }
        

    }
    
}