
var EventService = function () {

    var cancelEvent = function (eventId, url, done, fail) {
        $.ajax({
            url: url + "/" + eventId,
            method: "DELETE"
        })
        .done(done)
        .fail(fail);
    }

    return {
        cancelEvent: cancelEvent
    }
}();