$(function () {

    $("[id$='txtDtFrom']").datepicker({ dateFormat: "dd/mm/yyyy" });
    $("[id$='txtDtTo']").datepicker({ dateFormat: "dd/mm/yyyy" });

    $("[id$='txtSearchBoreHole']").autocomplete("../ajax/autocompleter.ashx?act=searchborehole",
    {
        dataType: 'json',
        minChars: 1,
        parse: function (data) {
            var parsed = [];
            if (data != null) {
                data = data.BoreHoles;
                for (var i = 0; i < data.length; i++) {
                    parsed[parsed.length] = {
                        data: data[i],
                        value: data[i].BoreHoleID,
                        result: data[i].BoreHoleName
                    };
                }
            }
            return parsed;
        },
        autoFill: true,
        cacheLength: 100,
        formatItem: function (row) {
            $("[id$='hdnBoreHoleID']").val("0");
            return row.BoreHoleName + '[Depth=' + row.Depth + ';Area=' + row.AreaName + ']';
        }
    }).result(function (e, data, formatted) {
        $("[id$='hdnBoreHoleID']").val(data.BoreHoleID);
    });

});