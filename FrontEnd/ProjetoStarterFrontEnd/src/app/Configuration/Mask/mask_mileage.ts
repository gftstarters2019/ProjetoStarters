export function MileageFormatter(params) {
    return mileageValue(params.value) + "Km";
  }
  function mileageValue(number) {
    return number.toFixed(3);
  }
  