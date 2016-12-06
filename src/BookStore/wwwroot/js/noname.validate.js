function initializeValidationCRUDForm() {
    var forms = $('.crud-form');

    forms.each(function (index, element) {
        var fieldsRules = {};

        var form = $(this);

        form.find('input').each(function (index, element) {
            var input = $(this);
            var name = input.attr('name');
            var rules = [];

            var required = input.attr('data-val-required');
            var maxlength = input.attr('data-val-maxlength-max')

            if (required !== null) {
                rules.push({
                    type: 'empty',
                    prompt: required
                });
            }

            if (maxlength !== null) {
                rules.push({
                    type: 'empty',
                    prompt: required
                });
            }

            if (maxlength) {
                rules.push({
                    type: 'maxLength[' + maxlength + ']',
                    prompt: input.attr('data-val-maxlength')
                });

                input.attr('maxlength', maxlength);
            }

            fieldsRules[name] = {
                identifier: name,
                rules: rules
            };
        });

        form.form({
            fields: fieldsRules,
            inline: true,
            on: 'blur',
            onSuccess: function (event) {
                event.preventDefault();
                crudFormSubmit(this);
            }
        });
    });
}
