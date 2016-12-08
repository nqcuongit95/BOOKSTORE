function initializeValidationCRUDForm() {
    var forms = $('.crud-form');

    forms.each(function (index, element) {
        var form = $(this);

        var fieldsRules = createFieldsRules(form);

        form.form({
            fields: fieldsRules,
            inline: true,
            on: 'blur'
        });

        form.submit(function (event) {
            event.preventDefault();

            crudFormSubmit(this);
        });
    });
}

function initializeValidationFormModal() {
    var form = $('#form-modal > .crud-form');

}

function createFieldsRules(form) {
    var result = {};
    var form = $(form);

    form.find('input').each(function (index, element) {
        var input = $(element);
        var id = input.attr('name');
        var rules = [];

        var required = input.attr('data-val-required');
        var maxlength = input.attr('data-val-maxlength-max');
        var range = input.attr('data-val-range');
        var min = input.attr('data-val-range-min');
        var max = input.attr('data-val-range-max');

        if (typeof required !== 'undefined') {
            input.closest('.field').addClass('required');

            rules.push({
                type: 'empty',
                prompt: required
            });
        }

        if (typeof maxlength !== 'undefined') {
            input.attr('maxlength', maxlength);

            rules.push({
                type: 'maxLength[' + maxlength + ']',
                prompt: input.attr('data-val-maxlength')
            });
        }

        if (typeof range !== 'undefined') {
            if (typeof min !== 'undefined') {
                input.attr('min', min);
            }
            else {
                min = 0;
            }

            if (typeof max !== 'undefined') {
                input.attr('max', max);
            }
            else {
                max = Number.MAX_SAFE_INTEGER;
            }

            input.attr('value', min);
            input.blur(function () {
                if ($(this).val() === null ||
                    $(this).val() === '')
                    $(this).val(min);
            });

            rules.push({
                type: 'integer[' + min + '..' + max + ']',
                prompt: range
            });
        }

        result[id] = {
            identifier: id,
            rules: rules
        };
    });

    return result;
}
