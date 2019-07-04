export function maskCpf(params) {
  return maskValue(params.value);
}
 function maskValue(cpf) {
  return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4")
}
