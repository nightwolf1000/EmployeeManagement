$(document).ready(function () {
    $("#assignment-table").on("click", ".js-toggle-employees", function (event) {
        var link = $(event.target);
        var container = link.next();

        if (link.hasClass("js-employees-hidden")) {
            $.ajax({
                url: "/api/assignments/" + link.attr("data-assignment-id") + "/employees",
                method: "GET"
            })
            .done(function (data) {
                container.empty().html(paragraphs(data)).slideDown();
                link.text("Hide employees").removeClass("js-employees-hidden");
            })
            .fail(function (jqXHR) {
                toastr.error(jqXHR.responseJSON.message, jqXHR.responseJSON.errorDefinition);
            });
        }
        else {
            container.slideUp();
            link.text("View employees").addClass("js-employees-hidden");
        }

        return false;
    });

    var paragraphs = function (employees) {
        var result = "";
        for (var employee of employees)
            result += "<p>" + employee.name + " <i>(" + employee.department.name + ")</i></p>";

        return result;
    };   

    $("#assignment-table").on("click", ".js-delete-assignment", function (event) {
        var link = $(event.target);
        var table = $(event.delegateTarget);

        bootbox.confirm("Are you shure you want to delete this assignment?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/assignments/" + link.attr("data-assignment-id"),
                    method: "DELETE"
                })
                .done(function () {
                    link.parents("tr").remove();
                    toastr.success("Assignment was succesfully deleted!");

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