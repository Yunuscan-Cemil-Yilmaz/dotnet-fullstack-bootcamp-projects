function sendRegisterForm() { 
        const form = $("#registerFormForm");

        if($("#registerPassword").val() !== $("#registerConfirmPassword").val()) {
            showGeneralError("Passwords do not match");
            return;
        }

        const data = {
            Name: $("#registerName").val(),
            LastName: $("#registerSurname").val(),
            Email: $("#registerEmail").val(),
            UserName: $("#registerUsername").val(),
            Password: $("#registerPassword").val(),
        }

        $.ajax({
            url: "/api/Auth/register",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (response) {
                handleResponse({ success: true, message: "Registration successful!" }, form);
            },
            error: function (xhr) {
                let response;
                try {
                    response = JSON.parse(xhr.responseText);
                } catch {
                    response = {
                        success: false,
                        general: true,
                        message: "unexpected error."
                    };
                }

                handleResponse(response, form);
            }
        });
}

function handleResponse(response, form) {
    if (response.success) {
        showGeneralError('✅ ' + response.message);
        alert('✅ Register is successful! \n Please login to continue \n you will redirect to login page in 5 sec.');
        setTimeout(() => {
            window.location.reload();
        }, 5000);
    } else {
        if (response.general) {
            showGeneralError(response.message);
        } else if (response.field) {
            showFieldError(response.field, response.message);
        }
    }
}

function sendLoginForm() {
    console.log("sendLoginForm called");
    const form = $("#loginFormForm");

    const password = $("#loginPassword").val();
    if (password.length < 6) {
        showFieldError("loginPassword", "Password must be at least 6 characters long.");
        return;
    }

    const data = {
        Email: $("#loginEmail").val(),
        Password: password,
    };

    $.ajax({
        url: "/api/Auth/login",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (response) {
            localStorage.clear();
            localStorage.setItem("user", JSON.stringify(response.response.userResponse));
            localStorage.setItem("authToken", response.response.token);
            alert("✅ Successfull Login.\nYou will redirect to app in 2 sec.")
            setTimeout(() => {
                window.location.href = `http://localhost:5182/application/News/${response.response.token}/${response.response.userResponse.id}`;
            }, 2000);
        },
        error: function (xhr) {
            showGeneralError("Your email or password is not valid. Please try again.")
        }
    })
}