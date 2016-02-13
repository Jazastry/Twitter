$(function () {
//    $.getScript("~/signalr/hubs")
//           .done(function (script, textStatus) {
//
//
//           });

    $.connection.hub.start().done(function() {
        console.log("Connection is ready.");
    });

    //HHEELLOOOOOO

    var tweeter = $.connection.tweeterHub;

    // Register client functions called from server
    tweeter.client.showTweet = showTweet;

    function showTweet(tweetId) {
        console.log(tweetId);
        var tweetPartial = $('<div>').load('/Tweets/GetPartialTweet/' + tweetId);
        console.log(tweetPartial);
        $('.tweets').prepend(tweetPartial);
    }
}())
