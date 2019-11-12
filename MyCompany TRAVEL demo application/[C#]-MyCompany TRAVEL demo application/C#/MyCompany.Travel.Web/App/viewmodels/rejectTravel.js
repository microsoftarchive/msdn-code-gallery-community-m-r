define(['services/model'], function (model) {

    var travelEntity = ko.observable(),
        message = ko.observable(),
        reason = ko.observable("");

    var vm = {
        activate: activate,
        travelEntity: travelEntity,
        message: message,
        reason: reason,
        accept: accept,
        viewAttached: viewAttached,
        close: close
    };

    return vm;

    function activate(routeData) {
        if (routeData && routeData.travelRequest) {
            message('Write a reason to reject "' + routeData.travelRequest.name() + '".');
            reason("");
            travelEntity(routeData.travelRequest);
        }
        return true;
    };

    function viewAttached(view) {
        return true;
    }

    function close() {
        vm.modal.close();
        return true;
    }

    function accept() {
        travelEntity().comments(reason());
        vm.modal.close(travelEntity);
        return true;
    }
});
