  
function Create() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        WebAddress: $('#WebAddress').val(),
        CityKey: $('#CityKey').val(),
        ProfileKey: $('#ProfileKey').val(),
        Address: $('#Address').val(),
        JobTitle: $('#JobTitle').val(),
        CellPhone: $('#CellPhone').val(),
        OfficePhone: $('#OfficePhone').val(),
        FaxPhone: $('#FaxPhone').val(),
        StateKey: $('#StateKey').val(),
        Zipcode: $('#Zipcode').val(),
        Address2: $('#Address2').val(),
        Zipcode: $('#Zipcode').val(),
        InternalNote: $('#InternalNote').val(),
        GeneralNote: $('#GeneralNote').val()

    };
    $.ajax({
        url: "/MgtClientContact/Create",
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

    function Edit() {
        var res = validate();
        if (res === false) {
            return false;
        }
        var empObj = {
            ContactKey: $('#ContactKey').val(),
            FirstName: $('#FirstName').val(),
            LastName: $('#LastName').val(),
            Email: $('#Email').val(),
            WebAddress: $('#WebAddress').val(),
            CityKey: $('#CityKey').val(),
            ProfileKey: $('#ProfileKey').val(),
            Address: $('#Address').val(),
            JobTitle: $('#JobTitle').val(),
            CellPhone: $('#CellPhone').val(),
            OfficePhone: $('#OfficePhone').val(),
            FaxPhone: $('#FaxPhone').val(),
            StateKey: $('#StateKey').val(),
            Zipcode: $('#Zipcode').val(),
            Address2: $('#Address2').val(),
            Zipcode: $('#Zipcode').val(),
            InternalNote: $('#InternalNote').val(),
            GeneralNote: $('#GeneralNote').val()
        };
        $.ajax({
            url: "/MgtClientContact/Edit",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                clearTextBox();
                $('#msgSuccess').show();
                setTimeout(function () {
                    //window.location.href = 'Index.cshtml';
                    window.location.reload(1);
                }, 3000);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    function clearTextBox() {

        $('#FirstName').val("");
        $('#LastName').val("");
        $('#JobTitle').val("");
        $('#WebAddress').val("");
        ContactKey: $('#ContactKey').val("")
        
    }

    function validate() {
        var isValid = true;
        if ($('#FirstName').val().trim() === "") {
            $('#FirstName').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#FirstName').css('border-color', 'lightgrey');
        }
        if ($('#LastName').val().trim() === "") {
            $('#LastName').val($('#LastName').val());

        }
        else {
            $('#JobTitle').css('border-color', 'lightgrey');
        }
        if ($('#JobTitle').val().trim() === "") {
            $('#JobTitle').val($('#JobTitle').val());

        }
        else {
            $('#JobTitle').css('border-color', 'lightgrey');
        }
        if ($('#WebAddress').val().trim() === "") {
            $('#WebAddress').val($('#WebAddress').val());

        }
        else {
            $('#WebAddress').css('border-color', 'lightgrey');
        }
        return isValid;
    }
}

