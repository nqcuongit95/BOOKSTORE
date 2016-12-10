$(document).ready(function () {

    var _roleId;

    $('.icon.item').popup();

    $('.ui.primary.mini.button').click(function (event) {

        event.preventDefault();

        var elem = $(this);

        activeLoading(elem);

        var modal = $('.create-modal');

        $.ajax({
            url: createRoleUrl,

            success: function (result, status, xhr) {
                if (typeof result === 'string') {

                    deactiveLoading(elem);

                    modal.html(result);

                    modal.modal('show');
                }
                else {

                    deactiveLoading(elem);
                }
            },
            error: function (xhr, status, error) {

                deactiveLoading(elem);
            }
        });
    });

    function activeLoading(element) {
        var elem = $(element);
        elem.addClass('loading');
    }

    function deactiveLoading(element) {
        var elem = $(element);
        elem.removeClass('loading');
    }

    //bind crud event
    $('.item.view').click(function (event) {

        var name = 'unhide';
        var elem = $(this);
        activeLoader(elem, 'unhide');
        var url = viewUrl + "/" + elem.attr('id');

        crud(url, ".view-role-modal", elem, name);

    });

    $('.item.delete').click(function (event) {

        var name = 'remove';
        var elem = $(this);
        _roleId = elem.attr('id');
        activeLoader(elem, name);
        var url = deleteUrl + "/" + _roleId;

        crud(url, ".delete-role-modal", elem, name);

    });

    $('.item.update').click(function (event) {

        var name = 'edit';
        var elem = $(this);
        _roleId = elem.attr('id');
        activeLoader(elem, name);
        var url = editUrl + "/" + _roleId;        
        crud(url, ".update-role-modal", elem, name);

    });


    function crud(url, type, elem, name) {

        var modal = $(type);

        $.ajax({
            url: url,
            type: 'get',
            success: function (result, status, xhr) {
                
                if (status === 'success') {
                    
                    deactiveLoader(elem, name);
                    modal.html(result);
                    modal.modal('show');


                }
                else {

                }
            },
            error: function (xhr, status, error) {
                deactiveLoader(elem, name);
            }
        });
    }

    function activeLoader(elem, str) {
        var icon = $(elem).find('.icon');

        icon.removeClass(str).addClass('spinner').addClass('loading');

    }

    function deactiveLoader(elem, str) {
        var icon = $(elem).find('.icon');
        icon.addClass(str).removeClass('spinner').removeClass('loading')
    }

    //delete role modal
    $('.delete-role-modal').modal({
        onDeny: function () {

            var elem = $(this).find('.negative.button');
            activeLoading(elem);
            var modal = $('.notify-modal');

            $.ajax({
                url: confirmDeleteUrl + "/" + _roleId,
                success: function (result, status, xhr) {
                    if (status === 'success') {
                        deactiveLoading(elem);
                        modal.html(result);
                        modal.modal('show');
                        return true;
                    }
                },
                error: function (xhr, status, error) {
                    deactiveLoading(elem);
                }
            });
        }
    });
    
    //update role modal
    $('.update-role-modal').modal({
        onApprove: function () {

            activeLoading(elem);

            var form = $(this).find('#updateForm');
            var elem = $(this).find('.positive.button');
            var data = form.serialize();

            var modal = $('.notify-modal');

            $.ajax({
                url: editUrl + "/" + _roleId,
                type: 'POST',
                data: data,
                success: function (result, status, xhr) {

                    if (status === 'success') {
                        deactiveLoading(elem);
                        modal.html(result);
                        modal.modal('show');
                        
                        setTimeout(function () {
                            modal.modal('hide');
                        }, 2000);
                    }
                },
                error: function (xhr, status, error) {
                    deactiveLoading(elem);
                }
            });
        }
    });


    $('.notify-modal').modal({
        onHidden: function () {
            location.reload();
        }
    });

});