$(document).ready(function () {

    $('.icon.item').popup();

    $('.ui.primary.mini.button').click(function (event) {

        var elem = $(this);
        event.preventDefault();

        activeLoading(elem);

        var modal = $('.create-modal');

        $.ajax({
            url: url,
            
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

    function activeLoading(element)
    {
        var elem = $(element);
        elem.addClass('loading');
    }

    function deactiveLoading(element) {
        var elem = $(element);

        elem.removeClass('loading');
    }
    
    //bind crud event
    $('.item.view').click(function (event) {
        alert('view');


        $.ajax({
            url: url,

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

    
    
});