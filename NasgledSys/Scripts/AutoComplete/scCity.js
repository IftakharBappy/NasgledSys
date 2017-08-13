

//Load Data function  
function loadData() {
    $.ajax({
        url: "/MgtCity/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.CityName + '</td>';
                html += '<td>' + item.StateName + '</td>';
             
                html += '<td><a href="#" onclick="return getbyID(' + item.CityKey + ')">Edit</a> | <a href="#" onclick="Delele(' + item.CityKey + ')">Delete</a></td>';
                html += '</tr>';
            });
           
            $('#tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

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
            window.location.reload();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon Employee ID  
function getbyID(EmpID) {
    
    $.ajax({
        url: "/MgtCity/GetbyID/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#CityKey').val(result.CityKey);
            $('#CityName').val(result.CityName);
            $('#StateCode').val(result.StateCode);
          

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
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
            window.location.reload();
            $('#myModal').modal('hide');
            $('#CityName').val("");
            $('#StateCode').val("");
            $('#CityKey').val("");
          
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/MgtCity/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                window.location.reload();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
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
        $('#StateCode').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#StateCode').css('border-color', 'lightgrey');
    }
   
    return isValid;
}