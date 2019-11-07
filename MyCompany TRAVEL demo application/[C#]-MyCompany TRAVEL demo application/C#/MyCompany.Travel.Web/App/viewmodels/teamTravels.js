define(['services/dataservice', 'services/enums', 'config', 'durandal/app', 'durandal/modalDialog', 'viewmodels/viewModels', 'services/logger', 'services/navigation', 'viewmodels/base', 'services/context'],
    function (dataservice, enums, config, app, modalDialog, viewModels, logger, navigation, base, context) {
        var initialized = false,
            useCache = false,
            vmList = new viewModels.ListViewModel();

        var vm = {
            activate: activate,
            viewAttached: viewAttached,
            travels: vmList.source,
            travelsCount: vmList.sourceItemsCount,
            title: 'Travels view',
            refresh: vmList.refresh,
            filter: vmList.filter,
            pages: vmList.pages,
            paginate: vmList.paginate,
            nextPage: vmList.nextPage,
            previousPage: vmList.previousPage,
            anyRecord: vmList.anyRecord,
            approveTravel: approveTravel,
            rejectTravel: rejectTravel,
            openDetail: openDetail,
            getView: getView
        };

        return vm;

        function getView()
        {
            if (context.platform == 'mobile')
                return 'views/teamTravels-mobile.html';
            else
                return 'views/teamTravels.html'
        }

        function activate(routeData) {
            base.showLoading();
            if (!initialized || (routeData && routeData.refresh)) {
                if (context.platform == 'mobile')
                    vmList.initialize(dataservice.getNotFinishedTeamTravelRequests, null, getCustomParameters);
                else
                    vmList.initialize(dataservice.getTeamTravelRequests, dataservice.getTeamTravelRequestsCount, getCustomParameters);

                initialized = true;
                 
                return vmList.refresh();
            }


            return;
        }

        function viewAttached() {
            base.hideLoading();
        }

        function getCustomParameters(parameters) {
            parameters.travelRequestStatus = enums.travelRequestStatus.pending;
            return parameters;
        }

        function approveTravel(entity) {
            dataservice.updateTravelRequestStatus(entity.travelRequestId(), enums.travelRequestStatus.approved).then(function () {
                    vmList.removeSourceItem(entity);
            });
        }

        function rejectTravel(entity) {
            app.showModal('viewmodels/rejectTravel', { travelRequest: entity }).then(function (travelRequest) {
                if (travelRequest) {
                    base.showLoading();
                    dataservice.updateTravelRequestStatus(entity.travelRequestId(), enums.travelRequestStatus.denied, travelRequest().comments()).then(function () {
                        vmList.removeSourceItem(entity);
                        base.hideLoading();
                    });
                }
            });
        }

        function openDetail(entity) {
            navigation.navigateTo('#/manager/travelRequest/' + entity.travelRequestId());
        }
    });