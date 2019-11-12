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
            add: add,
            edit: edit,
            deleteTravel: deleteTravel,
            openDetail: openDetail,
            getView: getView,
            canEdit: canEdit,
            canDelete: canDelete
        };

        return vm;

        function getView() {
            if (context.platform == 'mobile')
                return 'views/travels-mobile.html';
            else
                return 'views/travels.html'
        }

        function activate(routeData) {
            base.showLoading();
            if (!initialized || (routeData && routeData.refresh)) {
                if (context.platform == 'mobile')
                    vmList.initialize(dataservice.getNotFinishedUserTravelRequests, null, getCustomParameters);
                else 
                    vmList.initialize(dataservice.getUserTravelRequests, dataservice.getUserTravelRequestsCount, getCustomParameters);

                initialized = true;

                if (routeData && routeData.travelId)
                    navigation.navigateTo('#/user/travelDetail/' + routeData.travelId);

                if (routeData && routeData.travelRequest && context.currentUser.isManager())
                    navigation.navigateTo('#/manager/travelRequest/' + routeData.travelRequest);

                return vmList.refresh();
            }

            return;
        }

        function viewAttached() {
            base.hideLoading();
        }

        function getCustomParameters(parameters) {
            parameters.travelRequestStatus = enums.travelRequestStatus.all;
            return parameters;
        }

        function add() {
            navigation.navigateTo('#/user/travelForm');
        }

        function edit(entity) {
            navigation.navigateTo('#user/travelForm/' + entity.travelRequestId());
        }

        function openDetail(entity) {
            navigation.navigateTo('#/user/travelDetail/' + entity.travelRequestId());
        }

        function deleteTravel(entity) {
            var message = 'Are you sure you want to delete "' + entity.name() + '"?';
            app.showMessage(message, 'confirmation', [enums.options.yes, enums.options.no])
                .then(function (dialogResult) {
                    if (dialogResult == enums.options.yes) {
                        base.showLoading();
                        dataservice.deleteTravelRequest(entity.travelRequestId()).then(function () {
                            vmList.removeSourceItem(entity);
                            base.hideLoading();
                        });
                    }
                });
        }

        function canEdit(entity) {
            return entity.status() == enums.travelRequestStatus.pending;
        }

        function canDelete(entity) {
            return entity.status() == enums.travelRequestStatus.pending;
        }       
    });