using asp.net_mvc_event_manager.Core.Models;
using asp.net_mvc_event_manager.Persistence;
using asp.net_mvc_event_manager.Persistence.Repositories;
using asp.net_mvc_event_manager.Tests.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace asp.net_mvc_event_manager.Tests.Persistence.Repositories
{
    [TestClass]
    public class EventRepositoryTests
    {
        private EventRepository _repository;
        private Mock<DbSet<Event>> _mockEvents;
        private Mock<DbSet<Attendance>> _mockAttendances;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockEvents = new Mock<DbSet<Event>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Events).Returns(_mockEvents.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);

            _repository = new EventRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingEventsByArtist_EventIsInThePast_ShouldNotBeReturned()
        {
            var pastEvent = new Event() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockEvents.SetSource(new[] { pastEvent });

            var events = _repository.GetUpcomingEventsByArtist("1");
            events.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingEventsByArtist_EventIsCanceled_ShouldNotBeReturned()
        {
            var futureEvent = new Event() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            futureEvent.Cancel();

            _mockEvents.SetSource(new[] { futureEvent });

            var events = _repository.GetUpcomingEventsByArtist("1");
            events.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingEventsByArtist_EventIsForADifferentArtist_ShouldNotBeReturned()
        {
            var futureEvent = new Event() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockEvents.SetSource(new[] { futureEvent });

            var events = _repository.GetUpcomingEventsByArtist(futureEvent.ArtistId + "-");
            events.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingEventsByArtist_EventIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var futureEvent = new Event() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockEvents.SetSource(new[] { futureEvent });

            var events = _repository.GetUpcomingEventsByArtist(futureEvent.ArtistId);
            events.Should().Contain(futureEvent);
        }

        [TestMethod]
        public void GetEventsUserAttending_EventIsInThePast_ShouldNotBeReturned()
        {
            var pastEvent = new Event() { DateTime = DateTime.Now.AddDays(-1) };
            var attendance = new Attendance { Event = pastEvent, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var events = _repository.GetEventsUserAttending(attendance.AttendeeId);
            events.Should().BeEmpty();
        }

        [TestMethod]
        public void GetEventsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            var futureEvent = new Event() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Event = futureEvent, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var events = _repository.GetEventsUserAttending(attendance.AttendeeId + "-");
            events.Should().BeEmpty();
        }

        [TestMethod]
        public void GetEventsUserAttending_UpcomingEventUserAttending_ShouldBeReturned()
        {
            var futureEvent = new Event() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Event = futureEvent, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var events = _repository.GetEventsUserAttending(attendance.AttendeeId);
            events.Should().Contain(futureEvent);
        }
    }
}
