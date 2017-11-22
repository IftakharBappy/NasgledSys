
//Add Data Function   
function Add() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        TypeName: $('#TypeName').val(),
        Description: $('#Description').val()
    };
    $.ajax({
        url: "/MgtItemCatelogue/Add",
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
        PKey: $('#PKey').val(),
        TypeName: $('#TypeName').val(),
        Description: $('#Description').val(),
    };
    $.ajax({
        url: "/MgtItemCatelogue/Update",
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
            url: "/MgtItemCatelogue/Delete/" + ID,
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
    $('#PKey').val("");
    $('#TypeName').val("");
    $('#Description').val("");
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#TypeName').val().trim() === "") {
        $('#TypeName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TypeName').css('border-color', 'lightgrey');
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