$(document).ready(function () {

    $('.ui.form')
        .form({
            on: 'blur',
            inline: true,
            fields: {
                role: {
                    identifier: 'valid-role',
                    rules: [{
                        type: 'empty',
                        prompt: 'Tên quyền không được để trống.'
                    }]
                }
            }
        });

});