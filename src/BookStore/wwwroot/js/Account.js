// Write your Javascript code.
$(document).ready(function () {

    $('.ui.radio.checkbox').checkbox();
    $('.ui.checkbox').checkbox();
    
    function activeLoading(element) {
        var elem = $(element);
        elem.addClass('loading');
    }

    function deactiveLoading(element) {
        var elem = $(element);
        elem.removeClass('loading');
    }

    //register form input
    var input1 = $("form input:eq(0)");
    var input2 = $("form input:eq(1)");
    var input3 = $("form input:eq(2)");
    var input4 = $("form input:eq(3)");
    var radioBtn = $(".ui.radio.checkbox").first().find('input');

    $('#register-form').form({
        fields: {
            fullname: {
                identifier: 'valid-fullname',
                rules: [{
                    type: 'empty',
                    prompt: 'Họ tên không được để trống'
                },
                {
                    type: 'maxLength[' + input1.attr('data-val-length-max') + ']',
                    prompt: input1.attr('data-val-length')
                }]
            },
            user: {
                identifier: 'valid-username',
                rules: [{
                    type: 'empty',
                    prompt: 'Tài khoản không được để trống.'
                },
                {
                    type: 'minLength[' + input2.attr('data-val-length-min') + ']',
                    prompt: input2.attr('data-val-length')
                },
                {
                    type: 'maxLength[' + input2.attr('data-val-length-max') + ']',
                    prompt: "Tài khoản không được quá " + input2.attr('data-val-length-max') + ' kí tự'
                }]
            },
            password: {
                identifier: 'valid-password',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Mật khẩu không được để trống.'
                    },
                    {
                        type: 'minLength[' + input3.attr('data-val-length-min') + ']',
                        prompt: input3.attr('data-val-length')
                    },
                    {
                        type: 'maxLength[' + input3.attr('data-val-length-max') + ']',
                        prompt: "Mật khẩu không được quá " + input3.attr('data-val-length-max') + ' kí tự'
                    }
                ]
            },
            comfirmPassword: {
                identifier: 'valid-confirm-password',
                rules: [
                    {
                        type: 'match[Password]',
                        prompt: input4.attr('data-val-equalto')
                    }
                ]
            },
            role: {
                identifier: 'AssignedRole',
                rules: [
                    {
                        type: 'checked',
                        prompt: radioBtn.attr('data-val-required')
                    }
                ]
            }
        },
        on: 'blur',
        inline: true
    });

    
    $('.ui.primary.button').click(function (event) {
        
        event.preventDefault();

        var form = $('#register-form');
        var url = $(this).attr('action');
        var data = form.serialize();
        var elem = $(this);
        var modal = $('.notify-modal');

        activeLoading(elem);

        $.ajax({
            url: url,
            data: data,
            type: 'post',
            success: function (result, status, xhr) {
                if (status === 'success') {                    
                    deactiveLoading(elem);
                    modal.html(result);
                    modal.modal('show');

                    setTimeout(function () {
                        modal.modal('hide');
                    }, 1500);
                }
            },
            error: function (xhr, status, error) {
                deactiveLoading(elem);
            }
        });
    });

    $('.notify-modal').modal({
        onHidden: function () {
            location.replace(redirectUrl);
        }
    });

});