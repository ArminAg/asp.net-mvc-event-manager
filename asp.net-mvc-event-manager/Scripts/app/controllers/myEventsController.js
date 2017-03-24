
var MyEventsController = function (eventService) {
    var url = "/api/events";
    var link;

    var init = function (selector) {
        $(selector).click(cancelEvent);
    }

    var cancelEvent = function (e) {
        link = $(e.target);

        bootbox.confirm({
            message: "Are you sure you want to cancel this event?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-default'
                }
            },
            callback: bootboxCallback
        });
    }

    var bootboxCallback = function (result) {
        var eventId = link.attr("data-event-id");
        eventService.cancelEvent(eventId, url, done, fail);
    }

    var done = function () {
        link.parents("li").fadeOut(function () {
            $(this).remove();
        });
    }

    var fail = function () {
        alert("Something failed!");
    }

    return {
        init: init
    }
}(EventService);