define(['services/dataservice', 'services/enums', 'config', 'durandal/app', 'durandal/modalDialog', 'viewmodels/viewModels', 'services/logger', 'services/navigation', 'viewmodels/base'],
    function (dataservice, enums, config, app, modalDialog, viewModels, logger, navigation, base) {
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
            edit: edit,
            check: check,
            openDetail: openDetail
        };

        return vm;

        function activate(routeData) {
            base.showLoading();
            if (!initialized || (routeData && routeData.refresh)) {
                vmList.initialize(dataservice.getAllTravelRequests, dataservice.getAllTravelRequestsCount, getCustomParameters);
                initialized = true;
                 
                return vmList.refresh();
            }


            return;
        }

        function viewAttached() {
            base.hideLoading();
        }

        function getCustomParameters(parameters) {
            parameters.travelRequestStatus = enums.travelRequestStatus.approved;
            return parameters;
        }

        function edit(entity) {
            navigation.navigateTo('#/rrhh/travelForm/' + entity.travelRequestId());
        }

        function check(entity) {
            base.showLoading();
            dataservice.updateTravelRequestStatus(entity.travelRequestId(), enums.travelRequestStatus.completed).then(function () {
                vmList.removeSourceItem(entity);
                base.hideLoading();
            });
        }

        function openDetail(entity) {
            navigation.navigateTo('#/rrhh/travelRequest/' + entity.travelRequestId());
        }
    });