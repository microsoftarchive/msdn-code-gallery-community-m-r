(function () {
    'use strict';

    angular.module('VisitorsApp').factory('modelSvc', [
        'settings',
        modelSvc
    ]);

    function modelSvc(settings) {

        function VisitListItem(entity) {
            if (!entity) {
                throw new Error("Entity must be valid");
            }

            var visitListItem = this;
            visitListItem.id = entity && entity.VisitId;
            visitListItem.visitDateTime = entity && moment.utc(entity.VisitDateTime).toDate();

            visitListItem.visitor = {
                fullName: entity && entity.Visitor && entity.Visitor.FirstName + ' ' + entity.Visitor.LastName,
                company: entity && entity.Visitor && entity.Visitor.Company,
                image: entity && entity.Visitor && entity.Visitor.VisitorPictures && entity.Visitor.VisitorPictures.length ? ('data:image/jpeg;base64,' + entity.Visitor.VisitorPictures[0].Content) : settings.defaultImagePath
            };

            return visitListItem;
        }

        function Visit(entity) {
            if (!entity) {
                throw new Error("Entity must be valid");
            }

            var visit = this;
            visit.visitDateTime = entity && moment.utc(entity.VisitDateTime).toDate();
            visit.hasCar = entity && entity.HasCar;
            visit.Plate = entity && entity.Plate;
            visit.Comments = entity && entity.Comments;

            visit.visitor = {
                id: entity && entity.Visitor && entity.Visitor.VisitorId,
                fullName: entity && entity.Visitor && entity.Visitor.FirstName + ' ' + entity.Visitor.LastName,
                company: entity && entity.Visitor && entity.Visitor.Company,
                image: entity && entity.Visitor && entity.Visitor.VisitorPictures && entity.Visitor.VisitorPictures.length ? ('data:image/jpeg;base64,' + entity.Visitor.VisitorPictures[0].Content) : settings.defaultImagePath
            };

            visit.employee = {
                fullName: entity && entity.Employee && entity.Employee.FirstName + ' ' + entity.Employee.LastName,
                jobTitle: entity && entity.Employee && entity.Employee.JobTitle,
                image: entity && entity.Employee && entity.Employee.EmployeePictures && entity.Employee.EmployeePictures.length ? ('data:image/jpeg;base64,' + entity.Employee.EmployeePictures[0].Content) : settings.defaultImagePath
            };

            return visit;
        }

        var service = {
            VisitListItem: VisitListItem,
            Visit: Visit
        };

        return service;
    }

}());