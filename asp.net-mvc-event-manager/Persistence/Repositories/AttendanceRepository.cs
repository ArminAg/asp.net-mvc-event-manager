using asp.net_mvc_event_manager.Core.Models;
using asp.net_mvc_event_manager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace asp.net_mvc_event_manager.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAteendances(string userId)
        {
            return _context.Attendances
                            .Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
                            .ToList();
        }

        public Attendance GetAttendance(int eventId, string attendeeId)
        {
            return _context.Attendances
                    .SingleOrDefault(a => a.EventId == eventId && a.AttendeeId == attendeeId);
        }
    }
}