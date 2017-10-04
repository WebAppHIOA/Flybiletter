
$(document).ready(function () {

    console.log("ready!");

});

/*
$.ajax({
    url: '/Home/hentAlleFraFlyplasser',
    type: 'GET',
    dataType: 'json',
    success: function (fraListe) {
        VisDropDown1(fraListe);
    },
    error: function (x, y, z) {
        alert(x + '\n' + y + '\n' + z);
    }
});



$(function () {
    $("#but1").click(function () {
        $("#but1").css("background-color", "blue");
    })
});
*/

$(function () {
    $("#one-way").click(function () {
        $("#return").hide();
        $("#return-label").hide();
    });
});

$(function () {
    $("#both-way").click(function () {
        $("#return").show();
        $("#return-label").show();
    });
});


/*
$("#on").change(function() {
    
    for (var i in fraListe) {
        utStreng += "<option value='" + fraListe[i] + "'>" + fraListe[i] + "</option>";
    }
    $("#drop1").empty().append(utStreng);
}

// opprett en hendelse på dropdown-listen fraFlyplass når siden lastes

$("#o").change(function () {
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

function VisDropDown2(tilListe) {
    var utStreng = "";
    for (var i in tilListe) {
        utStreng += "<option value='" + tilListe[i] + "'>" + tilListe[i] + "</option>";
    }
    $("#drop2").empty().append(utStreng);
}

    */