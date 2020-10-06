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

    $("#employee-table").on("click", ".js-delete-employee", function (event) {
        var link = $(event.target);
        var table = $(event.delegateTarget);

        bootbox.confirm("Are you shure you want to delete this employee?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/employees/" + link.attr("data-employee-id"),
                    method: "DELETE"
                })
                .done(function () {
                    link.parents("tr").remove();
                    toastr.success("Employee was succesfully deleted!");

                    if (table.find("tbody tr").length === 0) {
                        table.addClass("d-none");
                        $(".alert-info").removeClass("d-none");                            
                    }
                })
                .fail(function (jqXHR) {
                    toastr.error(jqXHR.responseJSON.message, jqXHR.responseJSON.errorDefinition);
                });
            }
        })
        .on("shown.bs.modal", function () {
            $(".btn-primary").blur();
        });

        return false;
    });   
});