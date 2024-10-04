using Microsoft.EntityFrameworkCore;
using API.Models;
using Task = System.Threading.Tasks.Task;

namespace API.Services
{
    public class ScheduleServices
    {
        private readonly LinkProjectBddContext _context;

        public ScheduleServices(LinkProjectBddContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schedule>> FindAll(int ScheduleId)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(x => x.StudentCode == ScheduleId);
            if(schedule !=  null){
                return await _context.Schedules.Where(d => d.StudentCode == schedule.StudentCode).ToListAsync();
            }else{
                throw new Exception("Schedule not found");
            }
            
        }

        public async Task<int> Insert(Schedule day)
        {
            _context.Schedules.Add(day);
            try{
                await _context.SaveChangesAsync();
                return 1;
            }catch(Exception){
                return 0;
            }
            
        }

       public async Task Update(int Code, string day1, Schedule day)
        {
            var toUpdate = await _context.Schedules.FirstOrDefaultAsync(x => x.StudentCode == Code && x.Day == day1);
            if (toUpdate != null)
            {
                toUpdate.StartTime = day.StartTime;
                toUpdate.EndTime = day.EndTime;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Person not found");
            }
        }

    }
    
}