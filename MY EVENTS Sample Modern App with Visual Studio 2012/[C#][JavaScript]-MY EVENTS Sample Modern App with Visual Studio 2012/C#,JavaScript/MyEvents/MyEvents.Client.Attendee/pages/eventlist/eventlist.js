(function () {
    "use strict";

    var nav = WinJS.Navigation;
    var ui = WinJS.UI;

    var numberOfHighlightedEvents = 1;
    var numberOfBlueEvents = 3;

    function bindData(data, updateLayout) {
        var eventsList = new WinJS.Binding.List(data);

        var groupedEvents = eventsList.createGrouped(
            function (dataItem) {
                return dataItem.type.toString();
            },
            function (dataItem) {
                var title = "";
                if (dataItem.type === MyEvents.Enums.eventGroupType.comingSoon) {
                    title = "coming soon";
                }
                else if (dataItem.type === MyEvents.Enums.eventGroupType.iAssist) {
                    title = "I assist";
                }
                return { title: title };
            },
            function (group1, group2) {
                return group1 - group2;
            }
        );

        updateLayout(document, Windows.UI.ViewManagement.ApplicationView.value, { groupedEvents: groupedEvents });
    };

    function applyMultiEllipsis() {
        setTimeout(function () {
            $(".multi-line-ellipsis").dotdotdot();
        }, 250);

        setTimeout(function () {
            $(".multi-line-ellipsis").dotdotdot();
        }, 500);
    }

    function getGridLayoutGroupInfo() {
        return {
            cellWidth: 250,
            cellHeight: 260,
            enableCellSpanning: true
        };
    };

    function getGridLayoutSnappedGroupInfo() {
        return {
            cellWidth: 285,
            cellHeight: 120,
            enableCellSpanning: true
        };
    };

    function assignTemplate(itemPromise) {
        return itemPromise.then(function (currentItem) {
            var templateElement;
            var result = null;
            if (currentItem.index < numberOfHighlightedEvents) {
                templateElement = document.querySelector("#eventTemplate-highlighted").winControl;
                result = templateElement.render(currentItem.data).then(function (element) {
                    element.style.width = "510px";
                    return element;
                });
            }
            else if (currentItem.index < numberOfBlueEvents) {
                templateElement = document.querySelector("#eventTemplate").winControl;
                result = templateElement.render(currentItem.data).then(function (element) {
                    element.children[0].className += " blue";
                    return element;
                });
            }
            else {
                templateElement = document.querySelector("#eventTemplate").winControl;
                result = templateElement.render(currentItem.data).then(function (element) {
                    return element;
                });
            }

            return result;
        });
    };

    function initAppBar() {
        var appBar = document.getElementById("appbar");
        if (appBar) {
            var appbarwin = appBar.winControl;
            appbarwin.disabled = true;
        }
    }

    ui.Pages.define(MyEvents.Enums.pages.eventList, {
        updateLayout: function (element, viewState, data) {
            var eventsListControl = element.querySelector("#eventsList").winControl;

            if (viewState === Windows.UI.ViewManagement.ApplicationViewState.snapped) {
                initAppBar();
                var snappedTemplate = document.querySelector(".itemTemplate-snapped");
                if (data && data.groupedEvents) {
                    ui.setOptions(eventsListControl, {
                        layout: new ui.GridLayout({ groupInfo: getGridLayoutSnappedGroupInfo }),
                        itemTemplate: snappedTemplate,
                        itemDataSource: data.groupedEvents.dataSource,
                        groupDataSource: data.groupedEvents.groups.dataSource
                    });
                }
                else {
                    ui.setOptions(eventsListControl, {
                        layout: new ui.GridLayout({ groupInfo: getGridLayoutSnappedGroupInfo }),
                        itemTemplate: snappedTemplate
                    });
                }
            } else {
                if (data && data.groupedEvents) {
                    ui.setOptions(eventsListControl, {
                        layout: new ui.GridLayout({ groupInfo: getGridLayoutGroupInfo }),
                        itemTemplate: assignTemplate,
                        itemDataSource: data.groupedEvents.dataSource,
                        groupDataSource: data.groupedEvents.groups.dataSource
                    });
                }
                else {
                    ui.setOptions(eventsListControl, {
                        layout: new ui.GridLayout({ groupInfo: getGridLayoutGroupInfo }),
                        itemTemplate: assignTemplate
                    });
                }

                applyMultiEllipsis();
            }
            
            if (data && data.groupedEvents) {
                var zoomedOutEventsList = document.querySelector("#zoomedOutEventsList").winControl;
                zoomedOutEventsList.itemDataSource = data.groupedEvents.groups.dataSource;
            }
        },
        itemInvoked: function (args) {
            var control = args.currentTarget.winControl;
            var index = args.detail.itemIndex;
            control.itemDataSource.itemFromIndex(index).then(function (item) {
                nav.navigate(MyEvents.Enums.pages.eventDetail, { event: item.data });
            });
        },
        loadData: function () {
            var self = this;

            // Uncomment to get fake static data for design purposes
            //if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
            //    var eventsData = MyEvents.Data.Fake.getEventsList();
            //    bindData(eventsData, this.updateLayout);
            //    return;
            //}

            var promises = [];
            promises.push(MyEvents.Services.EventService.getNextEvents(MyEvents.Config.maximumNumberOfItems));
            promises.push(MyEvents.Services.EventService.getEventsIAssist(MyEvents.Context.currentUserId));

            MyEvents.Application.showLoading();
            WinJS.Promise.join(promises).then(function (results) {
                var cominSoonEvents = results[0];
                for (var i = 0; i < cominSoonEvents.length; i++) {
                    cominSoonEvents[i].type = MyEvents.Enums.eventGroupType.comingSoon;
                }
                var iAssistEvents = results[1];
                for (var i = 0; i < iAssistEvents.length; i++) {
                    iAssistEvents[i].type = MyEvents.Enums.eventGroupType.iAssist;
                }

                var eventsData = cominSoonEvents.concat(iAssistEvents);
                bindData(eventsData, self.updateLayout);
                MyEvents.Application.hideLoading();
            });
        },
        ready: function (element, options) {
            initAppBar();
            var eventsListControl = element.querySelector("#eventsList").winControl;
            eventsListControl.oniteminvoked = this.itemInvoked.bind(this);

            this.loadData();

            WinJS.Binding.processAll(document.querySelector(".user-data"), {
                user: {
                    facebookId: MyEvents.Context.facebookId,
                    name: MyEvents.Context.userName
                }
            });
        }
    });
})();
