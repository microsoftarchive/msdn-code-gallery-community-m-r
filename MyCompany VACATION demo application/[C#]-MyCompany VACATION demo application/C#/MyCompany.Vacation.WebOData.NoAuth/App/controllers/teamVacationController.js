vacationApp.controller('TeamVacationController', ['$scope', '$q', '$dialog', 'config', 'dataService', 'dialogService', 'enums',
    function ($scope, $q, $dialog, config, dataService, dialogService, enums) {
        $scope.teamVacations = [];
        $scope.pages = [];
        $scope.numPages = 0;
        $scope.registersCount = 0;
        $scope.currentPage = 1;
        $scope.query = '';

        var filter = {
            year: 2013,
            status: enums.vacationRequestStatus.pending,
            pictureType: enums.pictureType.small,
            pageSize: config.pageSize
        };

        var pageSize = config.pageSize;

        $scope.search = function () {
            $scope.refresh();
        };

        $scope.refresh = function () {
            $scope.$parent.showLoading();
            filter.pageCount = $scope.currentPage - 1;
            filter.filter = $scope.query;

            dataService.getTeamVacations(filter, $scope)
            .then(function (results) {
                $scope.teamVacations = results.items;
                calculatePaginator(results.count)
                $scope.$parent.hideLoading();
            });
        };

        $scope.paginate = function (page) {
            $scope.currentPage = page.number || page;
            $scope.refresh();
        }

        $scope.nextPage = function () {
            if ($scope.currentPage == $scope.numPages) //is last page
                return;
            $scope.paginate($scope.currentPage + 1);
        }

        $scope.previousPage = function (page) {
            if (($scope.currentPage - 1) == 0) //is first page
                return;
            $scope.paginate($scope.currentPage - 1);
        }

        $scope.anyRecord = function (page) {
            return $scope.teamVacations.length > 0;
        }

        $scope.acceptVacation = function (teamVacation) {
            dataService.acceptVacationRequest(teamVacation, $scope).then(function () {
                var vacationIndex = $scope.teamVacations.indexOf(teamVacation);
                $scope.teamVacations.splice(vacationIndex, 1);
                $scope.registersCount--;
            });
        };

        $scope.rejectVacation = function (teamVacation) {
            var message = 'Are you sure you want to deny the request from ' + teamVacation.employee.fullName + '?'
            dialogService.messageBox(message, [enums.options.yes, enums.options.no])
                .then(function (result) {
                    if (result == enums.options.yes) {
                        dataService.rejectVacationRequest(teamVacation, $scope).then(function () {
                            var vacationIndex = $scope.teamVacations.indexOf(teamVacation);
                            $scope.teamVacations.splice(vacationIndex, 1);
                            $scope.registersCount--;
                        });
                    }
                });
        }

        function calculatePaginator(count) {
            $scope.pages.splice(0, $scope.pages.length)
            $scope.registersCount = count;
            $scope.numPages = Math.ceil(count / config.pageSize);
            var firstPage = Math.floor(($scope.currentPage + 1) / 12) * 12;
            if (firstPage > 0)
                firstPage = firstPage - 2;

            var activePage = $scope.currentPage - 1;
            for (var i = firstPage; i < firstPage + Math.min(12, $scope.numPages) && i < $scope.numPages ; i++) {
                var isActive = activePage == i;
                $scope.pages.push({ number: i + 1, activated: isActive, clickable: true });
            }
        }

        $scope.refresh();
    }]);

