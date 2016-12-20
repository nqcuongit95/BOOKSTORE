$(document).ready(function () {

    $('#loginForm')
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
                               prompt: 'Nhập mật khẩu.'
                           }
                       ]
                   }
               }
           });

    //$('.submit.button')
});