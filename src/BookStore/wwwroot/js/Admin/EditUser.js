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
    var input5 = $("form input:eq(4)");
    var radioBtn = $(".ui.radio.checkbox").first().find('input');

    $('#update-form').form({
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
            phone: {
                identifier: 'valid-phone',
                rules: [{
                    type: 'empty',
                    prompt: 'Số điện thoại không được để trống'
                },
                {
                    type: 'minLength[' + input2.attr('data-val-length-min') + ']',
                    prompt: input2.attr('data-val-length')
                }]
            },
            user: {
                identifier: 'valid-username',
                rules: [{
                    type: 'empty',
                    prompt: 'Tài khoản không được để trống.'
                },
                {
                    type: 'minLength[' + input3.attr('data-val-length-min') + ']',
                    prompt: input3.attr('data-val-length')
                },
                {
                    type: 'maxLength[' + input3.attr('data-val-length-max') + ']',
                    prompt: "Tài khoản không được quá " + input3.attr('data-val-length-max') + ' kí tự'
                }]
            },
            password: {
                identifier: 'valid-password',
                optional: true,
                rules: [
                    //{
                    //    type: 'empty',
                    //    prompt: 'Mật khẩu không được để trống.'
                    //},
                    {
                        type: 'minLength[' + input4.attr('data-val-length-min') + ']',
                        prompt: input4.attr('data-val-length')
                    },
                    {
                        type: 'maxLength[' + input4.attr('data-val-length-max') + ']',
                        prompt: "Mật khẩu không được quá " + input4.attr('data-val-length-max') + ' kí tự'
                    }
                ]
            },
            comfirmPassword: {
                identifier: 'valid-confirm-password',
                depends: 'valid-password',
                rules: [
                    {
                        type: 'match[valid-password]',
                        prompt: input5.attr('data-val-equalto')
                    }
                ]
            }            
        },
        on: 'blur',
        inline: true
    });


    $('#submit-btn').click(function (event) {

        event.preventDefault();

        var form = $('#update-form');
        var url = form.attr('action');
        var data = form.serialize();

        var elem = $(this);

        var modal = $('.additional-modal');
        activeLoading(elem);

        form.form('validate form');

        if (form.form('is valid')) {

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
        }
        else {
            deactiveLoading(elem);
        }

        $('.additional-modal').modal({
            onHidden: function () {
                //reupdate list user
                updateListUser();
            }
        });

    });

    //reset form update
    $('#reset-btn').on('click', function (event) {

        event.preventDefault();
        $('#update-form').form('reset');

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

    //loader
    function activeLoader() {
        $('#table-loader').addClass('active');
    }

    function inactiveLoader() {
        $('#table-loader').removeClass('active');
    }

});