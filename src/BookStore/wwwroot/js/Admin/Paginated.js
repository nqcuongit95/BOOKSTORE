$(document).ready(function () {

    $('.ui.compact.menu .icon').popup()

    var userTable = $('#user-segment');
    inactiveTableLoader();

    $('.ui.pagination a').on('click', function (event) {
        event.preventDefault();
        activeTableLoader();
        var url = $(this).attr('href');

        $.ajax({
            type: "post",
            url: url,
            data: '',
            success: function (result, status, xhr) {
                
                if (status === 'success') {                    
                    userTable.html(result);
                    inactiveTableLoader();
                }
            },
            error: function (xhr, status, error) {
                
                inactiveTableLoader();
            }
        });

    });

    //bind crud event
    $('.item.view').click(function (event) {

        var name = 'file text outline';
        var elem = $(this);
        activeLoader(elem, 'file text outline');
        var url = viewUrl + "/" + elem.attr('id');       
        crud(url, ".additional-modal", elem, name);

    });

    $('.item.delete').click(function (event) {

        var name = 'remove';
        var elem = $(this);
        _roleId = elem.attr('id');
        activeLoader(elem, name);
        var url = deleteUrl + "/" + _roleId;

        crud(url, ".additional-modal", elem, name);

    });

    $('.item.update').click(function (event) {

        var name = 'edit';
        var elem = $(this);
        _roleId = elem.attr('id');
        activeLoader(elem, name);
        var url = editUrl + "/" + _roleId;
        crud(url, ".edit-modal", elem, name);

    });


    function activeDimmer() {
        $('.inverted.dimmer').addClass('active');
    }

    function inactiveDimmer() {
        $('.inverted.dimmer').removeClass('active');
    }

    function activeLoader(elem, str) {
        var icon = $(elem).find('.icon');

        icon.removeClass(str).addClass('spinner').addClass('loading');

    }

    function deactiveLoader(elem, str) {
        var icon = $(elem).find('.icon');
        icon.addClass(str).removeClass('spinner').removeClass('loading');
    }

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
            },
            error: function (xhr, status, error) {
                deactiveLoader(elem, name);
            }
        });
    }

    //loader
    function activeTableLoader() {
        $('#table-loader').addClass('active');
    }

    function inactiveTableLoader() {
        $('#table-loader').removeClass('active');
    }
    
});