export class UtilsService {

    convertToDate(date) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        const d = new Date(date);
        return [d.getFullYear(), pad(d.getMonth() + 1), pad(d.getDate())].join('-');
    }
}
