using Microsoft.EntityFrameworkCore;
using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public class UsersService
    {
        private readonly LinkProjectBddContext _context;

        public UsersService(LinkProjectBddContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> FindOne(int Code)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Code == Code);
        }

        public async Task<int> Insert(User user)
        {
            _context.Users.Add(user);
            try{
                await _context.SaveChangesAsync();
                return 1;
            }catch(Exception){
                return 0;
            }
            
        }

       public async Task Update(int Code, User user)
        {
            var toUpdate = await _context.Users.FindAsync(Code);
            if (toUpdate != null)
            {
            
                toUpdate.Email = user.Email;
                toUpdate.Password = user.Password;
                toUpdate.Name = user.Name;
                toUpdate.Path = user.Path;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Person not found");
            }
        }


        public async Task Delete(int Code)
        {
            var person = await _context.Users.FindAsync(Code);
            if (person != null)
            {
                _context.Users.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

       public async Task<int?> Login(string Email, string pass)
        {
            var person = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email && x.Password == pass);

            if (person != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
    
}