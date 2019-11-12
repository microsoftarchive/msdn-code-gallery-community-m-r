function connect(token, clientId, callbacks) {
    $.connection.hub.stop();
    $.connection.hub.qs = { 'Token': token, 'ClientId': clientId };
    $.connection.hub.url = signalrRoot + '/signalr';    

    var pulseHub = $.connection.pulseHub;

    if (pulseHub !== null && pulseHub !== undefined) {
        for (var i = 0; i < callbacks.length; i++) {
            for (var method in callbacks[i]) {
                pulseHub.client[method] = callbacks[i][method];                
            }            
        }        
    }

    $.connection.hub.start({ transport: 'longPolling' });
}