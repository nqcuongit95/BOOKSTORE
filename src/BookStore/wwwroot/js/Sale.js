$(document).ready(function () {
    $('.top.menu .item').tab();

    $('.ui.search').search({        
        apiSettings: {
            url: urlSearch + "?val={query}"
        },
        fields: {            
            title: 'name',
            description: 'phone'
        },        
        minCharacters: 3
    });

});