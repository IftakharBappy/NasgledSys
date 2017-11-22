
//Add Data Function   
function Add() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        TypeName: $('#TypeName').val(),
        Description: $('#Description').val(),
        PKey: $('#PKey').val()

    };
    $.ajax({
        url: "/MgtHeatingEfficiencyType/Add",
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
        PKey: $('#PKey').val(),
        TypeName: $('#TypeName').val(),
        Description: $('#Description').val(),
    };
    $.ajax({
        url: "/MgtHeatingEfficiencyType/Update",
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
            url: "/MgtHeatingEfficiencyType/Delete/" + ID,
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

    $('#TypeName').val("");
    $('#Description').val("");
    $('#Pkey').val("");
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
        $('#Description').val($('#TypeName').val());

    }
    else {
        $('#Description').css('border-color', 'lightgrey');
    }
    return isValid;
}