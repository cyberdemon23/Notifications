function Notifications(options) {
    var self = this;

    self.notify = options.notify;
    
    var notificationHub = $.connection.notificationHub;

    notificationHub.notify = function (data) {
        $(data).each(function (index, item) {
            if (self.notify) {
                self.notify(item);
            }
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