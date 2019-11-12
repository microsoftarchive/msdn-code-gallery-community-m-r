define(['config', 'services/enums'], function (config, enums) {
    var TravelRequest = function (entity) {
        var travel = this;
        var nowTemp = new Date();
        var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);

        travel.accommodationNeed = entity && entity.AccommodationNeed;
        travel.comments = entity && entity.Comments;        
        travel.creationDate = entity && ko.observable(moment.utc(entity.CreationDate));
        travel.depart = (entity && ko.observable(moment.utc(entity.Depart))) || ko.observable(moment(now));
        travel.description = entity && entity.Description;        
        travel.employee = new Employee(entity && entity.Employee);	
        travel.employeeId = entity && entity.EmployeeId;
        travel.from = entity && entity.From;
        travel.lastModifiedDate = entity && ko.observable(moment.utc(entity.LastModifiedDate));
        travel.name = entity && entity.Name;
        travel.relatedProject = entity && entity.RelatedProject;
        travel.return = (entity && ko.observable(moment.utc(entity.Return))) || ko.observable(moment(now));
        travel.status = entity && entity.Status;
        travel.to = entity && entity.To;
        travel.transportationNeed = entity && entity.TransportationNeed;	
	
        travel.attachments = new ko.observableArray([]);
        if (entity && entity.TravelAttachments)
        {
            for (var i = 0; i < entity.TravelAttachments.length; i++)
            {
                travel.attachments.push(new TravelAttachment(entity.TravelAttachments[i]));
            }
        }
        travel.travelRequestId = entity && entity.TravelRequestId;	
        travel.travelType = entity && entity.TravelType || enums.travelType.roundtrip;
        travel.isRoundTrip = travel.travelType == enums.travelType.roundtrip;
             
        travel = ko.mapping.fromJS(travel);

        travel = addTravelRequestComputeds(travel);
        travel = addTravelRequestValidations(travel);

        return travel;
    };

    var TravelAttachment = function (entity) {
        var attachment = this;
        attachment.travelAttachmentId = entity && entity.TravelAttachmentId;
        attachment.fileName = entity && entity.FileName;
        attachment.name = entity && entity.Name;
        attachment.travelRequestId = entity && entity.TravelRequestId;
    };

    var Employee = function (entity) {
        var employee = this;

        employee.employeeId = entity && entity.EmployeeId;
        employee.firstName = entity && entity.FirstName;
        employee.lastName = entity && entity.LastName;
        employee.jobTitle = entity && entity.JobTitle;
        employee.pictureContent = entity && entity.EmployeePictures && entity.EmployeePictures.length > 0 ? entity.EmployeePictures[0].Content : null;
        employee.isManager = entity && entity.IsManager;
        employee.isRRHH = entity && entity.IsRRHH;

        return addEmployeeComputeds(ko.mapping.fromJS(employee));
    };

    var TravelDistribution = function (entity) {
        var travelDistribution = this;
        travelDistribution.city = entity && entity.City;
        travelDistribution.yearCount = entity && entity.YearCount;
        travelDistribution.monthCount = entity && entity.MonthCount;
        travelDistribution.percent = entity && entity.Percent;
        travelDistribution.employeesPictures = entity && entity.EmployeesPictures;

        return ko.mapping.fromJS(travelDistribution)
    };
    

    var model = {
        TravelRequest: TravelRequest,
        Employee: Employee,
        TravelDistribution: TravelDistribution,
        TravelAttachment: TravelAttachment
    };

    return model;

    //#region Internal methods

    function addEmployeeComputeds(entity) {
        entity.fullName = ko.computed(function () {
            return (entity.firstName() || '') + ' ' + (entity.lastName() || '');
        });
        entity.picture = ko.computed(function () {
            return 'data:image/jpeg;base64,' + entity.pictureContent();
        });
        return entity;
    }

    function addTravelRequestComputeds(entity) {
            entity.statusName = ko.computed(function () {
                switch (entity.status()) {
                    case enums.travelRequestStatus.pending:
                        return "Pending";
                    case enums.travelRequestStatus.approved:
                        return "Approved";
                    case enums.travelRequestStatus.completed:
                        return "Completed";
                    case enums.travelRequestStatus.denied:
                        return "Denied";
                }
                return 'Unknown'; 
            });

            entity.travelTypeName = ko.computed(function () {
                switch (entity.travelType()) {
                    case enums.travelType.oneway:
                        return "One way";
                    case enums.travelType.roundtrip:
                        return "Round trip";
                }
                return 'Unknown';
            });
            
            entity.travelType.subscribe(function (type) {
                entity.isRoundTrip(type == enums.travelType.roundtrip);
            });

            return entity;
    }
    
    function addTravelRequestValidations(entity) {
        entity.depart.extend({ required: true });
        entity.description.extend({ required: true });
        entity.employeeId.extend({ required: true });
        entity.from.extend({ required: true });
        entity.name.extend({ required: true });
        entity.return.extend({ required: true });
        entity.to.extend({ required: true });

        entity["errors"] = ko.validation.group(entity); 

        return entity;
    }

    //#endregion
});