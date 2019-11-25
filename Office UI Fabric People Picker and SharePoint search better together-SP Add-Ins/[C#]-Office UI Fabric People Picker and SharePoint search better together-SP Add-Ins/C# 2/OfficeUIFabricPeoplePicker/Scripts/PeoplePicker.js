
var OfficeUIfabric = OfficeUIfabric || {};
OfficeUIfabric.PeoplePicker = function () { };

OfficeUIfabric.PeoplePicker.prototype = {
    Components: {
        PeoplePicker: {
            /*
             * Create an Office 365 People Picker
             * @param: peoplePickerId = the dom id of the input
             * @param: multi = true or false allow to add single or multiple users
             */
            init: function (peoplePickerId, multi) {
                var peoplePicker = new OfficeUIfabric.PeoplePicker();
                var $searchField = $('#' + peoplePickerId);
                var $searchMoreText = $('.ms-PeoplePicker-searchMoreText');
                var searchTimeout = null;
                DomWindow(peoplePickerId);

                $($searchField).on('focus', function (e) {
                    peoplePicker.Components.Dom.openPeoplePopUp(this.id, '');
                })
                $($searchField).on('keyup', function (e) {
                    onSearchFieldKeyUp(this);
                    /*
                     * Is called when a keyup is done on search field. Triggers the search if necessary
                     */
                    function onSearchFieldKeyUp(control) {
                        var value = control.value.trim();
                        var $peoplePicker = $('#' + control.id);
                        if (value.length <= 3) {
                            $('.ms-PeoplePicker-result').remove();
                            $('.ms-PeoplePicker-searchMoreText').text('Please type in ' + (4 - value.length) + ' more characters...');
                            var style = peoplePicker.Components.Dom.calculateWhereOpen(control.id);
                            $('.ms-ContextualHost').attr('style', style);
                        }
                        else {
                            $('.ms-PeoplePicker-searchMoreText').text('Searching for ' + value);
                            $('.ms-PeoplePicker-result').html('');
                            //if (searchTimeout != null)
                            //    window.clearTimeout(searchTimeout);

                            //searchTimeout = window.setTimeout(processSearch(control.id), 300);
                            processSearch(control.id);
                        }
                    }

                    /*
                     * Process the search and iterates the search results
                     */
                    function processSearch(peoplePickerId) {
                        var $peoplePicker = $('#' + peoplePickerId);
                        if ($searchField.val().trim().length > 3) {
                            peoplePicker.RestApi.GetSearch(_spPageContextInfo.siteAbsoluteUrl + "/_api/search/query?querytext='*" + $searchField.val() + "*'&rowlimit=10&sourceid='b09a7990-05ea-4af9-81ef-edfab16c4e31'")
                                .done(function (people) {
                                    var relevantResults = people.d.query.PrimaryQueryResult.RelevantResults;
                                    var resultCount = relevantResults.TotalRows;
                                    $searchMoreText.text('Found ' + resultCount + ' ' + (resultCount == 1 ? 'person' : 'people'));
                                    var people = [];
                                    var htmlPerson = '';
                                    var htmlPeople = '';
                                    if (resultCount > 0) {
                                        relevantResults.Table.Rows.results.forEach(function (row) {
                                            var person = {};
                                            row.Cells.results.forEach(function (cell) {
                                                person[cell.Key] = cell.Value;
                                            });
                                            people.push(person);
                                        });
                                    }
                                    people.forEach(function (person) {
                                        htmlPerson =
                                            '<div class="ms-PeoplePicker-result person" tabindex="1">' +
                                            '    <div class="ms-Persona ms-Persona--sm">' +
                                            '       <div class="ms-Persona-imageArea">' +
                                                        (person.PictureURL != null ? '<img class="ms-Persona-image" src="' + person.PictureURL + '">' : '<i class="ms-Persona-placeholder ms-Icon ms-Icon--person"></i>') +
                                            '       </div>' +
                                            '       <div class="ms-Persona-details">' +
                                            '           <div class="ms-Persona-primaryText">' + person.PreferredName + '</div>' +
                                            '           <div class="ms-Persona-secondaryText">' + (person.JobTitle || '') + '</div>' +
                                            '       </div>' +
                                            '   </div>' +
                                            '</div>';
                                        htmlPeople += htmlPerson;
                                    });
                                    // Clean the previous search
                                    $('.person').remove();
                                    peoplePicker.Components.Dom.openPeoplePopUp(peoplePickerId, htmlPeople);

                                    $('.ms-PeoplePicker-result').on('click', function () {
                                        var picture = $(this).find('.ms-Persona-image').attr('src') !== undefined
                                            ? '<img class="ms-Persona-image" src="' + $(this).find('.ms-Persona-image').attr('src') + '">'
                                            : '<i class="ms-Persona-placeholder ms-Icon ms-Icon--person"></i>';
                                        var primaryText = $(this).find('.ms-Persona-primaryText').text();
                                        var secondaryText = $(this).find('.ms-Persona-secondaryText').text();

                                        var htmlPerson =
                                            '<div class="ms-Persona ms-Persona--token ms-PeoplePicker-persona ms-Persona--xs">' +
                                            '   <div class="ms-Persona-imageArea">' +
                                            '       ' + picture +
                                            '   </div>' +
                                            '   <div class="ms-Persona-details">' +
                                            '       <div class="ms-Persona-primaryText">' + primaryText + '</div>' +
                                            '       <div class="ms-Persona-secondaryText">' + secondaryText + '</div>' +
                                            '   </div>' +
                                            '   <div class="ms-Persona-actionIcon"><i class="ms-Icon ms-Icon--Cancel"></i></div>' +
                                            '</div>';
                                        $peoplePicker.parent().before(htmlPerson);
                                        $peoplePicker.val('');
                                        if (multi !== undefined && multi === false) {
                                            $peoplePicker.prop('disabled', true);
                                        }

                                        $('.ms-Persona-actionIcon').on('click', function () {
                                            $(this).parent().remove();
                                            $peoplePicker.val('');
                                            if (multi !== undefined && multi === false) {
                                                $peoplePicker.prop('disabled', false);
                                            }
                                        })
                                    })

                                })
                                .fail(function (error) {
                                    error.responseText = (error.responseText === "") ? "Sorry something went wrong." : error.responseText;
                                    console.log(error.responseText);
                                })
                        }
                    }
                })

                /*
                 * Close the peoplepicker dialog when the user click outside of it
                 * @param {OfficeUIFabricPeoplePicker} peoplePicker DOM Id
                 */
                function DomWindow(peoplePicker) {
                    $(window).on('click', function (e) {
                        if (e.target.className != 'ms-TextField-field') {
                            $('.ms-ContextualHost').remove();
                        }
                    });
                }
            },
        },
        Dom: {
            openPeoplePopUp: function (peoplePickerId, htmlPeople) {
                var peoplePicker = new OfficeUIfabric.PeoplePicker();
                var style = peoplePicker.Components.Dom.calculateWhereOpen(peoplePickerId);
                var $peoplePicker = $('#' + peoplePickerId);
                if ($('.ms-ContextualHost').length < 1) {
                    var htmlPeoplePopUp =
                        '<div class="ms-ContextualHost is-positioned is-open ms-ContextualHost--" ' + style + '>' +
                        '   <div class="ms-ContextualHost-main">' +
                        '       <div class="ms-PeoplePicker-results">' +
                        '           <div class="ms-PeoplePicker-resultGroup">' +
                        '               <div class="ms-PeoplePicker-resultGroupTitle">' +
                        '                   Contacts' +
                        '               </div>' +
                                        htmlPeople +
                        '               <button class="ms-PeoplePicker-searchMore">' +
                        '                   <div class="ms-PeoplePicker-searchMoreIcon">' +
                        '                       <i class="ms-Icon ms-Icon--Search"></i>' +
                        '                   </div>' +
                        '                   <div class="ms-PeoplePicker-searchMoreText">' +
                        '                       Please type in 3 more characters...' +
                        '                   </div>' +
                        '               </button>' +
                        '           </div>' +
                        '       </div>' +
                        '   </div>' +
                        '   <div class="ms-ContextualHost-beak"></div>' +
                        '</div>';
                    $('body').append(htmlPeoplePopUp);
                }
                else {
                    $('.ms-PeoplePicker-resultGroupTitle').after(htmlPeople);
                    style = peoplePicker.Components.Dom.calculateWhereOpen(peoplePickerId);
                    $('.ms-ContextualHost').attr('style', style);
                }
            },
            calculateWhereOpen: function (peoplePickerId) {
                var $peoplePicker = $('#' + peoplePickerId);
                var windowHeight = window.outerHeight;
                var currentTop = $peoplePicker.offset().top;
                if ($('.ms-ContextualHost').length < 1) {
                    if (currentTop > 400) {
                        var style = 'style="overflow-x: hidden; overflow-y: auto; max-height: 400px; left: ' +
                            $peoplePicker.offset().left + 'px; top: ' +
                            ($peoplePicker.offset().top - 82) + 'px; width: ' +
                            $peoplePicker.outerWidth() + 'px;"';
                        return style;
                    }
                    else {
                        var style = 'style="overflow-x: hidden; overflow-y: auto; max-height: 400px; left: ' +
                            $peoplePicker.offset().left + 'px; top: ' +
                            ($peoplePicker.offset().top + 42) + 'px; width: ' +
                            $peoplePicker.outerWidth() + 'px;"';
                        return style;
                    }
                }
                else {
                    if (currentTop > 400) {
                        var style = 'overflow-x: hidden; overflow-y: auto; max-height: 400px; left: ' + $peoplePicker.offset().left + 'px; top: ' +
                            ($peoplePicker.offset().top - $('.ms-ContextualHost').outerHeight()) + 'px; width: ' +
                            $peoplePicker.outerWidth() + 'px';
                        return style;
                    }
                    else {
                        var style = 'overflow-x: hidden; overflow-y: auto; max-height: 400px; left: ' + $peoplePicker.offset().left + 'px; top: ' +
                            ($peoplePicker.offset().top + 42) + 'px; width: ' +
                            $peoplePicker.outerWidth() + 'px';
                        return style;
                    }
                }
            }
        }
    },
    RestApi: {
        GetSearch: function (uri) {
            var dfd = $.Deferred();
            // Send the request and return the response.
            $.ajax({
                url: uri,
                xhrFields: {
                    withCredentials: true
                },
                type: "GET",
                headers: { "accept": "application/json;odata=verbose" },
                success: function (data) {
                    dfd.resolve(data);
                },
                error: function (data) {
                    dfd.reject(data);
                }
            });
            return dfd.promise();
        }
    },
    Util: {
        /*
         * get SharePoint stage end point
         */
        getSharePointStage: function () {
            return 'https://giuleon.sharepoint.com/sites/demo';
        }
    }
}