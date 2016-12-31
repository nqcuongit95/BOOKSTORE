$(document).ready(function () {
    
    //loading data when first lanch
    var userTable = $('#user-segment');
    
    $.ajax({
        type: "post",
        url: urlListStaff,
        data: '',
        success: function (result, status, xhr) {

            //some loading indicating            

            if (status === 'success') {
               
                userTable.html(result);                
            }
        },
        error: function (xhr, status, error) {            
            //inactive dimmer
            
        }
    });

    //handling paginated buttons


});