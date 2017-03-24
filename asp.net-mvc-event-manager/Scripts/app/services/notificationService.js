
var NotificationService = function () {

    var getNotifications = function (url, handleNotifications) {
        $.getJSON(url, handleNotifications);
    }

    var markAsRead = function (url, done) {
        $.post(url + "/markAsRead")
            .done(done);
    }

    return {
        getNotifications: getNotifications,
        markAsRead: markAsRead
    }
}();