using TimeTracker.Shared.Entities;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.API.Services
{
    public interface ITimeEntryService
    {
        List<TimeEntryResponse> GetAllTimeEntries();
        List<TimeEntryResponse> CreateTimeEntry(TimeEntryCreateRequest timeEntry);
    }
}
