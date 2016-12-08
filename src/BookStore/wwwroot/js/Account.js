// Write your Javascript code.
$(document).ready(function () {
    $('.ui.checkbox').checkbox();

    $('.ui.form')
       .form({
           on: 'blur',
           fields: {
               user: {
                   identifier: 'valid-user',
                   rules: [{
                       type: 'empty',
                       prompt: 'Nhập tài khoản.'
                   }]
               },
               password: {
                   identifier: 'valid-password',
                   rules: [
                       {
                           type: 'empty',
                           prompt: 'Nhập mật khảu.'
                       }                       
                   ]
               }               
           }
       });

    //register form input
    var input1 = $("form input:eq(0)");
    var input2 = $("form input:eq(1)");
    var input3 = $("form input:eq(2)");
    var input4 = $("form input:eq(3)");

    $('#register-form').form({
        on: 'blur',
        inline: true,
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
            }
        }
    });
});