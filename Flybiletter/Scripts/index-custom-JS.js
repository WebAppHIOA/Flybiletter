$(document).ready(function () {

    console.log("ready from index-custom-JS!");

});

$('input').blur(function () {
    console.log("same!");
    if ( $('#ToAirportID').attr('value') == $('#FromAirportID').attr('value')) {
        alert('Same Value');
        console.log("same!");
    }
});

$('input').change(function () {
    console.log("same!");
    if ($('#ToAirportID').val() == $('#FromAirportID').val()) {
        alert("Same Value");
        console.log("same!");
    }
});

$('input').blur(function () {
    alert('Same Value');
    if ($('#ToAirportID').attr('value') == $('#FromAirportID').attr('value')) {
        alert('Same Value'); return false;
    } else { return true; }
});