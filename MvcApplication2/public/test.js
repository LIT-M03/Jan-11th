$(function() {
    $("form").on('submit', function () {
        var form = $(this);
        var firstName = form.parent().parent().find("td:first").text();
        var confirmation = confirm("Are you sure you want to delete " + firstName + "?");
        return confirmation;
    });
});