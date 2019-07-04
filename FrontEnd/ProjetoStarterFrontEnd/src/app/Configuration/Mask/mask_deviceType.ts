export function DeviceFormatter(params) {
    return deviceValue(params.value);
}
function deviceValue(number) {
    if (number == 0)
        return "Smartphone";
    if (number == 1)
        return "Tablet";
    if (number == 2)
        return "Laptop";
}
