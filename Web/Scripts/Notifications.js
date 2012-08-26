function Notifications(options) {
    var notificationHub = $.connection.notificationHub;

    notificationHub.notify = function (data) {
        $(data).each(function (index, item) {
            $('#messages').append('<li>' + item.Message + '</li>');
            notificationHub.remove(item.Id);
        });
    };

    $.connection.hub.start()
        .done(function () {
        })
        .fail(function () {
            alert("Could not Connect!");
        });

}