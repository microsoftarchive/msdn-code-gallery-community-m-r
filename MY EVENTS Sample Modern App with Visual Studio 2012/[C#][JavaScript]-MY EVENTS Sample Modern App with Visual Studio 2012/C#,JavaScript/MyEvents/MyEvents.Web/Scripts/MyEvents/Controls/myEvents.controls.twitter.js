
(function ($) {
    
    $.fn.showTweets = function (account, numberOfTweets) {
        var self = this;

        $(document).ready(function () {

            var onGetTweetsComplete = function (data) {
                if (data && data.results && data.results.length > 0) {
                    for (var i = 0; i < data.results.length; i++) {
                        var tweet = "<div class='tweet'>";
                        tweet = tweet + "<div id='account' class='half account'>";
                        tweet = tweet + '<span>' + account + '</span>';
                        tweet = tweet + '</div>';
                        tweet = tweet + "<div id='time-ago' class='half time-ago'>";
                        var timeAgo = moment(data.results[i].created_at).fromNow();
                        tweet = tweet + '<span>' + timeAgo + '</span>';
                        tweet = tweet + '</div>';
                        tweet = tweet + '<span>' + data.results[i].text + '</span>';
                        tweet = tweet + '</div>';
                        
                        self.append(tweet);
                    }
                }
                else {
                    self.append('<p>there are no available tweets at this moment</p>');
                }
            };

            if (!myEvents.app.settings.isOffline) {
                $.getJSON('http://search.twitter.com/search.json?from=@' + account + '&rpp=' + numberOfTweets + '&result_type=recent&callback=?', {}, onGetTweetsComplete);

            } else {
                onGetTweetsComplete({});
            }

            return this;
        });
    }
})(jQuery);