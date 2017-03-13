using asp.net_mvc_event_manager.Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace asp.net_mvc_event_manager.Tests.Domain.Models
{
    [TestClass]
    public class NotificationTests
    {
        [TestMethod]
        public void EventCanceled_WhenCalled_ShouldReturnedANotificationForCanceledEvent()
        {
            var newEvent = new Event();
            var notification = Notification.EventCanceled(newEvent);

            // Again, here, we have two assertions, but that doesn't mean we're
            // violating the single responsibility principle. We're verifying 
            // one logical fact: that upon calling Notification.EventCanceled()
            // we'll get a notification object for the canceled event. This notification
            // object should be in the right state, meaning its type should be
            // EventCanceled and its event should be the event for each we created 
            // the notification. 

            notification.Type.Should().Be(NotificationType.EventCanceled);
            notification.Event.Should().Be(newEvent);
        }
    }
}
