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
});