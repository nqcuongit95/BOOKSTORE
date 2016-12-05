$(document).ready(function () {
    initializeValidation();
});

function initializeValidation() {
    var nhaCungCapValidationRules = {
        TenNhaCungCap: {
            identifier: 'TenNhaCungCap',
            rules: [
              {
                  type: 'empty',
                  prompt: $('#TenNhaCungCap').attr('data-val-required')
              },
              {
                  type: 'maxLength[' +
                      $('#TenNhaCungCap').attr('data-val-maxlength-max')
                      + ']',
                  prompt: $('#TenNhaCungCap').attr('data-val-maxlength')
              }
            ]
        },
    }

    $('#NhaCungCap').form({
        fields: nhaCungCapValidationRules,
        inline: true,
        on: 'blur',
        onSuccess: function (event) {
            event.preventDefault();
            crudFormSubmit(this);
        }
    });
}
