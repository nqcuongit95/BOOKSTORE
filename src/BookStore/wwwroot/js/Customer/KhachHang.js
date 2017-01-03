// Write your Javascript code.
$(document).ready(function () {
    $(".ui.dropdown").dropdown();   

    $('.ui.form')
       .form({
           on: 'blur',
           fields: {
               name: {
                   identifier: 'valid-name',
                   rules: [{
                       type: 'empty',
                       prompt: 'Tên không được để trống.'
                   }]
               },
               phone: {
                   identifier: 'valid-phone',
                   rules: [
                       {
                           type: 'empty',
                           prompt: 'Số điện thoại không được để trống.'
                       },
                       {
                           type: 'minLength[10]',
                           prompt: "Vui lòng nhập đúng số điện thoại"
                       }
                   ]
               },
               //address: {
               //    identifier: 'valid-address',
               //    rules: [{
               //        type: 'empty',
               //        prompt: 'Vui lòng nhập địa chỉ.'
               //    }]
               //},
               //email: {
               //    identifier: 'valid-email',
               //    rules: [{
               //        type: 'email',
               //        prompt: 'Vui lòng nhập đúng email.'
               //    }]
               //}
           }
       });   

    //helper function
    function activeDimmer() {
        $('#dimmer').addClass('active');

    }

    function deactiveDimmer() {
        $('#dimmer').removeClass('active');

    }

    //submit form
    var form = $('.ui.form');
    $('#submit-form').on('click', function (event) {
        event.preventDefault();
        activeDimmer()

        form.form('validate form');
        var modal = $('#notify-modal');

        if (form.form('is valid')) {

            var url = form.attr('action');
            var data = form.serialize();

            $.ajax({
                type: "post",
                url: url,
                data: data,
                //dataType: 'json',
                success: function (result, status, xhr) {
                    if (status === 'success') {
                        
                        deactiveDimmer()
                        modal.html(result);
                        modal.modal('show');
                    }
                },
                error: function (xhr, status, error) {
                    //go the the fucking hell
                    deactiveDimmer()
                }
            })
        }
        else {
            deactiveDimmer();
        }
    });

    $('#notify-modal').modal({
        onHidden: function () {
            window.location.reload();
        }
    })

});