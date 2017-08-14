
//Add Data Function   
function Add() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        CityName: $('#CityName').val(),
        StateCode: $('#StateCode').val()
    };
    $.ajax({
        url: "/MgtCity/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            clearTextBox();
            $('#msgSuccess').show();
            setTimeout(function () {
                window.location.reload(1);
            }, 5000);
          
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for updating employee's record  
function Update() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        CityKey: $('#CityKey').val(),
        CityName: $('#CityName').val(),
        StateCode: $('#StateCode').val(),
       
    };
    $.ajax({
        url: "/MgtCity/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
           
            clearTextBox();
            $('#msgSuccess').show();
            setTimeout(function () {
                window.location.reload(1);
            }, 5000);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Function for clearing the textboxes  
function clearTextBox() {
   
    $('#CityName').val("");
    $('#StateCode').val("");
    $('#CityKey').val("");
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#CityName').val().trim() === "") {
        $('#CityName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CityName').css('border-color', 'lightgrey');
    }
    if ($('#StateCode').val().trim() === "") {
        $('#StateCode').css('border', '2px solid red');
        isValid = false;
    }
    else {
        $('#StateCode').css('border-color', 'lightgrey');
    }   
    return isValid;
}