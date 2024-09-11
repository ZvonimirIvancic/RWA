function register() {
    $("#spinner-placeholder").addClass("spinner");
    $("#register-button").prop("disabled", true);

    let registerUrl = "http://localhost:5127/api/User/Register";
    let registerData = {
        "firstName": $("#firstName").val(),
        "lastName": $("#lastName").val(),
        "email": $("#email").val(),
        "phone": $("#phone").val(),
        "username": $("#username").val(),
        "password": $("#password").val()
    }
    $.ajax({
        method: "POST",
        url: registerUrl,
        data: JSON.stringify(registerData),
        contentType: 'application/json'
    }).done(function (tokenData) {
        jwtLogin();
    }).fail(function (err) {
        alert(err.responseText);

        localStorage.removeItem("JWT");
        $("#spinner-placeholder").removeClass("spinner");
        $("#register-button").prop("disabled", false);
    });
}
