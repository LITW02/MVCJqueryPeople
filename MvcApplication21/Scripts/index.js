$(function() {
    $("#add-person").on('click', function() {
        $(".modal").modal();
    });

    $(".edit").on('click', function() {
        var tr = $(this).closest('tr');
        var firstName = tr.find("td:eq(0)").text();
        var lastName = tr.find("td:eq(1)").text();
        var age = tr.find("td:eq(2)").text();
        $("#firstName").val(firstName);
        $("#lastName").val(lastName);
        $("#age").val(age);

        $("#modal-form").attr('action', '/people/edit');
        $("#id").val(tr.data('person-id'));

        $(".modal").modal();
    });

    $(".one").on('click', function() {
        $(".two").on('click', function() {
            alert('foo');
        });
    });
});