(function () {
    "use strict";

    var roomColors;
    var scheduleModel = null;
    var drawContent = false;

    function drawSessionMapImage(session) {
        drawContent = false;
        roomColors = ['rgba(199, 53, 69, .9)', 'rgba(199, 53, 69, .9)', 'rgba(199, 53, 69, .9)'];

        if (!session.MapImage || !session.RoomPoints) {
            hideMap();
            return;
        }
        drawMap(session.MapImage, session.RoomPoints);
    }

    function drawEventMapImage(event, schedule) {
        drawContent = true;
        roomColors = ['rgba(67, 93, 130,.9)', 'rgba(165, 174, 191,.9)', 'rgba(131, 154, 170,.9)'];
        scheduleModel = schedule;

        if (!event.MapImage || !event.RoomPoints) {
            hideMap();
            return;
        }

        drawMap(event.MapImage, event.RoomPoints);
    }

    function drawMap(mapImage, roomPoints) {
        var canvas = document.getElementById('map-image');
        var transparentImageMap = document.getElementById('map-image-mapTransparent');
        var imageMap = document.getElementsByName('map-image-map')[0];

        var imageObj = new Image();
        imageObj.onload = function () {
            canvas.width = this.width;
            canvas.height = this.height;
            transparentImageMap.width = this.width;
            transparentImageMap.height = this.height;
            imageMap.width = this.width;
            imageMap.height = this.height;
            canvas.parentElement.style.width = this.width + "px";

            var context = canvas.getContext('2d');
            context.drawImage(this, 0, 0, this.width, this.height);
            drawRooms(roomPoints);
        };

        imageObj.src = "data:image/jpg;base64," + mapImage;
    }

    function hideMap() {
        var roomMapContainter = document.getElementsByClassName('room-map')[0];
        var roomMapTitle = document.getElementsByClassName('title-room')[0];
        roomMapContainter.style.display = "none";
        roomMapTitle.style.display = "none";
    }

    function adaptPointsData(roomPoints) {
        var roomsData = {
            room1: {
                roomNumber: 1,
                points: [],
                externalArea: { leftX: Number.MAX_VALUE, rightX: 0, topY: 0, bottomY: Number.MAX_VALUE }
            },
            room2: {
                roomNumber: 2,
                points: [],
                externalArea: { leftX: Number.MAX_VALUE, rightX: 0, topY: 0, bottomY: Number.MAX_VALUE }
            },
            room3: {
                roomNumber: 3,
                points: [],
                externalArea: { leftX: Number.MAX_VALUE, rightX: 0, topY: 0, bottomY: Number.MAX_VALUE }
            },
        };

        var roomPoint, currentRoom;
        var roomPointsLength = roomPoints.length;
        for (var roomPointIndex = 0; roomPointIndex < roomPointsLength; roomPointIndex++) {
            roomPoint = roomPoints[roomPointIndex];
            currentRoom = roomsData["room" + roomPoint.RoomNumber];
            currentRoom.points.push({ x: roomPoint.PointX, y: roomPoint.PointY });

            currentRoom.externalArea.leftX = Math.min(currentRoom.externalArea.leftX, roomPoint.PointX);
            currentRoom.externalArea.rightX = Math.max(currentRoom.externalArea.rightX, roomPoint.PointX);

            currentRoom.externalArea.bottomY = Math.min(currentRoom.externalArea.bottomY, roomPoint.PointY);
            currentRoom.externalArea.topY = Math.max(currentRoom.externalArea.topY, roomPoint.PointY);
        }

        for (var i = 0; i < 3; i++) {
            var roomNumber = i + 1;
            currentRoom = roomsData["room" + roomNumber];
            currentRoom.center = {
                x: currentRoom.externalArea.leftX + ((currentRoom.externalArea.rightX - currentRoom.externalArea.leftX) / 2),
                y: currentRoom.externalArea.bottomY + ((currentRoom.externalArea.topY - currentRoom.externalArea.bottomY) / 2),
            };
        }

        return roomsData;
    }

    function drawRooms(roomPoints) {
        if (!roomPoints)
            return;

        var canvas = document.getElementById('map-image');
        var imageMap = document.getElementsByName('map-image-map')[0];

        var context = canvas.getContext("2d");
        var roomPointsLength = roomPoints.length;
        var beforeRoom = 0;

        //group points data
        var roomsData = adaptPointsData(roomPoints);
        var currentPoint;

        //draw rooms
        for (var i = 0; i < 3; i++) {
            var roomNumber = i + 1;
            var currentRoom = roomsData["room" + roomNumber];
            drawRoom(context, imageMap, currentRoom);
        }
    }

    function drawRoom(context, imageMap, currentRoom) {
        var coords, pointsLength, currentPoint;

        pointsLength = currentRoom.points.length

        if (!pointsLength)
            return;
        currentPoint = 0;

        //create poligon
        context.fillStyle = roomColors[currentRoom.roomNumber - 1];
        context.beginPath();
        context.moveTo(currentRoom.points[currentPoint].x, currentRoom.points[currentPoint].y);
        coords = currentRoom.points[currentPoint].x + ',' + currentRoom.points[currentPoint].y;

        currentPoint++;
        do {
            // draw poligon point
            context.lineTo(currentRoom.points[currentPoint].x, currentRoom.points[currentPoint].y);
            coords = coords + ',' + currentRoom.points[currentPoint].x + ',' + currentRoom.points[currentPoint].y;
            currentPoint++;
        } while (currentPoint < pointsLength);

        // close polygon
        context.closePath();
        context.fill();

        var area = document.createElement('area');
        area.shape = "POLY";
        area.href = "#";
        area.coords = coords;
        area.style.position = "relative";
        area.id = "area" + currentRoom.roomNumber;

        imageMap.appendChild(area);

        if (drawContent) {
            area.addEventListener('click', roomClickEventHandler);
            drawRoomContent();
        }

        function drawRoomContent() {
            var titleY = currentRoom.externalArea.bottomY + 20;

            context.lineWidth = 1;
            context.textAlign = "center";
            context.fillStyle = "#3e3e3e";
            context.lineStyle = "#3e3e3e";
            context.font = '14px "Segoe UI"';

            var session = getNextSession(currentRoom.roomNumber);
            if (!session) {
                context.fillText(ellipsis(context, "sessions finished", currentRoom.externalArea.leftX, currentRoom.externalArea.rightX), currentRoom.center.x, titleY);
                return;
            }

            MyEvents.Services.FacebookService.getFriendsBySession(session).then(function (facebookUsers) {
                var pictureWidth = 50;
                var pictureHeight = 50;
                var pictureMargin = 5;
                var maxNumberOfPicture = Math.round(((currentRoom.externalArea.rightX - currentRoom.externalArea.leftX) - 30) / (pictureWidth + pictureMargin));
                var numberOfPictures = Math.min(maxNumberOfPicture, facebookUsers.length);

                context.fillText(ellipsis(context, session.Title, currentRoom.externalArea.leftX, currentRoom.externalArea.rightX), currentRoom.center.x, titleY);


                var firstPictureX = currentRoom.center.x - (numberOfPictures / 2) * (pictureWidth + pictureMargin);
                var pictureX;
                var pictureY = titleY + 10;


                for (var i = 0; i < numberOfPictures; i++) {
                    var facebookImage = new Image();
                    facebookImage.id = i;
                    facebookImage.onload = function () {
                        var imageOrder = this.id
                        pictureX = firstPictureX + imageOrder * (pictureWidth + pictureMargin);

                        context.drawImage(this, pictureX, pictureY, pictureWidth, pictureHeight);
                        drawRooms(event);
                    };
                    facebookImage.src = "https://graph.facebook.com/" + facebookUsers[i].FacebookId + "/picture";
                }

                var friendsLeft = facebookUsers.length - maxNumberOfPicture;
                if (friendsLeft > 0) {
                    var friendsLeftMessageY = pictureY + pictureHeight + 15;
                    //context.textAlign = "right";
                    context.fillText("and " + friendsLeft + " more", currentRoom.center.x, friendsLeftMessageY);
                }

            });
        }
    }

    function getNextSession(roomNumber) {
        //Todo: get sessions? or load sessions into events?
        var sessionLength = scheduleModel.EventDefinition.Sessions.length;
        var now = moment();
        var currentSession, halfEventTime, startTime, endTime, isHalfEventLeft, isNotEnded;
        var sessionToGo = null;
        for (var sessionIndex = 0; sessionIndex < sessionLength; sessionIndex++) {
            currentSession = scheduleModel.EventDefinition.Sessions[sessionIndex];
            startTime = moment(currentSession.StartTime);
            halfEventTime = moment(new Date(now.year(), now.month(), now.date(), startTime.hours(), startTime.minutes())).add('minutes', currentSession.Duration / 2);
            endTime = moment(new Date(now.year(), now.month(), now.date(), startTime.hours(), startTime.minutes())).add('minutes', currentSession.Duration);
            isHalfEventLeft = halfEventTime.diff(now, 'minutes') > 0;
            isNotEnded = endTime.diff(now, 'minutes') > 0;

            if (currentSession.RoomNumber === roomNumber && isNotEnded && isHalfEventLeft) {
                sessionToGo = currentSession;
                break;
            }
        }
        return sessionToGo;
    }

    function roomClickEventHandler() {
        if (!scheduleModel.EventDefinition.Sessions)
            return;

        var roomNumber = parseInt(this.id.replace("area", ""));
        var sessionToGo = getNextSession(roomNumber);

        if (sessionToGo) {
            navigateToSessionDetail(sessionToGo);
        } else {

        }
    }

    function ellipsis(context, text, leftX, rightX) {
        var maxWidth = rightX - leftX;
        var width = context.measureText(text).width;
        var ellipsisText = "...";
        var ellipsisWidth = context.measureText(ellipsisText).width;
        if (width <= maxWidth || width <= ellipsisWidth) {
            return text;
        } else {
            var len = text.length;
            while (width >= maxWidth - ellipsisWidth && len-- > 0) {
                text = text.substring(0, len);
                width = context.measureText(text).width;
            }
            return text + ellipsisText;
        }
    }

    function navigateToSessionDetail(selectedSession) {
        WinJS.Navigation.navigate("/pages/sessionDetail/sessionDetail.html", { session: selectedSession });
    }

    WinJS.Namespace.define("MyEvents.Controls.RoomMap", {
        drawEventMapImage: drawEventMapImage,
        drawSessionMapImage: drawSessionMapImage
    });

})();
