export function currencyCategory(params) {
    return changeCategoryValue(params.value);
  }
  function changeCategoryValue(number) {
    if (number == 0) {
      return "Iron";
    }
    if (number == 1) {
      return "Bronze";
    }
    if (number == 2) {
      return "Silver";
    }
    if (number == 3) {
      return "Gold"
    }
    if (number == 4) {
      return "Platium"
    }
    if (number == 5) {
      return "Diamond"
    }
  }
  