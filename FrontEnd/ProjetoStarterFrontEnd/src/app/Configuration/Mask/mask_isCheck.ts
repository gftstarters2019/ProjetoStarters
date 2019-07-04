export function doneFormatter(params) {
    return doneValue(params.value);
  }
  function doneValue(bool){
    if (bool == true)
      return "Check";
    else
      return "UnCheck";
  }
  