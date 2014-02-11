$(function () {

    $("[id$='txtDtFrom']").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true,
        changeYear: true, showButtonPanel: true
    });
    $("[id$='txtDtTo']").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true,
        changeYear: true, showButtonPanel: true
    });


    //    $("[id$='txtSearchBoreHole']").autocomplete({
    //        source: function (request, response) {
    //            $.ajax({
    //                type: "GET",
    //                url: "../ajax/autocompleter.ashx?act=searchborehole",
    //                dataType: "json",
    //                data: {
    //                    q: request.term
    //                },
    //                success: function (data) {
    //                    var _arr = [];
    //                    $.each(data.BoreHoles, function (idx, item) {
    //                        item.label = item.BoreHoleName;
    //                        item.value = item.BoreHoleID;
    //                        _arr.push(item);

    //                    });
    //                    response(_arr);
    //                }
    //            });
    //        },
    //        focus: function (event, ui) {
    //            $("[id$='txtSearchBoreHole']").val(ui.item.label);
    //            return false;
    //        },
    //        minLength: 0,
    //        select: function (event, ui) {
    //            $("[id$='txtSearchBoreHole']").val(ui.item.label);
    //            $("[id$='hdnBoreHoleID']").val(ui.item.BoreHoleID);
    //            return false;
    //        },
    //        open: function () {
    //            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
    //        },
    //        close: function () {
    //            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
    //        }
    //    }).data("ui-autocomplete")._renderItem = function (ul, item) {
    //        return $("<li>")
    //        .append("<a><b>" + item.BoreHoleName + "</b> Area : <b>" + item.AreaName + "</b> Depth : <b>"+ item.Depth +"</b></a>")
    //        .appendTo(ul);
    //    };

});

var YAxisGasAttributes = { id: "GasAttributes",
    labels: {
        formatter: function () {
            return this.value + 'cm3 ';
        }
    },
    title: {
        text: 'Gas Attributes'
    }
}


var YAxisPressure = { id: "PressureAttributes",
    labels: {
        formatter: function () {
            return this.value + 'nm ';
        }
    },
    title: {
        text: 'Pressure Attributes'
    },
    opposite: false
}

var YAxisWater = { id: "WaterLevel",
    labels: {
        formatter: function () {
            return this.value + 'L ';
        }
    },
    title: {
        text: 'Water Level'
    },
    opposite: true
}

var YAxisTemperature = { id: "Temperature",
    labels: {
        formatter: function () {
            return this.value + 'C ';
        }
    },
    title: {
        text: 'Temperature'
    },
    opposite: true
}

var YAxisBattery = { id: "Battery",
    labels: {
        formatter: function () {
            return this.value + 'C ';
        }
    },
    title: {
        text: 'Battery'
    },
    opposite: true
}