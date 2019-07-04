export function colorFormatter(params) {
    return colorValue(params.value);
  }
  function colorValue(number) {
    if (number == 0) {
      return "White";
    }
    if (number == 1) {
      return "Silver";
    }
    if (number == 2) {
      return "Black";
    }
    if (number == 3) {
      return "Gray";
    }
    if (number == 4) {
      return "Red";
    }
    if (number == 5) {
      return "Blue";
    }
    if (number == 6) {
      return "Brown";
    }
    if (number == 7) {
      return "Yellow";
    }
    if (number == 8) {
      return "Green";
    }
    if (number == 9) {
      return "Other";
    }
  }
  