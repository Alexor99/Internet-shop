$(document).ready(function () {
    //start of the document ready function
    //delcaring global variable to hold primary key value.
    var id;
    var url;
    $('.delete-prompt').click(function (event) {
        event.preventDefault();

        id = $(this).attr('id');
        url = $(this).attr('href');

        $('#modalFade').modal('show');
    });

    $('.delete-done').click(function () {
        location.reload();
    });

    //$('#modalFade').click(function () {
    //    location.reload();
    //});
    
    $('.delete-confirm').click(function () {
        if (id !== '') {
            $.ajax({
                url: url,
                data: { 'Id': id },
                type: 'post',
                success: function (data) {
                    if (data) {
                        //now re-using the boostrap modal popup to show success message.
                        //dynamically we will change background colour
                        if ($('.modal-header').hasClass('alert-danger')) {
                            $('.modal-header').removeClass('alert-danger').addClass('alert-success');
                            //hide ok button as it is not necessary
                            $('.delete-confirm').css('display', 'none');
                            $('.delete-calcel').css('display', 'none');

                            $('.delete-done').toggleClass('invisible visible');
                        }
                        $('.success-message').html('Record deleted successfully');
                    }
                }, error: function (err) {
                    if (!$('.modal-header').hasClass('alert-danger')) {
                        $('.modal-header').removeClass('alert-success').addClass('alert-danger');
                        $('.delete-confirm').css('display', 'none');
                    }
                    $('.success-message').html(err.statusText);
                }   
            });
        }
    });

    //function to reset bootstrap modal popups
    $("#myModal").on("hidden.bs.modal", function () {
        $(".modal-header").removeClass(' ').addClass('alert-danger');
        $('.delete-confirm').css('display', 'inline-block');
        $('.success-message').html('').html('Are you sure you wish to delete this record ?');
    });

    //end of the docuement ready function
});