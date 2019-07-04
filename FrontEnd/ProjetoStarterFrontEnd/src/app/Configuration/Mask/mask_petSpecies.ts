export function SpeciesFormmatter(params) {
    return speciesValue(params.value);
  }
  function speciesValue(number) {
    if (number == 0) {
      return "Canis Lupus Familiaris";
    }
    if (number == 1) {
      return "Felis Catus"
    }
    if (number == 2) {
      return "Mesocricetus Auratus"
    }
    if (number == 3) {
      return "Nymphicus Hollandicus"
    }
    if (number == 4) {
      return "Ara Chloropterus"
    }
  }
  
  