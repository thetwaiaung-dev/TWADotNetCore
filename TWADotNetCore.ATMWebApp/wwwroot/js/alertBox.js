
if (message != null) {
    Swal.fire(message);
}

function errorMessage(message) {
    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: message
    });
}

function successMessage(message,url) {
    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    }).then((result) => {
        if (result.isConfirmed && url != undefined) {
            console.log('Url , ', url)
            console.log('Result , ', result.isConfirmed)
            location.href = url;
        }
    });
}