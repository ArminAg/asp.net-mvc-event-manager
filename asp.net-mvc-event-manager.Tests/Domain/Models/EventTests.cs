using asp.net_mvc_event_manager.Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace asp.net_mvc_event_manager.Tests.Domain.Models
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var newEvent = new Event();

            newEvent.Cancel();

            newEvent.IsCanceled.Should().BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveNotification()
        {
            var newEvent = new Event();
            newEvent.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" } });

            newEvent.Cancel();

            // TODO: This could be pushed into the Event class (eg event.GetAttendees())
            var attendees = newEvent.Attendances.Select(a => a.Attendee).ToList();
            attendees[0].UserNotifications.Count.Should().Be(1);
        }
    }
}
