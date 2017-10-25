var orderInn;
        
        function clickDep() {
            $('tr.clickDep').click(function () {
                $(this).addClass("selected").siblings().removeClass("selected");
                var tableData = $(this).children("td").map(function () {
                    return $(this).text();
                }).get();
                
                orderInn = {
                    id: $.trim(tableData[0]),
                    time: $.trim(tableData[1]),
                    date: $.trim(tableData[2]),
                    from: $.trim(tableData[3]),
                    to: $.trim(tableData[4]),
                    price: $.trim(tableData[5])
                };
            });
        }

       

        function sendPost() {
            if (orderInn !== null) {
                $.ajax({
                    url: '/Home/DeparturesFromFlightDetails',
                    type: 'POST',
                    data: JSON.stringify(orderInn),
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response !== null && response.success) {
                            window.location.href = "/Home/Passenger";
                        }
                        else {
                            alert("Vennligst velg en reise");
                        }
                    },
                    error: function (x, y, z) {
                        console.log(x, y, z);
                    }
                });
            }
            else {
                alert("Vennligst velg en reise");
            }
        }