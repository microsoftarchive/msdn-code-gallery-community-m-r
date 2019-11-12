define(['services/dataservice', 'services/enums', 'config', 'durandal/app', 'viewmodels/viewModels', 'viewmodels/base'],
    function (dataservice, enums, config, app, viewModels, base) {
        var initialized = false,
            vmList = new viewModels.ListViewModel();

        var vm = {
            activate: activate,
            teamVacations: vmList.source,
            teamVacationsCount: vmList.sourceItemsCount,
            title: 'Team vacations title',
            refresh: vmList.refresh,
            acceptVacation: acceptVacation,
            rejectVacation: rejectVacation,
            filter: vmList.filter,
            pages: vmList.pages,
            paginate: vmList.paginate,
            nextPage: vmList.nextPage,
            anyRecord: vmList.anyRecord,
            previousPage: vmList.previousPage
        };

        return vm;

        function activate() {
            if (!initialized) {
                vmList.initialize(dataservice.getTeamVacations, dataservice.getTeamVacationCount, getCustomParameters);
                initialized = true;
            }

            $(".input-search").focus();
        }
        
        function getCustomParameters(parameters) {
            parameters.month = null;
            parameters.year = 2013;
            parameters.status = enums.vacationRequestStatus.pending;
            parameters.pictureType = enums.pictureType.small;
            return parameters;
        }

        function acceptVacation(entity) {
            var parameters = {
                vacationRequestId: entity.vacationRequestId()
            };

            dataservice.acceptVacationRequest(parameters).then(function() {
                vmList.removeSourceItem(entity);
            });
        }
        
        function rejectVacation(entity) {
            var message = 'Are you sure you want to deny the request from ' + entity.employee.fullName() + '?';
            app.showMessage(message, 'confirmation', [enums.options.yes, enums.options.no])
                .then(function (dialogResult) {
                    if (dialogResult == enums.options.yes) {
                        var parameters = {
                            vacationRequestId: entity.vacationRequestId()
                        };
                        dataservice.rejectVacationRequest(parameters).then(function () {
                            vmList.removeSourceItem(entity);
                        });
                    }
                });
        }
    });