$(document).ready(function () {
        
    var userId = $('#staffId').attr('value');
    
    $('#delete-user').on('click', function (event) {
        event.preventDefault();
       
        $(this).addClass('loading');
        var modal = $('.additional-modal');

        $.ajax({
            type: "post",
            url: confirmDeleteUrl + "/" + userId,
            data: '',
            success: function (result, status, xhr) {

                if (status === 'success') {

                    $(this).removeClass('loading');
                    modal.html(result);
                    modal.modal('show');
                    //setTimeout(function () {
                    //    modal.modal('hide');
                    //}, 1500);
                }
            },
            error: function (xhr, status, error) {

                $(this).removeClass('loading');
            }
        });

    })


    //loader
    function activeLoader() {
        $('#first-load-loader').addClass('active');
    }

    function inactiveLoader() {
        $('#first-load-loader').removeClass('active');
    }


    $('.additional-modal').modal({
        onHidden: function () {
            //reupdate list user
            updateListUser();
        }
    });

    //update list user table
    function updateListUser() {

        activeLoader();
        var userTable = $('#user-segment');

        $.ajax({
            type: "post",
            url: urlListStaff,
            data: '',
            success: function (result, status, xhr) {

                if (status === 'success') {

                    userTable.html(result);
                    inactiveLoader();
                }
            },
            error: function (xhr, status, error) {
                //inactive loading
                inactiveLoader();
            }
        });
    }
});