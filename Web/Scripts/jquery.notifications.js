(function ($) {
    $.notifications = function (options) {

        var settings = $.extend({ notify: null }, options);

        var notificationHub = $.connection.notificationHub;

        notificationHub.notify = function (data) {
            $(data).each(function (index, item) {
                if (settings.notify) {
                    settings.notify(item);
                }
                notificationHub.remove(item.Id);
            });
        };

        $.connection.hub.start()
            .done(function () {
                notificationHub.userId = "wewillneedtoencryptthis";
                notificationHub.connected();
            })
            .fail(function () {
                alert("Could not Connect!");
            });
    };
})(jQuery);