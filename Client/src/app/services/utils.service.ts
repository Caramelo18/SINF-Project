export class UtilsService {

    convertToDate(date) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        const d = new Date(date);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }
}
