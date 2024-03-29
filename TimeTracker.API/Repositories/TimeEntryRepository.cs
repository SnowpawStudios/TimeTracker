﻿



using Microsoft.EntityFrameworkCore;

namespace TimeTracker.API.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly DataContext _context;

        public TimeEntryRepository(DataContext context)
        {
            _context = context;
        }

        
        public async Task<List<TimeEntry>> CreateTimeEntry(TimeEntry timeEntry)
        {
            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }

        public async Task<List<TimeEntry>?> DeleteTimeEntry(int id)
        {
            var dbTimeEntry = await _context.TimeEntries.FindAsync(id);
            if(dbTimeEntry == null)
            {
                return null;
            }
            _context.TimeEntries.Remove(dbTimeEntry);
            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }

        public async Task<List<TimeEntry>> GetAllTimeEntries()
        {
            return await _context.TimeEntries.Include(te => te.Project).ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesByProject(int projectId)
        {
            return await _context.TimeEntries.Where(t=>t.ProjectId == projectId).ToListAsync();
        }

        public async Task<TimeEntry?> GetTimeEntryById(int id)
        {
            var timeEntry = await _context.TimeEntries.Include(te => te.Project).FirstOrDefaultAsync(te => te.Id == id);

            return timeEntry;
        }

        public async Task<List<TimeEntry>> UpdateTimeEntry(int id, TimeEntry timeEntry)
        {
            var dbTimeEntry = await _context.TimeEntries.FindAsync(id);
            if(dbTimeEntry == null)
            {
                throw new EntityNotFoundException($"Entity with ID {id} was not found");
            }

            dbTimeEntry.ProjectId = timeEntry.ProjectId;
            dbTimeEntry.Start = timeEntry.Start;
            dbTimeEntry.End = timeEntry.End;
            dbTimeEntry.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }
    }
}
