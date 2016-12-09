$(document).ready(function () {
    initializeCRUDForm();
    $('#form-modal').modal({
        autofocus: false
    });

    initializeDataTable();
    initializeIndexPagination();

    initializeMessageProgress(0, 100);
});

function initializeCRUDForm() {
    initializeValidationCRUDForm();
    updateProperties();
    initializePropertyFieldset();

    $('.crud-form .add.button').each(function () {
        var button = $(this);

        button.click(function () {
            onClickActionButton(this, function (result) {
                var dropdown = button.siblings('.ui.search.selection.dropdown');
                var menu = dropdown.find('.menu');
                var current = result.results['Current'];

                if (dropdown !== undefined &&
                    current !== undefined) {
                    menu.append('<div class="item" data-value="' + current.value +
                        '">' + current.text + '</div>');

                    dropdown.dropdown('refresh');
                    dropdown.dropdown('set selected', current.value);
                }
            });
        });
    });
    $('.crud-form .add-property.button').click(function () {
        addPropertyForm($('.crud-form .properties'));

        updateProperties();
        initializeValidationCRUDForm();
    });
    $('.crud-form .ui.dropdown').dropdown({
        onChange: function () {
            updateProperties();
        }
    });
}

function crudFormSubmit(form, fn) {
    form = $(form);

    var url = form.closest('form').attr('action');
    var data = form.closest('form').serialize();
    var timeout = 1000;

    activeMessageLoader();

    $.ajax({
        url: url,
        type: 'post',
        data: data,
        success: function (result) {
            if (result !== undefined) {
                inactiveMessageLoader();
                showMessage(result);

                if (result.type !== 'error') {
                    activeMessageProgress();

                    if (result.results["Reload"] !== undefined &&
                        result.results["Reload"] === true) {
                        setTimeout(
                            function () {
                                location.reload();
                                message.modal('hide');
                            }, timeout);
                    }
                    else {
                        var redirect = result.results["RedirectUrl"];

                        if (redirect !== undefined) {
                            setTimeout(
                                function () {
                                    location = redirect;
                                    message.modal('hide');
                                }, timeout);
                        }
                    }

                    if (fn !== undefined) {
                        fn(result);
                    }
                }
            }
            else {
                inactiveMessageLoader();
                showDefaultErrorMessage();
            }
        },
        error: function (xhr, status, error) {
            inactiveMessageLoader();
            showDefaultErrorMessage();
        }
    });
}

function initializeFormModal(fn) {
    var form = $('#form-modal > .crud-form');

    var fieldsRules = createFieldsRules(form);

    form.form({
        fields: fieldsRules,
        inline: true,
        on: 'blur'
    });

    form.submit(function (event) {
        event.preventDefault();

        crudFormSubmit(this, fn);
    });

    updateProperties();
    initializePropertyFieldset();

    form.find('.cancel.button').click(function (event) {
        hideFormModal();
    });
    $('#form-modal > .crud-form .add-property.button').click(function () {
        addPropertyForm($('#form-modal > .crud-form .properties'));

        var fieldsRules = createFieldsRules(form);

        form.form({
            fields: fieldsRules,
            inline: true,
            on: 'blur'
        });
    });
    $('#form-modal > .crud-form .ui.dropdown').dropdown({
        onChange: function () {
            updateProperties();
        }
    });
}

function initializePropertyFieldset() {
    $('.property .remove.button').click(function () {
        var fieldset = $(this).closest('.property');

        fieldset.transition({
            animation: 'scale',
            onComplete: function () {
                $(this).closest('.property').remove();

                updateProperties();
                initializeValidationCRUDForm();
            }
        });
    });
}

function addPropertyForm(content) {
    content = $(content);
    var field = $('#properties');

    content.append(field.html());

    field = content.children('.fields:last-child');

    initializePropertyFieldset();
}

