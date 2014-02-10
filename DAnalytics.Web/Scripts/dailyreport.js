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