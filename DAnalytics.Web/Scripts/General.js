// To show the search option menu
//var windowOpener;
//windowOpener = window.opener;
//if (windowOpener) {
//    windowOpener.CloseWindow();
//}
var IsSearchMenuClicked = false;
var AllowSearchMenuClick = false;
function OpenSearchOption() {
    var menuDiv = document.getElementById("dropmenuoptions");
    if (menuDiv.style.display == "none")
        menuDiv.style.display = "block";
    IsSearchMenuClicked = true;
    AllowSearchMenuClick = true;
}
document.onclick = HideSearchOptions;


function HideSearchOptions() {
    if (IsSearchMenuClicked) {
        IsSearchMenuClicked = false;
        return;
    }
    if (AllowSearchMenuClick) {
        var menuDiv = document.getElementById("dropmenuoptions");
        menuDiv.style.display = "none";
        AllowSearchMenuClick = false;
    }
}

function ChangeOption(OptionValue, OptionText) {
    var span = document.getElementById("spnMenuOptionValue");
    var hdnSearchMenuOptionValue = document.getElementById("ctl00_hdnSearchMenuOptionValue");
    span.innerHTML = OptionText;
    hdnSearchMenuOptionValue.value = OptionValue;
}



//Delete confirmation
function ConfirmDelete() {
    if (!confirm('Are you sure you want to delete?')) {
        return false;
    }
    else {
        return true;
    }
}
//Prevent malicious entires
function DenyMalInput() {
    if (Page_ClientValidate()) {
        var length = document.forms[0].elements.length;
        for (var i = 0; i < document.forms[0].elements.length; i++) {
            var el = document.forms[0].elements[i];
            if (el.type == "hidden" || el.id == "ctl00_ContentPlaceHolder1_txtMailEditor")
                continue;
            if (el.value != ReplaceMalstring(el.value, false)) {
                var alertString = "A potentially dangerous input string is received from the form as part of input data." +
                "\nCheck your data for inputs such as '<,>,% etc";
                alert(alertString);
                document.all(el.id).focus();
                return false;
            }
        }
        return true;
    }
    else {
        return false;
    }
}

//Prevent malicious entries for pages without Validation controls
function DenyMalInput_NoValidationCtrls() {
    var length = document.forms[0].elements.length;
    for (var i = 0; i < document.forms[0].elements.length; i++) {
        var el = document.forms[0].elements[i];
        if (el.type == "hidden" || el.id == "ctl00_ContentPlaceHolder1_txtMailEditor")
            continue;
        if (el.value != ReplaceMalstring(el.value, false)) {
            var alertString = "A potentially dangerous input string is received from the form as part of input data." +
                "\nCheck your data for inputs such as '<,>,% etc";
            alert(alertString);
            document.all(el.id).focus();
            return false;
        }
    }
    return true;


}

function ReplaceMalstring(string, companyNameExists) {
    try {
        string = string.replace(/\</g, "");
        string = string.replace(/\>/g, "");
        //string = string.replace(/\(/g, "");
        //string = string.replace(/\)/g, "");
        //string = string.replace(/\#/g, "");
        //string = string.replace(/\&/g, "");
        string = string.replace(/\%/g, "");
    }
    catch (err)
	{ }
    return (string);
}

//-----------Function to Remove Space----------------------
String.prototype.Trim = function () {
    return this.replace(/^\s*/, "").replace(/\s*$/, "")
};

String.prototype.ToBoolean = function () {
    return (/^true$/i).test(this);
};

//---------------------------------------------------------

function IsDateLesser(DateValue1, DateValue2, IsCurrentDate) {
    var firstDate = FormatDateValue(DateValue1); var secondDate = FormatDateValue(DateValue2);
    var DaysDiff; Date1 = new Date(firstDate); Date2 = new Date(secondDate);
    DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));
    if (IsCurrentDate) {
        if ((DaysDiff <= -1)) return true; else return false;
    } else {
        if ((DaysDiff < 0)) return true; else return false;
    }
}

