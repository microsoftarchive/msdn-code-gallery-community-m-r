
function onSourceDownloadProgressChanged(sender, eventArgs) {
    sender.findName("uxStatus").Text =
        "Loading: " + Math.round((eventArgs.progress * 1000)) / 10 + "%";
    sender.findName("uxProgressBar").ScaleY = eventArgs.progress * 356;
}
