var myEvents = myEvents || {};
myEvents.controls = myEvents.controls || {};
myEvents.controls.Share = function () {
    var self = this;

    self.onLinkedin = function (linkedinElement, url, title, summary) {
        var linkedinLink = "http://www.linkedin.com/shareArticle?mini=true" +
            "&url=" + encodeURI(url) +
            "&title=" + escape(title) +
            "&summary=" + escape(summary) +
            "&source=myevents";
        
        var linkedinOnclick = getWindowOpenSentence(600, 450, linkedinLink);
        linkedinElement.attr('onclick', linkedinOnclick);
    };
    
    self.onTwitter = function (twitterElement, text, account) {
        var twitterLink = "https://twitter.com/share?text=" + text;
        if (account) {
            twitterLink = twitterLink + "&via=" + account;
        }   
        var twitterOnclick = getWindowOpenSentence(600, 350, twitterLink);
        twitterElement.attr('onclick', twitterOnclick);
    };
    
    self.onFacebook = function (facebookElement, url) {
        var facebookLink = "http://www.facebook.com/sharer.php?u=" + encodeURI(url);
        var facebookOnclick = getWindowOpenSentence(600, 350, facebookLink);
        facebookElement.attr('onclick', facebookOnclick);
    };
    
    function getWindowOpenSentence(width, height, url) {
        var centerWidth = (window.screen.width - width) / 2;
        var centerHeight = (window.screen.height - height) / 2;
        
        return "window.open('" + url + "','', 'toolbar=no, status=no," +
            " width=" + width +
            ", height=" + height +
            ", left=" + centerWidth +
            ", top=" + centerHeight +
            "');";
    }

    return this;
};
