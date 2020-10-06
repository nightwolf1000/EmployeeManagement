$(document).ready(function () {
    $("#employee-table, #employee-card").on("click", ".js-toggle-assignments", function (event) {
        var link = $(event.target);
        var container = link.next();

        if (link.hasClass("js-assignments-hidden")) {
            $.ajax({
                url: "/api/employees/" + link.attr("data-employee-id") + "/assignments",
                method: "GET"
            })
            .done(function (data) {
                container.empty().html(paragraphs(data)).slideDown();
                link.text("Hide assignments").removeClass("js-assignments-hidden");
            })
            .fail(function (jqXHR) {
                toastr.error(jqXHR.responseJSON.message, jqXHR.responseJSON.errorDefinition);
            });
        }
        else {
            container.slideUp();
            link.text("Show assignments").addClass("js-assignments-hidden");
        }

        return false;
    });

    var paragraphs = function (assignments) {
        var result = "";
        for (var assignment of assignments)
            result += '<p class="card-text">' + assignment.name + '</p>';

        return result;
    };    

    $('input[type=file]').change(function () {
        var bar = $(".bar")
        var percentage = $(".progress-percentage");

        $(this).simpleUpload("/api/employees/" + $(event.target).attr("data-employee-id") + "/photos", {
            init: function () {
                bar.width(0 + "%");
                percentage.text(0 + "%");
            },
            progress: function (progress) {
                bar.width(progress + "%");
                percentage.html(progress + "%");
            },
            success: function (data) {
                $("#employee-photo").attr('src', "data:image;base64," + data.imageData);
                toastr.success("Employee's photo was succesfully uploaded!");
            },
            error: function (error) {
                toastr.error(error.xhr.responseJSON.message, error.xhr.responseJSON.errorDefinition);
                bar.width(0 + "%");
                percentage.text(0 + "%");
            }
        });
    });
});