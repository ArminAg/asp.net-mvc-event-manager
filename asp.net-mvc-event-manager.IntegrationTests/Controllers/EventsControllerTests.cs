using asp.net_mvc_event_manager.Controllers;
using asp.net_mvc_event_manager.Core.Models;
using asp.net_mvc_event_manager.Core.ViewModels;
using asp.net_mvc_event_manager.IntegrationTests.Extensions;
using asp.net_mvc_event_manager.Persistence;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace asp.net_mvc_event_manager.IntegrationTests.Controllers
{
    [TestFixture]
    public class EventsControllerTests
    {
        private EventsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new EventsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingEvents()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var newEvent = new Event { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Events.Add(newEvent);
            _context.SaveChanges();

            // Act
            var result = _controller.Mine();

            // Assert
            (result.ViewData.Model as IEnumerable<Event>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenEvent()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var newEvent = new Event { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Events.Add(newEvent);
            _context.SaveChanges();

            // Act
            var result = _controller.Update(new EventFormViewModel
            {
                Id = newEvent.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                GenreId = 2
            });

            // Assert
            _context.Entry(newEvent).Reload();
            newEvent.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            newEvent.Venue.Should().Be("Venue");
            newEvent.GenreId.Should().Be(2);
        }
    }
}
