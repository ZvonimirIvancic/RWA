let loginUrl = "https://localhost:7245/api/User/Login";

function jwtLogin() {
    $("#spinner-placeholder").addClass("spinner");
    $("#login-button").prop("disabled", true);

    let loginData = {
        "username": $("#username").val(),
        "password": $("#password").val()
    }
    $.ajax({
        method: "POST",
        url: loginUrl,
        data: JSON.stringify(loginData),
        contentType: 'application/json'
    }).done(function (tokenData) {
        localStorage.setItem("JWT", tokenData);

        $("#spinner-placeholder").removeClass("spinner");
        $("#login-button").prop("disabled", false);

    }).fail(function (err) {
        alert(err.responseText);

        localStorage.removeItem("JWT");
        $("#spinner-placeholder").removeClass("spinner");
        $("#login-button").prop("disabled", false);
    });
}
