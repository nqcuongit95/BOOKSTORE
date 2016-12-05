$(document).ready(function () {
    initializeValidation();
});

function initializeValidation() {
    initializeNhaCungCapForm();
}

function initializeNhaCungCapForm() {
    var form = $('#TenNhaCungCap')

    var nhaCungCapFieldsRules = {
        'TenNhaCungCap': {
            identifier: 'TenNhaCungCap',
            rules: [
              {
                  type: 'empty',
                  prompt: form.attr('data-val-required')
              },
              {
                  type: 'maxLength[' +
                      form.attr('data-val-maxlength-max')
                      + ']',
                  prompt: form.attr('data-val-maxlength')
              }
            ]
        }
    }

    $('#NhaCungCap').form({
        fields: nhaCungCapFieldsRules,
        inline: true,
        on: 'blur',
        onSuccess: function (event) {
            event.preventDefault();
            crudFormSubmit(this);
        }
    });
}
