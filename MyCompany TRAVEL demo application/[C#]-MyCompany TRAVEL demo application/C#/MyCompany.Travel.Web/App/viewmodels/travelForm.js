define(['services/model', 'services/dataservice', 'services/enums', 'config', 'services/context', 'services/logger', 'services/navigation', 'durandal/app', 'viewmodels/base'],
    function (model, dataservice, enums, config, context, logger, navigation, app, base) {
    var modes = {
        add: 1,
        update: 2
    };

    var mode;

    var travelEntity = ko.observable(),
        title = ko.observable(),
        fileToUpload = ko.observable(),
        fileFriendlyName = ko.observable(),
        isRRHHMode = ko.observable(false),
        travelRequestDirtyFlag = null;

    var canUpload = ko.computed(function () {
        return (fileFriendlyName() && fileFriendlyName().length > 0);
    });

    fileToUpload.subscribe(function (file) {
        if (file) {
            uploadFile();
        }
    });

    var vm = {
        title: title,
        activate: activate,
        travelEntity: travelEntity,
        cancel: cancel,
        accept: accept,
        viewAttached: viewAttached,
        uploadFile: uploadFile,
        fileToUpload: fileToUpload,
        fileFriendlyName: fileFriendlyName,
        canUpload: canUpload,
        deleteAttachment: deleteAttachment,
        isRRHHMode: isRRHHMode
    };

    return vm;

    function createDirtyFlag() {
        //ko.dirtyFlag = function (root) {
        //    var _isDirty = ko.observable(false);

        //    var result = ko.computed(function () {
        //        if (!_isDirty()) {
        //            ko.toJS(root); //just for subscriptions
        //        }

        //        return _isDirty();
        //    });

        //    result.subscribe(function () {
        //        if (!_isDirty()) {
        //            _isDirty(true);
        //        }
        //    });

        //    return result;
        //};

        //dirtyFlag = new ko.dirtyFlag(travelEntity);
        ko.dirtyFlag = function (root, isInitiallyDirty) {
            var result = function () { },
                _initialState = ko.observable(ko.toJSON(root)),
                _isInitiallyDirty = ko.observable(isInitiallyDirty);

            result.isDirty = ko.computed(function () {
                return _isInitiallyDirty() || _initialState() !== ko.toJSON(root);
            });

            result.reset = function () {
                _initialState(ko.toJSON(root));
                _isInitiallyDirty(false);
            };

            return result;
        };

        travelRequestDirtyFlag = new ko.dirtyFlag(travelEntity);
        return;
    }

    function formatDate(date)
    {
        return moment(date).format(config.dateFormat);
    }

    function activate(routeData) {
        base.showLoading();

        if (routeData) {
            isRRHHMode(routeData.routeInfo.settings.mode == 'rrhh');
        }

        if (routeData && routeData.travelRequestId) {
            mode = modes.update;
            title('Edit travel request');
            return dataservice.getTravelRequest(routeData.travelRequestId, enums.pictureType.small, false).then(function (travelRequest) {
                if (canEdit(travelRequest)) {
                    logger.log("Retrieved travel " + travelRequest.travelRequestId() + " from " + travelRequest.from() + " to " + travelRequest.to());
                    travelEntity(travelRequest);
                }
            });
        } else {
            mode = modes.add;
            title('New travel request');
            travelEntity(new model.TravelRequest());
            return;
        }
    };

    function viewAttached(view) {
        if (travelEntity()) {
            /* set datepickers */
            var nowTemp = new Date();
            var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);

            var checkin = $('#depart').datepicker({
                format: config.dateFormat.toLowerCase()
            }).on('changeDate', function (ev) {
                if (ev.date.valueOf() > checkout.date.valueOf()) {
                    var newDate = new Date(ev.date)
                    newDate.setDate(newDate.getDate());
                    checkout.setValue(newDate);
                }
                else {
                    checkout.setValue(checkout.date);
                }
                checkin.hide();
                $('#return')[0].focus();
            }).on('focusout', function () {
                checkin.hide();
            }).data('datepicker');

            // Disable not valid date and mark checkin date as current
            var checkinDate = checkin.date;
            checkinDate.setHours(0, 0, 0, 0);
            checkin.onRender = function (date) {
                return (date.valueOf() < now.valueOf() && date.valueOf() < checkinDate.valueOf()) ? 'disabled' :
                    date.valueOf() == checkinDate.valueOf() ? 'active' : '';
            };

            var checkout = $('#return').datepicker({
                format: config.dateFormat.toLowerCase()
            }).on('changeDate focusout', function (ev) {
                checkout.hide();
            }).data('datepicker');

            // Disable not valid date and mark checkout date as current
            var checkoutDate = checkout.date;
            checkoutDate.setHours(0, 0, 0, 0);
            checkout.onRender = function (date) {
                return date.valueOf() < checkin.date.valueOf() ? 'disabled' :
                    date.valueOf() == checkoutDate.valueOf() ? 'active' : '';
            };

            // hide calendars, to avoid seen them while opening the page
            checkin.hide();
            checkout.hide();

            checkin.setValue(moment(travelEntity().depart()));
            checkout.setValue(moment(travelEntity().return()));

            $("#depart-calendar").click(function () {
                checkin.show();
            });

            $("#return-calendar").click(function () {
                checkout.show();
            });

            createDirtyFlag();
            base.hideLoading();
        }
        else
        {
            base.hideLoading();
            navigateList();
        }

        return true;
    }

    function cancel() {
        if (travelRequestDirtyFlag.isDirty()) {
            var message = 'Are you sure you want to discard your changes"?';
            app.showMessage(message, 'confirmation', [enums.options.yes, enums.options.no])
                .then(function (dialogResult) {
                    if (dialogResult == enums.options.yes) {
                        navigation.back();
                    }
                });
        }
        else {
            navigation.back();
        }

        return;
    }

    function accept() {
        if (mode == modes.add) {
            travelEntity().employeeId(context.currentUser.employeeId());
            travelEntity().status(enums.travelRequestStatus.pending);
        }

        if (!travelEntity().isValid()) {
            travelEntity().errors.showAllMessages(true);
            return;
        }

        if (mode == modes.add) {
            base.showLoading();
            dataservice.addTravelRequest(travelEntity).then(function (data) {
                base.hideLoading();
                navigateList();

            });
        }
        else if (mode == modes.update) {
            base.showLoading();
            dataservice.updateTravelRequest(travelEntity).then(function (data) {
                base.hideLoading();
                navigateList();

            });
        }
    }

    function navigateList()
    {
        if (isRRHHMode())
            navigation.navigateTo('#/rrhh/travels/employees?refresh=true');
        else
            navigation.navigateTo('#/user/travels?refresh=true');
    }

    function uploadFile()
    {
        base.showLoading();
        dataservice.uploadFile(fileToUpload(), travelEntity().travelRequestId(), fileFriendlyName()).then(function (travelAttachmentId) {
            var newAttachment = new model.TravelAttachment();
            newAttachment.travelAttachmentId = ko.observable(travelAttachmentId);
            newAttachment.fileName = ko.observable(fileToUpload().name);
            newAttachment.name = ko.observable(fileFriendlyName());
            newAttachment.travelRequestId = ko.observable(travelEntity().travelRequestId());

            travelEntity().attachments.push(newAttachment);

            fileToUpload(null);
            fileFriendlyName(null);
            base.hideLoading();
        });
    }

    function deleteAttachment(entity) {
        var message = 'Are you sure you want to delete "' + entity.name() + '"?';
        app.showMessage(message, 'confirmation', [enums.options.yes, enums.options.no])
            .then(function (dialogResult) {
                if (dialogResult == enums.options.yes) {
                    base.showLoading();
                    dataservice.deleteTravelRequestAttachment(entity.travelAttachmentId()).then(function () {
                        travelEntity().attachments.remove(entity);
                        base.hideLoading();
                    });
                }
            });
    }

    function canEdit(entity) {
        if (!entity)
            return false;

        return (isRRHHMode() && entity.status() == enums.travelRequestStatus.approved)
            || (!isRRHHMode() && entity.status() == enums.travelRequestStatus.pending);
    }
});
