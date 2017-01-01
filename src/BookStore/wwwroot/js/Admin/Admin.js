$(document).ready(function () {
        
    //loading data when first lanch

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
            
            inactiveLoader();
        }
    });


    //loader
    function activeLoader() {
        $('#first-load-loader').addClass('active');
    }

    function inactiveLoader() {
        $('#first-load-loader').removeClass('active');
    }

});