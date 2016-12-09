$(document).ready(function () {

    var roleModal = $('.create-modal');
    var form = $('#role-form');
    
    $('.ui.primary.submit.button').click(function (event) {

        event.preventDefault();
        //alert("click");
        var elem = $(this);

        activeLoading(elem);

        var url = form.attr('action');
        //alert(url);
        var data = form.serialize();
        
        $.ajax({
            url: url,
            type: 'post',
            data: data,
            success: function (result) {
                if (typeof result !== null) {
                    
                    if (result.type !== 'Error') {                                                

                        var statusModal = $('.notify-modal');                                               

                        $.post(result.url, function (response) {
                            //alert(result.url);

                            deactiveLoading(elem);
                            roleModal.modal('hide');
                            
                            statusModal.html(response);
                            statusModal.modal('show');
                        });                        
                    }
                    else {
                        deactiveLoading(elem);
                    }
                }
                else {
                    //show error message
                    deactiveLoading(elem);
                }
            },
            error: function (xhr, status, error) {

                deactiveLoading(elem);
            }
        });

    })

    function activeLoading(element) {
        var elem = $(element);
        elem.addClass('loading');
    }

    function deactiveLoading(element) {
        var elem = $(element);

        elem.removeClass('loading');
    }
    
});