function updateProperties() {
    var fields = $('.crud-form .properties .fields');

    fields.each(function (index) {
        $(this).find('input').each(function () {
            var defaultName = $(this).attr('default-name');
            if (defaultName !== undefined) {
                var name = 'properties[' + index + '].' +
                    defaultName;

                $(this).attr('id', name);
                $(this).attr('name', name);
            }
        });

        $(this).find('.ui.search').each(function () {
            $(this).search({
                minCharacters: 0,
                apiSettings: {
                    url: $(this).attr('href') + '?' +
                        $(this).attr('for-id') + '=' +
                        $('#' + $(this).attr('for-id')).val()
                },
                fields: {
                    results: 'results',
                    title: 'title'
                },
                error: false
            });

            $(this).search('clear cache');
        });
    });
}

function initializeDataTable() {
    var table = $('.data-table');

    $('.data-table .action-buttons .item').click(function (event) {
        event.preventDefault();

        onClickActionButton(this);
    });
}

function initializeIndexPagination() {
    var dropdown = $('#index-pagination > .ui.dropdown');

    dropdown.dropdown('set selected', $('#page-index').val());

    dropdown.dropdown({
        onChange: function (value, text, $selectedItem) {
            location.search({ page: value });
        }
    });
}

function onClickActionButton(element, fn) {
    element = $(element);

    var url = element.attr('href');
    var formModal = $('#form-modal');

    activeMessageLoader();

    $.ajax({
        url: url,
        type: 'post',
        success: function (result, status, xhr) {
            if (typeof result === 'string') {
                formModal.html(result);

                initializeFormModal(fn);

                inactiveMessageLoader();
                formModal.modal('show');
            }
            else {
                inactiveMessageLoader();
                showMessage(result);
            }
        },
        error: function (xhr, status, error) {
            inactiveMessageLoader();
            showDefaultErrorMessage();

            return null;
        }
    });
}

function showDefaultErrorMessage() {
    var element = $('#message');
    var messageHeader = $('#message>.header>.title');
    var messageContent = $('#message>.content');

    element.attr('type', 'error');
    messageHeader.text(messageHeader.attr('data-default-value'));
    messageContent.text(messageContent.attr('data-default-value'));

    element.children('.header').find('>.icon').addClass('hidden');
    element.children('.actions').find('>.button').addClass('hidden');
    element.children('.header')
        .find('>.error').removeClass('hidden');
    element.children('.actions')
        .find('>.error').removeClass('hidden');

    element.modal('show');
}

function showMessage(message) {
    if (message !== undefined) {
        var element = $('#message');
        var title = $('#message>.header>.title');
        var content = $('#message>.content');

        element.attr('type', message.type);
        title.text(message.header);
        content.text(message.content);

        element.children('.header').find('>.icon').addClass('hidden');
        element.children('.actions').find('>.button').addClass('hidden');
        element.children('.header')
            .find('>.' + message.type).removeClass('hidden');
        element.children('.actions')
            .find('>.' + message.type).removeClass('hidden');

        element.modal('show');
    }
}

function hideMessage() {
    $('#message').modal('hide');
}

function activeMessageLoader() {
    $('#message-loader').addClass('active');
}

function inactiveMessageLoader() {
    $('#message-loader').removeClass('active');
}

function initializeMessageProgress(duration, total) {
    $('#message-progress').progress({
        duration: duration,
        total: total,
        onSuccess: function () {
            inactiveMessageProgress();
        }
    });
}

function activeMessageProgress(timeout) {
    var element = $('#message-progress');
    var total = element.progress('get total');
    var intervals = timeout / total;

    element.progress('reset');
    element.removeClass('hidden');

    setInterval(function () {
        element.progress('increment');
    }, intervals);
}

function inactiveMessageProgress() {
    var element = $('#message-progress');

    element.progress('reset');
    element.addClass('hidden');
}

function showFormModal() {
    $('#form-modal').modal('show');
}

function hideFormModal() {
    $('#form-modal').modal('hide');
}
