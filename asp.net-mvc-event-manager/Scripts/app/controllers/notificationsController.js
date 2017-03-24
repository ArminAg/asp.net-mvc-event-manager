
var NotificationsController = function (notificationService) {
    var url = "/api/notifications";
    var notificationsCount;
    var notificationsContainer;
    var notifications;

    var init = function (notificationCountSelector, notificationsSelector) {
        notificationsCount = $(notificationCountSelector);
        notificationsContainer = $(notificationsSelector);
        notificationService.getNotifications(url, handleNotifications)
    }

    var handleNotifications = function (notifications) {
        //notifications = notifications;
        if (notifications.length == 0)
            return;

        notificationsCount
            .text(notifications.length)
            .removeClass("hide")
            .addClass("animated bounceInDown");

        notificationsContainer.popover({
            html: true,
            title: "Notifications",
            content: function () {
                var compiled = _.template($("#notifications-template").html());
                return compiled({ notifications: notifications });
            },
            placement: "bottom",
            template: '<div class="popover popover-notifications" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
        }).on("shown.bs.popover", markAsRead);
    }

    var markAsRead = function () {
        notificationService.markAsRead(url, done);
    }

    var done = function () {
        notificationsCount.text("").addClass("hide");
    }

    return {
        init: init
    }
}(NotificationService);