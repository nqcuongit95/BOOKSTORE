//fortmat input    
numeral.register('locale', 'vn', {
    delimiters: {
        thousands: ',',
        decimal: '.',
    },
    abbreviations: {
        thousand: 'k',
        million: 'm',
        billion: 'b',
        trillion: 't'
    },
    ordinal: function (number) {
        return '';
    },
    currency: {
        symbol: '₫'
    }
});

numeral.locale('vn');
