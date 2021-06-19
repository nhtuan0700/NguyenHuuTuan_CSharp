function currency_format(n) {
  n =  Math.floor(n);
  return n.toLocaleString('it-IT').replaceAll('.',',') + " đ";
}