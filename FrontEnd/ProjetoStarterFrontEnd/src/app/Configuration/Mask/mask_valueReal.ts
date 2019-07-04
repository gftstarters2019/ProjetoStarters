export function RealFormatter(params) {
    return "R$ " + realValue(params.value);
  }
  function realValue(number) {
    return number.toFixed(2);
  }
  