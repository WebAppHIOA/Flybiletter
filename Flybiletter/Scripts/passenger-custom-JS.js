
$(document).ready(

)
function getInfo() {
    var valgtFlyplass = $(this).val();
    $.ajax({
        url: '/Home/hentTilFlyplasser',
        type: 'GET',
        data: { fraFlyPlass: valgtFlyplass },
        dataType: 'json',
        success: function (tilListe) {
            VisDropDown2(tilListe);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
});

function showDepartureData() {
    var utStreng = "";
    for (var i in tilListe) {
        utStreng += "<option value='" + tilListe[i] + "'>" + tilListe[i] + "</option>";
    }
    $("#drop2").empty().append(utStreng);
}