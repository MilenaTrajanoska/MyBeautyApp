function GetEventsOnPageLoad() {
    $("#calendar").fullCalendar({
        header: {
            left: "претходен, следен, денес",
            center: "Резервации",
            right: "месец, недела, ден",

        },
        buttonText: {
            today: "денес",
            month: "месец",
            week: "недела",
            day: "ден",
        },
        selectable: true,
        select: function () {
            showModal('Направете резервација, Bind your information to navigate any page', null);
        },
        height: parent,
        events: function (start, end, timezone, callback) {
            $.ajax({
                type: "GET",
                contentType: "application/json",
                url: "Reservations/GetReservationData",
                dataType: "JSON",
                success: function (data) {
                    var events = [];
                    $.each(data, function (i, data) {
                        events.push({
                            title: "data.Service.Name",
                            description: "data.Notes"
                        })
                    })
                }
            })
        }

    })
