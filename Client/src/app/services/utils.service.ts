export class UtilsService {

    convertToDate(date) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        const d = new Date(date);
        return [d.getFullYear(), pad(d.getMonth() + 1), pad(d.getDate())].join('-');
    }

    encodeURI(str) {
        return encodeURIComponent(str);
    }

    decodeURI(str) {
        return decodeURIComponent(str);
    }

    divideBy100(number) {
        return parseFloat((number / 100).toFixed(2));
    }
}
