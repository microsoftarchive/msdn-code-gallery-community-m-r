define(['services/model', 'services/dataservice', 'services/enums', 'services/navigation', 'durandal/app', 'viewmodels/base', 'services/context'],
    function (model, dataservice, enums, navigation, app, base, context) {
     var mode;

     var travelEntity = ko.observable(),
         isManagerMode = ko.observable(false),
         isRRHHMode = ko.observable(false),
         isUserMode = ko.computed({
             read: function () {
                 return !isRRHHMode() && !isManagerMode()
             }
         });


    var vm = {
        activate: activate,
        travelEntity: travelEntity,
        back: back,
        isOneWay: isOneWay,
        viewAttached: viewAttached,
        isManagerMode: isManagerMode,
        isRRHHMode: isRRHHMode,
        isUserMode: isUserMode,
        approve: approve,
        reject: reject,
        check: check,
        getView: getView,
        download: download
    };

    return vm;

    function getView() {
        if (context.platform == 'mobile')
            return 'views/travelDetail-mobile.html';
        else
            return 'views/travelDetail.html'
    }

    function activate(routeData) {
        base.showLoading();

        if (routeData)
        {
            isManagerMode(routeData.routeInfo.settings.mode == 'manager');
            isRRHHMode(routeData.routeInfo.settings.mode == 'rrhh');
        }

        if (routeData && routeData.travelRequestId) {
            return dataservice.getTravelRequest(routeData.travelRequestId, enums.pictureType.small, false).then(function (travelRequest) {
                if (travelRequest)
                    travelEntity(travelRequest);
            });
        }

        return;
    };

    function viewAttached(view) {
        if (!travelEntity()) {
            base.hideLoading();
            navigation.navigateTo('#/user/travels?refresh=true');
            return true;
        }

        $(".row-text-multiline").dotdotdot();
        base.hideLoading();
        return true;
    }

    function back() {
        navigation.back();
    }

    function approve(entity) {
        base.showLoading();
        dataservice.updateTravelRequestStatus(entity.travelRequestId(), enums.travelRequestStatus.approved).then(function () {
            navigation.navigateTo('#/manager/travels/team?refresh=true');
            base.hideLoading();
        });        
    }

    function check(entity) {
        base.showLoading();
        dataservice.updateTravelRequestStatus(entity.travelRequestId(), enums.travelRequestStatus.completed).then(function () {
            navigation.navigateTo('#/rrhh/travels/employees?refresh=true');
            base.hideLoading();
        });
    }

    function reject(entity) {
        app.showModal('viewmodels/rejectTravel', { travelRequest: entity }).then(function (travelRequest) {
            if (travelRequest) {
                base.showLoading();
                dataservice.updateTravelRequestStatus(entity.travelRequestId(), enums.travelRequestStatus.denied, travelRequest().comments()).then(function () {
                    navigation.navigateTo('#/manager/travels/team?refresh=true');
                    base.hideLoading();
                });
            }
        });      
    }

    function download(entity)
    {
        window.location.href = 'api/travelattachments/files/' + entity.travelAttachmentId();
    }

    function isOneWay() {
        return travelEntity().travelType() == enums.travelType.oneway;
    }
});
