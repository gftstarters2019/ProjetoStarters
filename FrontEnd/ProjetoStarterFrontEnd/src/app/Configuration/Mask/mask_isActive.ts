export function currencyStatus(params) {
    return changeStatusValue(params.value);
}
function changeStatusValue(stats: boolean) {
    if (stats == true) {
        return "Active";
    } else {
        return "Inactive"
    }
}
