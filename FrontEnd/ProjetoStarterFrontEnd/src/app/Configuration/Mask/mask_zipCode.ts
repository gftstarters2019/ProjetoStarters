export function maskZipCode(params){
    return ZipCodeValue(params.value);
}
function ZipCodeValue(zipcode){
    return zipcode.replace(/(\d{5})(\d{3})/g, "\$1\-\$2")
}