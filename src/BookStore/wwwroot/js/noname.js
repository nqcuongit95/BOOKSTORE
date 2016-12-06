$(document).ready(function () {
    initializeCRUDForm();

    initializeDataTable();
    initializeIndexPagination();

    initializeMessageProgress(0, 100);
});

function initializeCRUDForm() {
    $('.crud-form .action-buttons .cancel.button').click(function () {
        hideFormModal();
    });

    initializeValidationCRUDForm();
}

function crudFormSubmit(element) {
    var form = $(element);
    var url = form.closest('form').attr('action');
    var data = form.closest('form').serialize();
    var timeout = 1000;

    activeMessageLoader();

    $.ajax({
        url: url,
        type: 'post',
        data: data,
        success: function (result) {
            if (result !== null) {
                inactiveMessageLoader();
                showMessage(result);

                if (result.type !== 'error') {
                    activeMessageProgress();

                    if (result.results["Reload"] !== null &&
                        result.results["Reload"] === true) {
                        setTimeout(
                            function () {
                                location.reload();
                                message.modal('hide');
                            }, timeout);
                    }
                    else {
                        var redirect = result.results["RedirectUrl"];

                        if (redirect !== null) {
                            setTimeout(
                                function () {
                                    location = redirect;
                                    message.modal('hide');
                                }, timeout);
                        }
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

function initializeDataTable() {
    var table = $('.data-table');

    $('.data-table .action-buttons .item').click(function (event) {
        onClickActionButton(this, event);
    });
}

function initializeIndexPagination() {
    var dropdown = $('#index-pagination>.dropdown');

    dropdown.dropdown('set selected', $('#page-index').val());

    dropdown.dropdown({
        onChange: function (value, text, $selectedItem) {
            location.search({ page: value });
        }
    });
}

function onClickActionButton(element, event) {
    event.preventDefault();

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

                initializeCRUDForm();

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
    if (message !== null) {
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
        total: total
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
