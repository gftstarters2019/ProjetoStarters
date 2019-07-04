export function realtiestypeFormatter(params) {
    return typeValue(params.value);
  }
  function typeValue(number) {
    if (number == 0) {
      return "Home";
    }
    if (number == 1) {
      return "Commercial";
    }
  }
  