$(function() {
    $("body").on('submit', 'form', function () {
        var form = $(this);
        var firstName = form.parent().parent().find("td:first").text();
        var confirmation = confirm("Are you sure you want to delete " + firstName + "?");
        return confirmation;
    });


    $("tr").popover({
        html: true,
        placement: 'right',
        content: function () {
            var td = $(this).find("td:eq(3)");
            return td.html();
        }
    });

    $("tr").on('shown.bs.popover', function() {
        var current = this;
        $("tr").each(function() {
            if (this !== current) {
                $(this).popover('hide');
            }
        });
    });
});