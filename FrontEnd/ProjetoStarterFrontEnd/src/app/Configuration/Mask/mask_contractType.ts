export function currencyType(params) {
    return changeTypValue(params.value);
  }
  function changeTypValue(number) {
    if (number == 0) {
      return "Health Plan";
    }
    if (number == 1) {
      return "Animal Health Plan";
    }
    if (number == 2) {
      return "Dental Plan";
    }
    if (number == 3) {
      return "Life Insurance Plan"
    }
    if (number == 4) {
      return "Real Estate Insurance"
    }
    if (number == 5) {
      return "Vehicle Insurance"
    }
    if (number == 6) {
      return "Mobile Device Insurance"
    }
  }
  