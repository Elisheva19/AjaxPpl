$(() => {

    loadPeople();

    function loadPeople() {
        $.get('/home/getall', function (people) {
            $("#people-table tr:gt(0)").remove();
            people.forEach(person => {
                $("#people-table tbody").prepend(`

<tr>
<td>${person.firstName}</td>
<td>${person.lastName}</td>

<td>${person.age}</td>
<td>
<button class= "btn btn-danger btn-block delete-person"  data-id=${person.id}>Delete</button>
</td>
<td>
<button class= "btn btn-info btn-block edit-person" data-id='${person.id}' data-first='${person.firstName}' data-last=${person.lastName} data-age =${person.age}>Edit</button>
</td>
</tr>
`);
            });
        });
    }



    $('#add-person').on('click', function () {
        const firstName = $('#first-name').val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();



        $.post('/home/addperson', { firstName, lastName, age }, function () {
            loadPeople();

            $("#first-name").val('');
            $("#last-name").val('');
            $("#age").val('');
        })
    })


    $("#people-table").on('click', '.edit-person', function () {

        const first = $(this).data('first')
        const last = $(this).data('last')
        const age = $(this).data('age')
        const id = $(this).data('id')

        const fullName = first +" " + last;

        $("#first_name").val(first);
        $("#last_name").val(last);
        $("#_age").val(age);
        $("#per-id").val(id);
        $('#edit-name').text(fullName);
        $('.edit').modal();

       

    })

    $('#save').on('click', function () {

        const firstName = $("#first_name").val();
        const lastName= $("#last_name").val();
        const age = $("#_age").val();
        const id = $("#per-id").val();
       
        $.post('/home/updateperson', { id, firstName, lastName, age }, function () {

            $('.edit').modal('hide');
            loadPeople();
          
            $("#first_name").val('');
            $("#last_name").val('');
            $("#_age").val('');

        })

    })

    $("#people-table").on('click', '.delete-person', function () {


        const id = $(this).data('id')

        $.post('/home/deleteperson', { id }, function () {

            loadPeople();
        })


    })

})


