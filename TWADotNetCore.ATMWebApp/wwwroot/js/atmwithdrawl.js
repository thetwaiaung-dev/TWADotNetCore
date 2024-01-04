function noClickWithdrawl(no) {
    var inputNo = document.getElementById("inputNo");
    var inputNoValue = inputNo.value.replace('.00Ks', '')
    inputNo.value = inputNoValue.toString() + no.toString() + ".00Ks";
}

function clearNo() {
    var inputNo = document.getElementById("inputNo");
    inputNo.value = '';
}