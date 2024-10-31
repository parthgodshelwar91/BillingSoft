function AJFunction(value) {
    // FUNCTION FOR NUMBER IDENTIFICATION
    //debugger;
    var Amount = '';
    var parts = value.split('.');
    var integerPart = parts[0];
    var decimalPart = parts.length > 1 ? parts[1] : '';

    Amount = convertNumberToWords(integerPart) + " Rupees";

    if (decimalPart !== '') {
        Amount += ' and ' + convertNumberToWords(decimalPart) + ' Paise';
    }

    document.getElementById('AJ').innerText = Amount;
}

function convertNumberToWords(number) {
    var words = ['Zero', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen'];
    var tens = ['Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

    var num = parseInt(number);
    if (isNaN(num)) return '';
    //debugger;
    if (num < 20) {
        return words[num];
    } else if (num < 100) {
        return tens[Math.floor(num / 10) - 2] + ' ' + (num % 10 !== 0 ? words[num % 10] : '');
    } else if (num < 1000) {
        var remainder = num % 100;
        var hundredsPart = words[Math.floor(num / 100)] + ' Hundred';
        var remainderPart = remainder !== 0 ? (remainder < 20 ? ' ' + words[remainder] : ' ' + tens[Math.floor(remainder / 10) - 2] + (remainder % 10 !== 0 ? ' ' + words[remainder % 10] : '')) : '';
        return hundredsPart + remainderPart;
    } else if (num < 100000) {
        return convertNumberToWords(Math.floor(num / 1000)) + ' Thousand ' + (num % 1000 !== 0 ? convertNumberToWords(num % 1000) : '');
    } else if (num < 10000000) {
        return convertNumberToWords(Math.floor(num / 100000)) + ' Lakh ' + (num % 100000 !== 0 ? convertNumberToWords(num % 100000) : '');
    } else {
        return convertNumberToWords(Math.floor(num / 10000000)) + ' Crore ' + (num % 10000000 !== 0 ? convertNumberToWords(num % 10000000) : '');
    }
}

