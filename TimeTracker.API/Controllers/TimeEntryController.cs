﻿using Microsoft.AspNetCore.Mvc;


namespace TimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;
        public TimeEntryController(ITimeEntryService timeEntryService) 
        {
            _timeEntryService = timeEntryService;
        }
       
        [HttpGet]
        public ActionResult<List<TimeEntryResponse>> GetAllTimeEntries()
        {
           return Ok(_timeEntryService.GetAllTimeEntries());
        }

        [HttpGet("{id}")]
        public ActionResult<TimeEntryResponse> GetAllTimeEntryById(int id)
        {
            var result = _timeEntryService.GetTimeEntryById(id);
            if(result == null)
            {
                return NotFound("Time entry with given ID not found!");
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<List<TimeEntryResponse>>CreateTimeEntry(TimeEntryCreateRequest timeEntry)
        {
            return Ok(_timeEntryService.CreateTimeEntry(timeEntry));
        }

        [HttpPut("{id}")]
        public ActionResult<List<TimeEntryResponse>>UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry)
        {
            var result = _timeEntryService.UpdateTimeEntry(id, timeEntry);
            if(result is null)
            {
                return NotFound("TimeEntry with given ID was not found.");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<TimeEntryResponse>> DeleteTimeEntry(int id) 
        {
            var result = _timeEntryService.DeleteTimeEntry(id);
            if (result is null)
            {
                return NotFound("TimeEntry with given ID was not found.");
            }
            return Ok(result);
        }

    }
}
