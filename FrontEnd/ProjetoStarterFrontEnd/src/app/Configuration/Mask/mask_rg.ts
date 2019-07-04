export function maskRG(params) {
    return maskRGValue(params.value);
  }
  function maskRGValue(rg) {
    return rg.replace(/(\d{2})(\d{3})(\d{3})(\d{1})/g, "\$1.\$2.\$3\-\$4")
  }
  