using asp.net_mvc_event_manager.Core.Models;
using System.Collections.Generic;

namespace asp.net_mvc_event_manager.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int eventId, string attendeeId);
        IEnumerable<Attendance> GetFutureAteendances(string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}