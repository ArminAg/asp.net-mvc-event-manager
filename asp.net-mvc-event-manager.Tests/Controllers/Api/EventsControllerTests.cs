using asp.net_mvc_event_manager.Controllers.Api;
using asp.net_mvc_event_manager.Core;
using asp.net_mvc_event_manager.Core.Models;
using asp.net_mvc_event_manager.Core.Repositories;
using asp.net_mvc_event_manager.Tests.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace asp.net_mvc_event_manager.Tests.Controllers.Api
{
    [TestClass]
    public class EventsControllerTests
    {
        private EventsController _controller;
        private Mock<IEventRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IEventRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Events).Returns(_mockRepository.Object);

            _controller = new EventsController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoEventWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_EventIsCanceled_ShouldReturnNotFound()
        {
            var newEvent = new Event();
            newEvent.Cancel();

            _mockRepository.Setup(r => r.GetEventWithAttendees(1)).Returns(newEvent);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersEvent_ShouldReturnUnauthorized()
        {
            var currentEvent = new Event { ArtistId = _userId + "-" };
            _mockRepository.Setup(r => r.GetEventWithAttendees(1)).Returns(currentEvent);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var currentEvent = new Event { ArtistId = _userId };
            _mockRepository.Setup(r => r.GetEventWithAttendees(1)).Returns(currentEvent);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