function CompareReservationDates(DateValue1, DateValue2) {
    var firstDate = FormatDateValue(DateValue1); var secondDate = FormatDateValue(DateValue2);
    var DaysDiff; Date1 = new Date(firstDate); Date2 = new Date(secondDate);
    DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));
    if ((DaysDiff <= 0)) return true; else return false;
}

function IsEqualDates(DateValue1, DateValue2) {
    var firstDate = FormatDateValue(DateValue1); var secondDate = FormatDateValue(DateValue2);
    var DaysDiff; Date1 = new Date(firstDate); Date2 = new Date(secondDate);
    DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));
    if ((DaysDiff == 0)) return true; else return false;
}

function IsDateGreater(DateValue1, DateValue2, IsCurrentDate) {
    var firstDate = FormatDateValue(DateValue1); var secondDate = FormatDateValue(DateValue2);
    var DaysDiff; Date1 = new Date(firstDate); Date2 = new Date(secondDate);
    DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));
    if (IsCurrentDate) {
        if ((DaysDiff >= 0)) return true; else return false;
    } else {
        if ((DaysDiff > 0)) return true; else return false;
    }
}

//dd/mm/yyyy -->mm/dd/yyyy
function FormatDateValue(dateValue) {
    if (dateValue.indexOf(" ") != -1) {
        var timeValue = dateValue.split(" ");
        var newDate = timeValue[0].split("/")[1] + "/" + timeValue[0].split("/")[0] + "/" + timeValue[0].split("/")[2];
        newDate = newDate + " " + timeValue[1];
    }
    else {
        var newDate = dateValue.split("/")[1] + "/" + dateValue.split("/")[0] + "/" + dateValue.split("/")[2];
    }

    return newDate;
}

function GetDateTime() {
    var localTime = new Date();
    var today = localTime.getDate() + "/" + (localTime.getMonth() + 1) + "/" + localTime.getYear() + " " + localTime.getHours() + ":" + localTime.getMinutes() + ":" + localTime.getSeconds();
    return today;
}

function GetTime() {
    var localTime = new Date();
    var time = " " + localTime.getHours() + ":" + localTime.getMinutes() + ":" + localTime.getSeconds();
    return time;
}

function GetDate() {
    var localTime = new Date();
    var today = localTime.getDate() + "/" + (localTime.getMonth() + 1) + "/" + localTime.getYear();
    return today;
}

function IsNumeric(val) {
    if (isNaN(parseFloat(val))) {
        return false;
    }
    return true;
}


function OnlyNumeric(e, obj) {
    if (IsNumeric(String.fromCharCode(e.keyCode)) || e.keyCode == 46) {

    }
    else {
        e.cancel = true;
        e.returnValue = false;

    }
}
function CurrencyFormatted(amount) {
    var i = parseFloat(amount);
    if (isNaN(i)) { i = 0.00; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    i = parseInt((i + .005) * 100);
    i = i / 100;
    s = new String(i);
    if (s.indexOf('.') < 0) { s += '.00'; }
    if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
    s = minus + s;
    return s;
}



function OverrideDefaultEvent(event, button) {
    if (event.keyCode == 13) {
        button.click();
        return false;
    }
}

function GetLastDayOfMonthInYear(month, year) {
    var day = 31;
    var isLeapYear = false;
    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0)) { isLeapYear = true; }
    switch (month) {
        case 1:
        case 3:
        case 5:
        case 7:
        case 8:
        case 10:
        case 12:
            day = 31;
            break;
        case 2:
            if (isLeapYear) { day = 29; } else { day = 28; }
            break;
        case 4:
        case 6:
        case 9:
        case 11:
            day = 30;
            break;
    }
    return day;
}

function OpenPopupWindow(Url) {
    javascript: window.open(Url, "popup", "toolbars=0,scrollbars=1");
    return false;
}

function OpenReportViewer(ReportName, QueryString) {
    javascript: window.open("../Report/ReportViewer.aspx?Rpt=" + ReportName + QueryString);
    return false;
}