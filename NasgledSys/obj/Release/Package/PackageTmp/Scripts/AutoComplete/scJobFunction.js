
//Add Data Function   
function Add() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        FunctionName: $('#FunctionName').val(),
        Description: $('#Description').val()
    };
    $.ajax({
        url: "/MgtJobFunction/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            clearTextBox();
            $('#msgSuccess').show();
            setTimeout(function () {
                window.location.reload(1);
            }, 3000);
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
        JobFunctionKey: $('#JobFunctionKey').val(),
        FunctionName: $('#FunctionName').val(),
        Description: $('#Description').val(),
    };
    $.ajax({
        url: "/MgtJobFunction/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            clearTextBox();
            $('#msgSuccess').show();
            setTimeout(function () {
                window.location.reload(1);
            }, 3000);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/MgtJobFunction/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                clearTextBox();
                $('#msgSuccess').show();
                setTimeout(function () {
                    window.location.reload();
                }, 3000);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
//Function for clearing the textboxes  
function clearTextBox() {
    $('#JobFunctionKey').val("");
    $('#FunctionName').val("");
    $('#Description').val("");
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#FunctionName').val().trim() === "") {
        $('#FunctionName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FunctionName').css('border-color', 'lightgrey');
    }
    if ($('#Description').val().trim() === "") {
        $('#Description').css('border', '2px solid red');
        isValid = false;
    }
    else {
        $('#Description').css('border-color', 'lightgrey');
    }
    return isValid;
}